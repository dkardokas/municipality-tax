using MunicipalityTaxCalculator.Models;
using MunicipalityTaxCalculator.Providers;
using MunicipalityTaxCalculator.Service;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MunicipalityTaxCalculator.Controllers
{
    public class TaxesController : ApiController
    {
        TaxDataProvider _taxProvider;
        MunicipalityDataProvider _mdp;
        const string IMPORT_PATH = "C:\\Import_Files";
        private MunicipalityDataProvider _municipalityProvider
        {
            get
            {
                if (_mdp == null) { _mdp = new MunicipalityDataProvider(); }
                return _mdp;
            }
        }
        public TaxesController()
        {
            _taxProvider = new TaxDataProvider();
        }

        public float Get(string municipality, DateTime taxDate)
        {
            var taxCalculator = new TaxCalculator(_taxProvider, _municipalityProvider);
            return taxCalculator.CalculateTaxForDate(municipality, taxDate);
        }

        public void Post(string municipality, TaxTypes taxType, DateTime startDate, float taxRate)
        {

            var municipalityObj = _municipalityProvider.GetByTitle(municipality);
            InsertNewTaxEntry(municipalityObj, taxType, startDate, taxRate);
        }

        public void Post(int municipalityID, TaxTypes taxType, DateTime startDate, float taxRate)
        {
            var municipalityObj = _municipalityProvider.GetByID(municipalityID);
            InsertNewTaxEntry(municipalityObj, taxType, startDate, taxRate);
        }

        [HttpGet()]
        public void ImportFiles()
        {
            if (Directory.Exists(IMPORT_PATH))
            {
                string[] fileEntries = Directory.GetFiles(IMPORT_PATH);
                foreach (string fileName in fileEntries)
                    ImportTaxFile(fileName);
            }
        }
        

        [HttpGet()]
        private void ExportToFile()
        {
            var allEntries = _taxProvider.GetAll();
            var stringOutput = "";

            foreach (var taxEntry in allEntries)
            {
                stringOutput += new JavaScriptSerializer().Serialize(taxEntry);
            }
           
            File.WriteAllText(IMPORT_PATH + "\\export.json", stringOutput);

        }

        private void InsertNewTaxEntry(Municipality municipality, TaxTypes taxType, DateTime startDate, float taxRate)
        {
            var newEntry = new TaxEntry { Municipality = municipality, TaxType = taxType, StartDate = startDate, TaxRate = taxRate };
            _taxProvider.Insert(newEntry);
        }

        private void ImportTaxFile(string fileName)
        {
            var contents = File.ReadAllText(fileName);
            var importData = new JavaScriptSerializer().Deserialize<Collection<TaxImportEntry>>(contents);
            foreach (var importEntry in importData)
            {
                var municipalityObj = _municipalityProvider.GetByTitle(importEntry.Municipality);
                var newTaxEntry = new TaxEntry
                {
                    Municipality = municipalityObj,
                    StartDate = importEntry.StartDate,
                    TaxRate = importEntry.TaxRate,
                    TaxType = importEntry.TaxType
                };
                _taxProvider.Insert(newTaxEntry);
            }
        }
    }
}
