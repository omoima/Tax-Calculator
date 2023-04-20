using TaxCalculator.PubSub;
using TaxCalculator.Controllers;
using TaxCalculator.Models;

namespace Tests
{
    [TestClass]
    public class PublisherSubscriberTest
    {
        [TestMethod]
        public void CheckEventAddedToList()
        {
            HomeController hm = new HomeController();

            hm.PublishDeduction("Testing Deduction", 100);

            Assert.IsTrue(hm.getDeductionsLength() > 0);
        }
    }
}
