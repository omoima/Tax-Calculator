using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public class Deduction
  {
    public int ID { get; set; }
    public string DeductionDescription { get; set; }
    public float DeductionRate { get; set; }
    public int DeductionMax { get; set; }
    public Deduction()
    {

    }
  }
}
