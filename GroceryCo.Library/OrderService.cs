using System;
using System.Collections.Generic;

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
            var invoice = invoiceService.Generate(order, promotions);

            return "";
        }
    }
}