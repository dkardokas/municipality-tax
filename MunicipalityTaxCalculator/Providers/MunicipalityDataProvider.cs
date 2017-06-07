using MunicipalityTaxCalculator.Models;
using System.Linq;

namespace MunicipalityTaxCalculator.Providers
{

    public class MunicipalityDataProvider
    {
        TaxContext _ctx;
        public MunicipalityDataProvider()
        {
            _ctx = new TaxContext();
        }

        public MunicipalityDataProvider(TaxContext context)
        {
            _ctx = context;
        }

        public Municipality GetByTitle(string title)
        {
            var returnVal = _ctx.Municipalities.Where(te => te.Title.ToLower() == title.ToLower()).FirstOrDefault();
            if (returnVal != null)
            {
                return returnVal;
            }
            else
            {
                Municipality newMunicipality = new Municipality { Title = title };
                _ctx.Municipalities.Add(newMunicipality);
                _ctx.SaveChanges();

                return newMunicipality;
            }
        }

        public Municipality GetByID(int Id)
        {
            return _ctx.Municipalities.Where(te => te.MunicipalityID == Id).FirstOrDefault();
        }


    }
}
