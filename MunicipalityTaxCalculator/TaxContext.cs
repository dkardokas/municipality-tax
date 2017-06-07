using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalityTaxCalculator.Models;

namespace MunicipalityTaxCalculator
{
    public class TaxContext : DbContext
    {
        public TaxContext() : base()
        {
        }

        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<TaxEntry> TaxEntries { get; set; }
    }
}
