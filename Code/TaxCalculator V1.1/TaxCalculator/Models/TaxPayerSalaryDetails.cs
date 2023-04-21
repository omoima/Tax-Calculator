using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.PubSub;

namespace TaxCalculator.Models
{
  public partial class TaxPayer : ISubsciber
  {
    public decimal YearlySalary { get; set; }
    public Dictionary<string, decimal> YearlyDeductions { get; set; }

    public void HandleDeduction(string name, decimal amount) {
      if(amount > 0)
      { 
      YearlyDeductions.Add(name, amount);
      }
    }
  }
}
