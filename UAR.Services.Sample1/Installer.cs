using System.Collections.Generic;
using System.Linq;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using UAR.Services.Contracts;

namespace UAR.Services.Sample1
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(GetRegistrations().ToArray());
        }

        private IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IAmService>()
                .ImplementedBy<MyService1>()
                .Named("MyService1")
                .LifestyleSingleton();
        }
    }
}