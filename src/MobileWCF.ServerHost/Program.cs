using System;
using System.ServiceModel;

namespace MobileWCF.ServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] addressBase = new Uri[] { new Uri("http://192.168.106.164:9003/CalculatorService") };
            var host = new ServiceHost(typeof(CalculatorService), addressBase);
            host.Open();
            Console.Read();
        }
    }
}
