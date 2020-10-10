using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public class OnSalePromotion : IPromotion
    {
        public Product Product { get; set; }
        
        public double SalePrice { get; set; }

        public IList<ProductGroup> ApplyTo(Order order)
        {
            return order.GroupedProducts.Where(x => x.Product == Product).ToList();
        }

        public double Discount(Order order)
        {
            var productGroup = order.GroupedProducts.FirstOrDefault(x => x.Product == Product);

            if (productGroup == null)
            {
                return 0;
            }

            return productGroup.Quantity * (Product.Price - SalePrice);
        }
    }
}