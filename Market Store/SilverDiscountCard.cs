using System;

namespace MarketStore
{
    class SilverDiscountCard : DiscountCard, IDiscountCard
    {
        private const decimal InitialDiscountRate = 0.02m;
        private const decimal FirstDiscountRateAddition = 0.035m;

        private const decimal FirstTurnoverCriteria = 300m;
        public SilverDiscountCard(Person cardHolder) 
            : base(cardHolder)
        {
        }

        protected override decimal DiscountRate
        {
            get
            {
                if (this.PreviousMonthTurnover < FirstTurnoverCriteria)
                {
                    return InitialDiscountRate;
                }

                return FirstDiscountRateAddition;
            }
        }
    }
}
