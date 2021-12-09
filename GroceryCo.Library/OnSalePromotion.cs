namespace GroceryCo.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OnSalePromotion : IPromotion
    {
        public Product Product { get; set; }

        public double SalePrice { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OnSalePromotion onSalePromotion ? this.Equals(onSalePromotion) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Product, this.SalePrice);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var items = order.Items.Where(x => x.Product == this.Product);

            return items.Select(x => new Discount { Item = x, Promotion = this, Value = this.Product.Price - this.SalePrice }).ToList();
        }

        private bool Equals(OnSalePromotion other)
        {
            return Equals(this.Product, other.Product) && this.SalePrice.Equals(other.SalePrice);
        }
    }
}