using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryCo.Library
{
    public class OrderService
    {
        private readonly IInvoiceService invoiceService;

        public OrderService(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public OrderService() : this(new InvoiceService())
        {
        }

        public string PrintInvoice(Order order, IEnumerable<IPromotion> promotions)
        {
            StringBuilder sb = new StringBuilder();
            var invoice = invoiceService.Generate(order, promotions);

            foreach (var item in invoice.Items)
            {
                sb.Append(item.Product.Name).Append(" ").Append(item.Product.Price).Append("\n");

                foreach (var discount in invoice.Discounts.Where(x => x.Item == item))
                {
                    sb.Append("-").Append(discount.Value);
                }
            }

            sb.Append("Total ").Append(invoice.Total);
            
            return sb.ToString();
        }
    }
}