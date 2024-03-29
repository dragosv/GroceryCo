namespace GroceryCo.Library
{
    using System.Collections.Generic;
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
                var promotionDiscounts = promotion.ApplyTo(order);

                discounts.AddRange(promotionDiscounts);
            }

            return new Invoice(order.Items, discounts);
        }
    }
}