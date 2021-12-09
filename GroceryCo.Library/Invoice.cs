namespace GroceryCo.Library
{
    using System.Collections.Generic;
    using System.Linq;

    public class Invoice
    {
        public Invoice(IList<Item> items, IList<Discount> discounts)
        {
            this.Items = items;
            this.Discounts = discounts;
        }

        public IList<Item> Items { get; }

        public IList<Discount> Discounts { get; }

        public double Total => this.Items.Sum(x => x.Product.Price) - this.Discounts.Sum(x => x.Value);
    }
}