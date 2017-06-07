using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityTaxCalculator
{
    partial class SelfHostService : ServiceBase
    {
        string baseAddress = "http://localhost:9000/";
        public SelfHostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WebApp.Start<SelfHostConfig>(url: baseAddress);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
