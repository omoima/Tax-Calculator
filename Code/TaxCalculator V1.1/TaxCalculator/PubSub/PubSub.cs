using TaxCalculator.Models;

namespace TaxCalculator.PubSub
{

    public delegate void DeductionDelegate(string deductionType, decimal deductionValue);

    public interface IPublisher
    {
        void Subscribe(DeductionDelegate deductionDelegate);
    }

    public interface ISubsciber
    {
        void HandleDeduction(string deductionType, decimal deductionValue);
    }
    public class Publisher : IPublisher
    {
        private event DeductionDelegate _deductionEvent;

        public void Subscribe(DeductionDelegate deductionHandler)
        {
            _deductionEvent += deductionHandler;
        }

        public void Publish(string deductionType, decimal deductionValue)
        {
            if(_deductionEvent != null)
            {
                _deductionEvent(deductionType, deductionValue);
            }
        }

    }
}
