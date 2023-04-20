using TaxCalculator.Controllers;
using TaxCalculator.Models;


namespace Tests
{
    [TestClass]
    public class CalculateTaxTests
    {
        [TestMethod]
        public void CalculateTaxTest_0()
        {
       
            var HomeController = new HomeController();
            TaxPayer taxPayer = new()
            {
                YearlySalary = 0,
                YearlyDeductions = new Dictionary<string, decimal>()
            };

            Assert.AreEqual(0, HomeController.CalculateTax(taxPayer));
        }

        [TestMethod]
        public void CalculateTaxTest_250k()
        {
            var HomeController = new HomeController();
            TaxPayer taxPayer = new()
            {
                YearlySalary = 250000,
                YearlyDeductions = new Dictionary<string, decimal>()
            };

            Assert.AreEqual((decimal)27765, HomeController.CalculateTax(taxPayer));
        }
    }
}
