using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Xml.Linq;
using TaxCalculator.Models;
using TaxCalculator.PubSub;

namespace TaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private Publisher deductionPublisher;
        private TaxPayer user;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            deductionPublisher = new Publisher();
            user = new TaxPayer
            {
              Age = 0,
              YearlySalary = 0,
              YearlyDeductions = new Dictionary<string, decimal>()
            };
            deductionPublisher.Subscribe(user.HandleDeduction);
    }

        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult BasicTaxResults(int age = 0, decimal yearlySalary = 0) 
        {

            user.Age = age;
            user.YearlySalary = yearlySalary;

            int ageGroup = 3;

            ViewData["PITAmount"] = "R" + CalculateTax(user.YearlySalary, ageGroup);
            
            return View();
        }

        public void PublishDeduction(string deductionType, decimal deductionValue) { 
            deductionPublisher.Publish(deductionType, deductionValue);
        }

        //to be moved to model potentially
        public decimal CalculateTax(decimal totalIncome, int ageGroup)
        {
            AgeThreshold taxFree = DBController.getTaxFree(ageGroup);
            decimal taxableIncome = totalIncome - taxFree.MinimumYearlySalary;
            //Replace this switch with model values for Tax Thresholds for Age groups
            if (taxableIncome <= 0)
            {
                return 0;
            }

            List < TaxPercentage > taxRates = DBController.getTaxRates(taxableIncome);

            decimal totalTax = 0;
            decimal prevThreshold = 0;

            foreach (TaxPercentage tp in taxRates )
            {
                if (taxableIncome > (tp.SalaryThreshold - prevThreshold))
                {
                    totalTax += tp.TaxPercent * (tp.SalaryThreshold - prevThreshold);
                    taxableIncome -= (tp.SalaryThreshold - prevThreshold);
                    prevThreshold = tp.SalaryThreshold;
                }
                else
                {
                    totalTax += taxableIncome * tp.TaxPercent;
                }
            }

            return totalTax;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}