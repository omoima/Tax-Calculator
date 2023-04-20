using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{

    public delegate void DeductionDelegate(Deduction deduction);

    public interface IPublisher
    {
        void Subscribe(DeductionDelegate deductionDelegate);
    }

    public interface ISubsciber
    {
        void Display(Deduction deduction);
    }
    public class Publisher : IPublisher
    {
        private event DeductionDelegate _deductionEvent;

        public void Subscribe(DeductionDelegate deductionHandler)
        {
            _deductionEvent += deductionHandler;
        }

        public void Publish(Deduction deduction)
        {
            if(_deductionEvent != null)
            {
                _deductionEvent(deduction);
            }
        }

    }

    public class DisplaySubscriber : ISubsciber
    {
        public void Display(Deduction deduction)
        {
            Console.WriteLine("Subscriber has received a deduction");
        }

    }
}
