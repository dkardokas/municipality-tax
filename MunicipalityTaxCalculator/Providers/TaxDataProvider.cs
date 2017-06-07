using MunicipalityTaxCalculator.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MunicipalityTaxCalculator.Providers
{

    public class TaxDataProvider
    {
        TaxContext _ctx;
        public TaxDataProvider()
        {
            _ctx = new TaxContext();
        }

        public TaxDataProvider(TaxContext context)
        {
            _ctx = context;
        }

        public TaxEntry GetByID(int id)
        {
            return _ctx.TaxEntries.Where(te => te.TaxEntryID == id).FirstOrDefault();
        }

        public TaxEntry GetYearlyTax(Municipality municipalityObj, DateTime taxDate)
        {
            return _ctx.TaxEntries.Where(te => te.StartDate.Year == taxDate.Year && te.TaxType == TaxTypes.Yearly).FirstOrDefault();
        }

        public TaxEntry GetMonthlyTax(Municipality municipalityObj, DateTime taxDate)
        {
            return _ctx.TaxEntries.Where(te => te.StartDate.Month == taxDate.Month && te.TaxType == TaxTypes.Monthly).FirstOrDefault();
        }
        
        public TaxEntry GetWeeklyTax(Municipality municipalityObj, DateTime taxDate)
        {
            while(taxDate.DayOfWeek != DayOfWeek.Monday)
            {
                taxDate = taxDate.AddDays(-1);
            }

            return _ctx.TaxEntries.Where(te => te.StartDate.Day == taxDate.Day && te.StartDate.Month == taxDate.Month && te.StartDate.Year == taxDate.Year && te.TaxType == TaxTypes.Weekly).FirstOrDefault();
        }

        public TaxEntry GetDailyTax(Municipality municipalityObj, DateTime taxDate)
        {
            return _ctx.TaxEntries.Where(te => te.StartDate.Day == taxDate.Day && te.StartDate.Month == taxDate.Month && te.StartDate.Year == taxDate.Year && te.TaxType == TaxTypes.Daily).FirstOrDefault();
        }

        internal void Insert(TaxEntry newEntry)
        {
            var oldEntry = _ctx.TaxEntries.Where(te => te.TaxType == newEntry.TaxType && te.StartDate == newEntry.StartDate).FirstOrDefault();
            if(oldEntry != null)
            {
                _ctx.TaxEntries.Remove(oldEntry);
                _ctx.SaveChanges();
            }
            
            _ctx.TaxEntries.Add(newEntry);
            _ctx.Entry(newEntry.Municipality).State = EntityState.Unchanged; //Municipality is guaranteed to already exist
            _ctx.SaveChanges();
        }

        internal IQueryable<TaxEntry> GetAll()
        {
            return _ctx.TaxEntries.Distinct();
        }
    }
}
