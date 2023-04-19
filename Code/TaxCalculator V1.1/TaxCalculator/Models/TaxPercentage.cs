using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public class TaxPercentage
  {
    public int ID { get; set; }
    public decimal SalaryThreshold { get; set; }
    public decimal TaxPercent { get; set; }
    public TaxPercentage()
    {

    }
  }
}
