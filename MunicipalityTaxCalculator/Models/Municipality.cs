using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityTaxCalculator.Models
{
    public class Municipality
    {
        public int MunicipalityID { get; set; }
        public string Title { get; set; }
        public ICollection<TaxEntry> TaxEntries { get; set; }
    }
}
