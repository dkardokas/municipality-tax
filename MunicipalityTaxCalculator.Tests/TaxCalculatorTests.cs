using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using MunicipalityTaxCalculator.Models;
using System.Linq;
using System.Data.Entity;
using MunicipalityTaxCalculator.Providers;
using MunicipalityTaxCalculator.Service;

namespace MunicipalityTaxCalculator.Tests
{
    [TestClass]
    public class TaxCalculatorTests
    {
        [TestMethod]
        public void DailyTaxOverridesMonthly()
        {
            var municipalityData = new List<Municipality>
            {
                new Municipality { Title = "Test Municipality" }
            }.AsQueryable();

            var taxEntryData = new List<TaxEntry>
            {
                new TaxEntry { Municipality = municipalityData.First(), StartDate = new DateTime(2017, 1, 1), TaxRate = 0.11F, TaxType = TaxTypes.Monthly},
                new TaxEntry { Municipality = municipalityData.First(), StartDate = new DateTime(2017, 1, 10), TaxRate = 0.22F, TaxType = TaxTypes.Daily},
            }.AsQueryable();

            var mockTaxEntrySet = new Mock<DbSet<TaxEntry>>();

            mockTaxEntrySet.As<IQueryable<TaxEntry>>().Setup(m => m.Provider).Returns(taxEntryData.Provider);
            mockTaxEntrySet.As<IQueryable<TaxEntry>>().Setup(m => m.Expression).Returns(taxEntryData.Expression);
            mockTaxEntrySet.As<IQueryable<TaxEntry>>().Setup(m => m.ElementType).Returns(taxEntryData.ElementType);


            var mockMunicipalitySet = new Mock<DbSet<Municipality>>();

            mockMunicipalitySet.As<IQueryable<TaxEntry>>().Setup(m => m.Provider).Returns(municipalityData.Provider);
            mockMunicipalitySet.As<IQueryable<TaxEntry>>().Setup(m => m.Expression).Returns(municipalityData.Expression);
            mockMunicipalitySet.As<IQueryable<TaxEntry>>().Setup(m => m.ElementType).Returns(municipalityData.ElementType);

            var mockContext = new Mock<TaxContext>();

            mockContext.Setup(c => c.TaxEntries).Returns(mockTaxEntrySet.Object);
            mockContext.Setup(c => c.Municipalities).Returns(mockMunicipalitySet.Object);

            TaxDataProvider tdp = new TaxDataProvider(mockContext.Object);
            MunicipalityDataProvider mdp = new MunicipalityDataProvider(mockContext.Object);
            var serviceUnderTest = new TaxCalculator(tdp, mdp);

            var calculatedResult1 = serviceUnderTest.CalculateTaxForDate("Test Municipality", new DateTime(2017, 1, 10));
            var calculatedResult2 = serviceUnderTest.CalculateTaxForDate("Test Municipality", new DateTime(2017, 1, 11));


            Assert.AreEqual(0.22F, calculatedResult1);
            Assert.AreEqual(0.11F, calculatedResult2);

        }
    }
}
