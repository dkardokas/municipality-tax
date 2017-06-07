using MunicipalityTaxCalculator.Models;
using MunicipalityTaxCalculator.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MunicipalityTaxCalculator.Controllers
{
    public class MunicipalitiesController : ApiController
    {
        MunicipalityDataProvider _provider;
        public MunicipalitiesController()
        {
            _provider = new MunicipalityDataProvider();
        }

        public int Get(string title)
        {
            return _provider.GetByTitle(title).MunicipalityID;
        }


    }
}
