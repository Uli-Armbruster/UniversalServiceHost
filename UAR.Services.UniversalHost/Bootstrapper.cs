using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace UAR.Services.UniversalHost
{
    static internal class Bootstrapper
    {
        internal static IWindsorContainer ConfigureContainer()
        {
            Console.WriteLine("Load IoC");

            var appDomainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var detectedAssemblies = FromAssembly.InDirectory(new AssemblyFilter(appDomainDirectory));

            var container = new WindsorContainer();
            container.Install(detectedAssemblies);
            container.Register(Component.For<IWindsorContainer>().Instance(container));

            Console.WriteLine(String.Format("IoC loaded with '{0}' components",
                                            container.Kernel.GetAssignableHandlers(typeof(object)).Count()));

            return container;
        }
    }
}