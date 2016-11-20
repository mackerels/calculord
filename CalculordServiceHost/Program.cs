using CalculordServiceLib;
using System;
using System.ServiceModel;

namespace CalculordServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculordService)))
            {
                serviceHost.Open();
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }
    }
}
