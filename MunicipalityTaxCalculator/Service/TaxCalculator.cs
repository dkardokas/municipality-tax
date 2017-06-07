using MunicipalityTaxCalculator.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityTaxCalculator.Service
{
    public class TaxCalculator
    {
        TaxDataProvider _tdp;
        MunicipalityDataProvider _mdp;
        public TaxCalculator(TaxDataProvider tdp, MunicipalityDataProvider mdp)
        {
            _tdp = tdp;
            _mdp = mdp;
        }

        public float CalculateTaxForDate(string municipality, DateTime taxDate)
        {
            var municipalityObj = _mdp.GetByTitle(municipality);
            var taxEntry = _tdp.GetDailyTax(municipalityObj, taxDate);
            if (taxEntry != null)
            {
                return taxEntry.TaxRate;
            }

            taxEntry = _tdp.GetWeeklyTax(municipalityObj, taxDate);
            if (taxEntry != null)
            {
                return taxEntry.TaxRate;
            }

            taxEntry = _tdp.GetMonthlyTax(municipalityObj, taxDate);
            if (taxEntry != null)
            {
                return taxEntry.TaxRate;
            }

            taxEntry = _tdp.GetYearlyTax(municipalityObj, taxDate);
            if (taxEntry != null)
            {
                return taxEntry.TaxRate;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
