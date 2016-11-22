using System;
using System.ServiceModel;
using CalculordServiceLib;

namespace CalculordServiceHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var serviceHost = new ServiceHost(typeof(CalculordService)))
            {
                serviceHost.Open();
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }
    }
}