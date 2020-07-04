using System;

namespace MarketStore
{
    class Purchase
    {
        public Purchase(Person client, decimal price, decimal discount)
        {
            this.Date = DateTime.Now;
            this.Client = client;
            this.Price = price;
            this.Discount = discount;
        }

        public Purchase(Person client, decimal price, decimal discount, DateTime date)
        {
            this.Date = date;
            this.Client = client;
            this.Price = price;
            this.Discount = discount;
        }

        public DateTime Date { get; }
        public Person Client { get; }
        public decimal Price { get; }
        public decimal Discount { get; }

        public decimal DiscountRatePercent => (1 - this.FinalPrice / this.Price) * 100m;
        public decimal FinalPrice => this.Price - this.Discount;
    }
}
