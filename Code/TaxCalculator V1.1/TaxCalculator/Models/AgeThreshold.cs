using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public class AgeThreshold
  {
    public int ID { get; set; } = -1;
    public int Age { get; set; } = -1;
    public decimal MinimumYearlySalary { get; set; } = 0;
    public AgeThreshold()
    {

    }
  }
}
