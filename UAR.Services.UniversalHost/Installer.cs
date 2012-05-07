using System.Collections.Generic;
using System.Linq;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using UAR.Services.Contracts;

namespace UAR.Services.UniversalHost
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
                .For<ICanDebug, IKnowYourIDE>()
                .ImplementedBy<Debug>()
                .LifestyleSingleton();
        }
    }
}