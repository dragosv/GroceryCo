using System;
using System.Collections.Generic;

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
    }
}