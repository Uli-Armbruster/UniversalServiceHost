using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using UAR.Services.Contracts;

namespace UAR.Services.UniversalHost
{
    public class Debug : ICanDebug, IKnowYourIDE
    {
         public void This(IList<IAmService> services)
         {
             // directly start a service to debug without installation
             Console.WriteLine("Select service to debug (without installation), <ESC> to exit.");
             Console.WriteLine();
             Console.WriteLine("Note: if you want to install or uninstall a service or all services,");
             Console.WriteLine("use parameters \"install\" or \"uninstall\" optionally followed");
             Console.WriteLine("by the service name (or a part of it) in quotes!");
             Console.WriteLine();

             for (int i = 0; i < services.Count; i++)
             {
                 Console.WriteLine("{0} - {1}",
                                   (i + 1).ToString(CultureInfo.InvariantCulture),
                                   services[i].DisplayName);
             }
             Console.WriteLine();


             int selectedServiceNumber;
             do
             {
                 ConsoleKeyInfo key = Console.ReadKey();
                 Console.WriteLine();

                 if (key.Key == ConsoleKey.Escape)
                     return;

                 if (!int.TryParse(key.KeyChar.ToString(CultureInfo.InvariantCulture), out selectedServiceNumber) ||
                     selectedServiceNumber <= 0 || selectedServiceNumber > services.Count)
                 {
                     Console.WriteLine("Invalid input");
                     selectedServiceNumber = -1;
                 }
             }
             while (selectedServiceNumber == -1);

             var service = services[selectedServiceNumber - 1];

             Console.WriteLine("Running service '{0}' (press any key to stop)", service.DisplayName);
             service.Start();

             Console.ReadKey();
             Console.WriteLine("Stopping service '{0}' (press any key to stop)", service.DisplayName);
             service.Stop();
             System.Threading.Thread.Sleep(2000);
         }

         public bool VisualStudioIsRunning
         {
             get
             {
                 return (Process.GetCurrentProcess()
                             .ProcessName
                             .ToUpperInvariant()
                             .EndsWith(".VSHOST", StringComparison.InvariantCultureIgnoreCase)
                         || Debugger.IsAttached);
             }
         }
    }
}