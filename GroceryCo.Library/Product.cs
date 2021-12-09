namespace GroceryCo.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Product
    {
        public Product()
        {
            this.Categories = new List<string>();
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public IList<string> Categories { get; }

        public override bool Equals(object? obj)
        {
            return obj is Product product ? this.Equals(product) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Price, this.Categories);
        }

        private bool Equals(Product other)
        {
            return this.Name == other.Name && this.Price.Equals(other.Price) &&
                   string.Join(",", this.Categories.ToList()).Equals(string.Join(",", other.Categories.ToList()), StringComparison.Ordinal);
        }
    }
}