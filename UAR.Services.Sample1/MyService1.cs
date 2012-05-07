using System;
using System.Linq;
using System.Threading;

using UAR.Services.Contracts;

namespace UAR.Services.Sample1
{
    public class MyService1 : IAmService
    {
        public string DisplayName
        {
            get
            {
                return "MyService1";
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
                return "UAR.Services.Sample1.MyService1";
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
                Console.WriteLine("calculate...");
                Thread.Sleep(1000);
            }
        }
    }
}
