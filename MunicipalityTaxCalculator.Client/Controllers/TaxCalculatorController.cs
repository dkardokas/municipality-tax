using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MunicipalityTaxCalculator.Client.Controllers
{
    public class TaxCalculatorController : ApiController
    {
        static HttpClient _client;
        // GET: api/TaxCalculator
        public TaxCalculatorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:9000/");
        }
        public float Get(string municipality, DateTime taxDate)
        {
            string queryString = "api/taxes?";
            queryString += String.Format("municipality={0}&taxDate={1}", municipality, taxDate.ToString("yyyy-MM-dd"));
            HttpResponseMessage response = _client.GetAsync(queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<float>().Result;
            }
            else
            {
                throw new InvalidOperationException("Invalid data");
            }

        }


    }
}
