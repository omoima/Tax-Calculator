using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Controllers
{
  public class PersonalController : Controller
  {
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult BasicTax(double totalIncome, int ageGroup)
    {
      ViewData["totalIncome"] = totalIncome;
      ViewData["ageGroup"] = ageGroup;
      ViewData["tax"] = CalculateTax(totalIncome, ageGroup);
        
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
      return taxableIncome*18/100;
    }
  }
}
