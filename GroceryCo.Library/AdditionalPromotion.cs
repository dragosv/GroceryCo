namespace GroceryCo.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdditionalPromotion : IPromotion
    {
        public string Category { get; set; }

        public int Percent { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AdditionalPromotion additionalPromotion ? this.Equals(additionalPromotion) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Category, this.Percent);
        }

        public IList<Discount> ApplyTo(Order order)
        {
            var count = order.Items.Count(x => x.Product.Categories.Contains(this.Category));

            var groups = count / 2;

            if (groups == 0)
            {
                return new List<Discount>();
            }

            var items = order.Items
                .Where(x => x.Product.Categories.Contains(this.Category))
                .OrderByDescending(x => x.Product.Price)
                .Take(groups * 2)
                .ToList();

            var discounts = new List<Discount>();

            for (int i = 0; i < items.Count; i++)
            {
                if (i % 2 == 1)
                {
                    discounts.Add(new Discount { Promotion = this, Item = items[i], Value = this.Percent * items[i].Product.Price / 100 });
                }
            }

            return discounts;
        }

        private bool Equals(AdditionalPromotion other)
        {
            return this.Category == other.Category && this.Percent == other.Percent;
        }
    }
}