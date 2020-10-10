using System.Collections.Generic;

namespace GroceryCo.Library
{
    public class Invoice
    {
        public Invoice(IList<Product> products, IList<Discount> discounts)
        {
            Products = products;
            Discounts = discounts;
        }
        
        public IList<Product> Products { get; private set; }
        
        public IList<Discount> Discounts { get; private set; }
    }
}