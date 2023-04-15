using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public partial class TaxPayer
  {
    public float YearlySalary { get; set; }
    public List<Deduction> YearlyDeductions { get; set; }
  }
}
