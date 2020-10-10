using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Library
{
    public interface IInvoiceService
    {
        Invoice Generate(Order order, IEnumerable<IPromotion> promotions);
    }

    public class InvoiceService : IInvoiceService
    {
        public Invoice Generate(Order order, IEnumerable<IPromotion> promotions)
        {
            var discounts = new List<Discount>();
            
            foreach (var promotion in promotions)
            {
                var productGroups = promotion.ApplyTo(order);

                if (productGroups.Count > 0)
                {
                    double discountValue = promotion.Discount(order);
                    
                    discounts.Add(new Discount() { Promotion = promotion, ProductGroups = productGroups, Value = discountValue });
                }
            }
            
            return new Invoice(order.Products, discounts);
        }
    }
}