using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class OnSalePromotion : IPromotion
    {
        public Product Product { get; set; }
        
        public double SalePrice { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is OnSalePromotion onSalePromotion)
            {
                return Equals(onSalePromotion);
            }
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, SalePrice);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var items = order.Items.Where(x => x.Product == Product);

            return items.Select(x => new Discount { Item = x, Promotion = this, Value = Product.Price - SalePrice}).ToList();
        }
        
        private bool Equals(OnSalePromotion other)
        {
            return Equals(Product, other.Product) && SalePrice.Equals(other.SalePrice);
        }
    }
}