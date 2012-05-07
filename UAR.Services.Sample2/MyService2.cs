using System;
using System.Linq;
using System.Threading;

using UAR.Services.Contracts;

namespace UAR.Services.Sample2
{
    public class MyService2 : IAmService
    {
        public string DisplayName
        {
            get
            {
                return "MyService2";
            }
        }

        public string Description
        {
            get
            {
                return "Sample service from Uli Armbruster";
            }
        }

        public string ServiceName
        {
            get
            {
                return "UAR.Services.Sample2.MyService2";
            }
        }

        bool _stop;
        public void Start()
        {
            Console.WriteLine("Hello world!");
            Console.WriteLine("Start calculating");

            var thread = new Thread(Calculate);
            thread.Start();
        }

        public void Stop()
        {
            _stop = true;
            Console.WriteLine("Goodbye world!");
        }

        void Calculate()
        {
            while (!_stop)
            {
                System.Diagnostics.Debug.WriteLine("MyService2: calculate...");
                Thread.Sleep(1000);
            }
        }
    }
}
