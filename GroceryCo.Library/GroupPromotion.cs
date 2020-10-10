using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class GroupPromotion : IPromotion
    {
        public Product Product { get; set; }
        
        public double Price { get; set; }
        
        public int Quantity { get; set; }
        
        public override bool Equals(object? obj)
        {
            if (obj is GroupPromotion groupPromotion)
            {
                return Equals(groupPromotion);
            }
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Price, Quantity);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var items = order.Items.Where(x => x.Product == Product).ToList();

            var groups = items.Count / Quantity;
            
            return items.Take(groups * Quantity)
                .Select(x => new Discount { Item = x, Promotion = this, Value = Product.Price - Price / Quantity}).ToList();
        }
        
        private bool Equals(GroupPromotion other)
        {
            return Equals(Product, other.Product) && Price.Equals(other.Price) && Quantity == other.Quantity;
        }
    }
}