using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculator.Models
{
    public class HomeViewModel
    {
        public int Age { get; set; }
        public decimal YearlySalary { get; set; }
        public List<SelectListItem> DeductionSelectList { get; set; }

        public decimal DonationDeduction { get; set; }
        public decimal RetirementDeduction { get; set; }

        public decimal DeductionAmount { get; set; }
    }
}