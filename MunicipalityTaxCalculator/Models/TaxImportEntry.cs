using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityTaxCalculator.Models
{
    class TaxImportEntry
    {
        public string Municipality { get; set; }
        public TaxTypes TaxType { get; set; }
        public DateTime StartDate { get; set; }
        public float TaxRate { get; set; }
    }
}
