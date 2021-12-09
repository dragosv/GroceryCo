namespace GroceryCo.Library
{
    using System.Collections.Generic;
    using System.Linq;

    public class Order
    {
        public Order(IEnumerable<Item> items)
        {
            this.Items = items.ToList();
        }

        public IList<Item> Items { get; }

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
