using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Xml.Linq;
using TaxCalculator.Models;
using TaxCalculator.PubSub;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      ViewBag.Deductables = PopulateDeductableDropDown();
      return View();
    }

    public IActionResult BasicTaxResults(int age = 0, decimal yearlySalary = 0)
    {

      user.Age = age;
      user.YearlySalary = yearlySalary;

      int ageGroup = 3;

      ViewData["PITAmount"] = "R" + CalculateTax(user);

      return View();
    }

    public void PublishDeduction(string deductionType, decimal deductionValue)
    {
      deductionPublisher.Publish(deductionType, deductionValue);
    }

    //to be moved to model potentially
    public decimal CalculateTax(TaxPayer details)
    {
      AgeThreshold taxFree = DBController.getTaxFree(details.Age);
      decimal taxableIncome = details.YearlySalary - taxFree.MinimumYearlySalary;
      //Replace this switch with model values for Tax Thresholds for Age groups
      if (taxableIncome <= 0)
      {
        return 0;
      }

      Dictionary<string, Deduction> deductions = DBController.getDeductions();

      decimal taxDeductions = 0;

      foreach (KeyValuePair<string, Deduction> item in deductions)
      {
        decimal d = 0;
        if (details.YearlyDeductions.TryGetValue(item.Key, out d))
        {
          decimal deductionTotal = d * item.Value.DeductionRate;
          if (item.Value.DeductionAmountMax < 0)
          {
            // No limit on amount

            if (item.Value.DeductionRateMax < 0)
            {
              // No limit on rate
              taxDeductions += deductionTotal;
            }
            else
            {
              taxDeductions += Math.Min(deductionTotal, (item.Value.DeductionRateMax * taxableIncome));
            }
          }
          else
          {
            // Limit on amount

            if (item.Value.DeductionRateMax < 0)
            {
              // No limit on rate
              taxDeductions += Math.Min(deductionTotal, item.Value.DeductionAmountMax);
            }
            else
            {
              // Limit on both
              taxDeductions = Math.Min(Math.Min(deductionTotal, (item.Value.DeductionRateMax * taxableIncome)), item.Value.DeductionAmountMax);
            }
          }
        }
      }

      taxableIncome -= taxDeductions;

      List<TaxPercentage> taxRates = DBController.getTaxRates(taxableIncome);

      decimal totalTax = 0;
      decimal prevThreshold = 0;

      foreach (TaxPercentage tp in taxRates)
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

    private SelectList PopulateDeductableDropDown()
    {
      List<string> deductables = new List<string>()
      {
        "Donations",
        "Medical Aid",
        "Retirement Fund"
      };

      return new SelectList(deductables);
    }
  }
}