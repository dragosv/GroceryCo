using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class Invoice
    {
        public Invoice(IList<Item> items, IList<Discount> discounts)
        {
            Items = items;
            Discounts = discounts;
        }
        
        public IList<Item> Items { get; private set; }
        
        public IList<Discount> Discounts { get; private set; }

        public double Total
        {
            get
            {
                return Items.Sum(x => x.Product.Price) - Discounts.Sum(x => x.Value);
            }
        }
    }
}