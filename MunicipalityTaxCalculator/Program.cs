using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.ServiceProcess;

namespace MunicipalityTaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SelfHostService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
