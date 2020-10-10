using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class GroupPromotion : IPromotion
    {
        public Product Product { get; set; }
        
        public double Price { get; set; }
        
        public int Quantity { get; set; }

        public IList<ProductGroup> ApplyTo(Order order)
        {
            var productGroup = order.GroupedProducts.FirstOrDefault(x => x.Product == Product);

            if (productGroup == null)
            {
                return new List<ProductGroup>();
            }

            if (productGroup.Quantity < Quantity)
            {
                return new List<ProductGroup>();
            }

            int discounted = productGroup.Quantity / Quantity;

            var productGroups = new List<ProductGroup> {new ProductGroup(Product, discounted * Quantity)};

            return productGroups;
        }

        public double Discount(Order order)
        {
            var productGroup = order.GroupedProducts.FirstOrDefault(x => x.Product == Product);

            if (productGroup == null)
            {
                return 0;
            }

            if (productGroup.Quantity < Quantity)
            {
                return 0;
            }

            int discounted = productGroup.Quantity / Quantity;
            
            return discounted  * (Product.Price * Quantity - Price);
        }
    }
}