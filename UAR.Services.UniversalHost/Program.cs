using System;
using System.Linq;

using Topshelf;

using UAR.Services.Contracts;

namespace UAR.Services.UniversalHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrapper.ConfigureContainer();
            var ide = container.Resolve<IKnowYourIDE>();
            var debug = container.Resolve<ICanDebug>();

            var services = container
                .ResolveAll<IAmService>()
                .ToList();

            if (services.Count == 0)
            {
                throw new ApplicationException("No service registration found");
            }

            if (args.Length == 0 && ide.VisualStudioIsRunning)
            {
                debug.This(services);
            }
            if (args.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("If you want to install or uninstall a service or all services,");
                Console.WriteLine("use parameters \"install\" or \"uninstall\" optionally followed");
                Console.WriteLine("by the service name (or a part of it) in quotes!");
                Console.Write("Press any key to exit");
                Console.ReadLine();
            }
            else if (args.Length != 3)
            {
                Console.WriteLine(string.Format("{0} all '{1}' detected service(s)", args[0], services.Count));

                foreach (IAmService service in services)
                {
                    InstallService(service, args[0]);
                }

                Console.WriteLine();
                Console.WriteLine("Operation(s) finished successfully");
            }
            else
            {
                var displayName = args[1];
                var service = services.FirstOrDefault(s => s.DisplayName.Contains(displayName));

                if (service == null)
                {
                    throw new ApplicationException(string.Format("Service '{0}' is not installed", displayName));
                }

                InstallService(service, "start");
            }
        }

        static void InstallService(IAmService service, string operationName)
        {
            Console.WriteLine(string.Format("{0} service '{1}'",operationName, service.DisplayName));

            var host = HostFactory.New(x =>
            {
                x.Service<IAmService>(s =>
                {
                    s.SetServiceName("ps");
                    s.ConstructUsing(name => service);
                    s.WhenStarted(ps => ps.Start());
                    s.WhenStopped(ps => ps.Stop());
                });

                x.ApplyCommandLine();
                x.RunAsLocalService();
                x.SetDescription(service.Description);
                x.SetDisplayName(service.DisplayName);
                x.SetServiceName(service.ServiceName);
            });

            Console.WriteLine("operation finished successfully");
            host.Run();
        }
    }
}
