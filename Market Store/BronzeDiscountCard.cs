using System;

namespace MarketStore
{
    class BronzeDiscountCard : DiscountCard, IDiscountCard
    {
        private const decimal InitialDiscountRate = 0m;
        private const decimal SecondStageDiscountRate = 0.01m;
        private const decimal ThirdStageDiscountRate = 0.025m;

        private const decimal FirstTurnoverCriteria = 100;
        private const decimal SecondTurnoverCriteria = 300;

        public BronzeDiscountCard(Person cardHolder)
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

                if (this.PreviousMonthTurnover <= SecondTurnoverCriteria)
                {
                    return SecondStageDiscountRate;
                }

                return ThirdStageDiscountRate;
            }
        }
    }
}
