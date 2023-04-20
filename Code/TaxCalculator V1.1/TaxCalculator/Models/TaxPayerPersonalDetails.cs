using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
  public partial class TaxPayer
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Surname { get; set; }
    public TaxPayer()
    {

    }
  }
}
