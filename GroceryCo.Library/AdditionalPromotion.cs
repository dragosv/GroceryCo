using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class AdditionalPromotion : IPromotion
    {
        public string Category { get; set; }
        
        public int Percent { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is AdditionalPromotion additionalPromotion)
            {
                return Equals(additionalPromotion);
            }
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Category, Percent);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var count = order.Items.Count(x => x.Product.Categories.Contains(Category));

            var groups = count / 2;

            if (groups == 0)
            {
                return new List<Discount>();
            }

            var items = order.Items
                .Where(x => x.Product.Categories.Contains(Category))
                .OrderByDescending(x => x.Product.Price)
                .Take(groups * 2)
                .ToList();

            var discounts = new List<Discount>();
            
            for (int i = 0; i < items.Count; i++)
            {
                if (i % 2 == 1)
                {
                    discounts.Add(new Discount { Promotion = this, Item = items[i], Value = Percent * items[i].Product.Price / 100 });
                }
            }

            return discounts;
        }

        private bool Equals(AdditionalPromotion other)
        {
            return Category == other.Category && Percent == other.Percent;
        }
    }
}