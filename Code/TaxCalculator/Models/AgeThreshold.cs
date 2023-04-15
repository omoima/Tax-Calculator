using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public class AgeThreshold
  {
    public string ID { get; set; }
    public int Age { get; set; }
    public int MinimumYearlySalary { get; set; }
    public AgeThreshold()
    {

    }
  }
}
