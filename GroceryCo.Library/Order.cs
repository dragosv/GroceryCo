using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class Order
    {
        public Order(IEnumerable<Item> items)
        {
            Items = items.ToList();
        }
        public IList<Item> Items { get; private set; }

        // public IList<ProductGroup> GroupedProducts
        // {
        //     get
        //     {
        //         return Products.GroupBy(x => x)
        //             .Select(group => new ProductGroup(group.Key, group.Count()))
        //             .ToList();
        //     }
        // }
    }
}    

