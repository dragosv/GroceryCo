using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class Product
    {
        public Product()
        {
            Categories = new List<string>();
        }
        
        public string Name { get; set; }
        
        public double Price { get; set; }
        
        public IList<string> Categories { get; private set; }  
        
        public override bool Equals(object? obj)
        {
            if (obj is Product product)
            {
                return Equals(product);
            }
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Categories);
        }
        
        private bool Equals(Product other)
        {
            return Name == other.Name && Price.Equals(other.Price) && 
                   string.Join(",", Categories.ToList()).Equals(string.Join(",", other.Categories.ToList()));
        }
    }
}