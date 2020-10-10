using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class AdditionalPromotion : IPromotion
    {
        public string Category { get; set; }
        
        public int Percent { get; set; }

        public IList<ProductGroup> ApplyTo(Order order)
        {
            var count = order.Products.Count(x => x.Categories.Contains(Category));

            var groups = count / 2;

            if (groups == 0)
            {
                return new List<ProductGroup>();
            }

            var products = order.Products
                .Where(x => x.Categories.Contains(Category))
                .OrderByDescending(x => x.Price)
                .Take(groups * 2)
                .ToList();

            return products.GroupBy(x => x)
                .Select(group => new ProductGroup(group.Key, group.Count()))
                .ToList();
        }

        public double Discount(Order order)
        {
            var count = order.Products.Count(x => x.Categories.Contains(Category));

            var groups = count / 2;

            if (groups == 0)
            {
                return 0;
            }

            var products = order.Products
                .Where(x => x.Categories.Contains(Category))
                .OrderByDescending(x => x.Price)
                .Take(groups * 2)
                .ToList();

            var discountedProducts = new List<Product>();
            
            for (int i = 0; i < products.Count; i++)
            {
                if (i % 2 == 1)
                {
                    discountedProducts.Add(products[i]);
                }
            }
            
            var totalPrices = discountedProducts.Sum(x => x.Price);

            return totalPrices * Percent / 100;
        }
    }
}