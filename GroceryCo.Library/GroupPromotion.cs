namespace GroceryCo.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupPromotion : IPromotion
    {
        public Product Product { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is GroupPromotion groupPromotion ? this.Equals(groupPromotion) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Product, this.Price, this.Quantity);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var items = order.Items.Where(x => x.Product == this.Product).ToList();

            var groups = items.Count / this.Quantity;

            return items.Take(groups * this.Quantity)
                .Select(x => new Discount { Item = x, Promotion = this, Value = this.Product.Price - (this.Price / this.Quantity) }).ToList();
        }

        private bool Equals(GroupPromotion other)
        {
            return Equals(this.Product, other.Product) && this.Price.Equals(other.Price) && this.Quantity == other.Quantity;
        }
    }
}