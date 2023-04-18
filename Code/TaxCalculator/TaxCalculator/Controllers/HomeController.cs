using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Xml.Linq;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult BasicTaxResults(int age = 0, int yearlySalary = 0) 
        {

            TaxPayer taxPayer123 = new TaxPayer
            {
                Age = age,
                YearlySalary = (float)yearlySalary,
                YearlyDeductions = new List<Deduction>()
            };

            int ageGroup = 3;

            ViewData["PITAmount"] = "R" + CalculateTax(taxPayer123.YearlySalary, ageGroup);
            
            return View();
        }

        public double CalculateTax(double totalIncome, int ageGroup)
        {
            double taxableIncome;
            //Replace this switch with model values for Tax Thresholds for Age groups
            switch (ageGroup)
            {
                case 1:
                    taxableIncome = totalIncome - 148217;
                    break;

                case 2:
                    taxableIncome = totalIncome - 165689;
                    break;

                default:
                    taxableIncome = totalIncome - 95750;
                    break;
            }
            if (taxableIncome <= 0)
            {
                return 0;
            }
            //Replace this with actual tax calculations based on model values
            return taxableIncome * 18 / 100;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}