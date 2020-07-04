using System;

namespace MarketStore
{
    class BronzeDiscountCard : DiscountCard, IDiscountCard
    {
        private const decimal InitialDiscountRate = 0m;
        private const decimal FirstDiscountRateAddition = 0.01m;
        private const decimal SecondDiscountRateAddition = 0.025m;

        private const decimal TurnoverFirstCriteria = 100;
        private const decimal TurnoverSecondCriteria = 300;

        public BronzeDiscountCard(Person cardHolder)
            : base(cardHolder)
        {
        }

        protected override decimal DiscountRate
        {
            get
            {
                if (this.PreviousMonthTurnover < TurnoverFirstCriteria)
                {
                    return InitialDiscountRate;
                }

                if (this.PreviousMonthTurnover <= TurnoverSecondCriteria)
                {
                    return FirstDiscountRateAddition;
                }

                return SecondDiscountRateAddition;
            }
        }
    }
}
