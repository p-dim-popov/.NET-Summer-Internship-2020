using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketStore
{
    abstract class DiscountCard : IDiscountCard
    {
        private Person _cardHolder;
        private readonly IList<Purchase> _purchases;

        protected DiscountCard(Person cardHolder)
        {
            this._purchases = new List<Purchase>();
            this.CardHolder = cardHolder;
        }

        protected abstract decimal DiscountRate { get; }

        protected decimal PreviousMonthTurnover
            => this._purchases
                .Where(p => p.Date.Year == DateTime.Now.Year && p.Date.Month == DateTime.Now.Month - 1)
                .Sum(p => p.FinalPrice);

        private Person CardHolder
        {
            get => this._cardHolder;
            set => this._cardHolder = value ?? throw new ArgumentNullException($"CardHolder", "Card holder cannot be null!");
        }

        public decimal CalculateDiscount(decimal valueOfPurchase)
            => valueOfPurchase * this.DiscountRate;

        public Purchase CreatePurchase(decimal valueOfPurchase)
        {
            if (valueOfPurchase <= 0)
            {
                throw new ArgumentException("Value cannot be below or equal to zero");
            }

            var purchase = new Purchase(this.CardHolder, valueOfPurchase, CalculateDiscount(valueOfPurchase), DateTime.Now);
            this._purchases.Add(purchase);
            return purchase;
        }

        public Purchase LastPurchase { get; private set; }

        public bool LoadLastPurchase()
        {
            if (!this._purchases.Any())
            {
                return false;
            }

            this.LastPurchase = this._purchases.LastOrDefault();
            if (this.LastPurchase is null)
            {
                return false;
            }

            return true;
        }

        public decimal? LastPrice()
            => this.LastPurchase?.Price;

        public decimal? LastFinalPrice()
            => this.LastPurchase?.FinalPrice;

        public decimal? LastDiscount()
            => this.LastPurchase?.Discount;

        public decimal? LastDiscountRatePercent()
            => this.LastPurchase?.DiscountRatePercent;

    }
}
