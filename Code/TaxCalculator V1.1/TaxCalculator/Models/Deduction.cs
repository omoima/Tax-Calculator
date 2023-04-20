using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public class Deduction
  {
    public int DeductionID { get; set; }
    public string DeductionDescription { get; set; }
    public decimal DeductionRateMax { get; set; }
    public decimal DeductionAmountMax { get; set; }
    public decimal DeductionRate { get; set; }
    public Deduction()
    {

    }
  }
}
