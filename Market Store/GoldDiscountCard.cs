using System;

namespace MarketStore
{
    class GoldDiscountCard : DiscountCard, IDiscountCard
    {
        private const decimal InitialDiscountRate = 0.02m;
        private const decimal DiscountRateGrow = 100;
        private const decimal MaxDiscountRate = 0.1m;
        public GoldDiscountCard(Person cardHolder) 
            : base(cardHolder)
        {
        }

        protected override decimal DiscountRate
            => Math.Max(InitialDiscountRate, Math.Min(Math.Floor(this.PreviousMonthTurnover / DiscountRateGrow) / 100m, MaxDiscountRate));
    }
}
