namespace GroceryCo.Tests
{
    using System.Collections.Generic;
    using GroceryCo.Library;
    using Moq;
    using Xunit;

    public class OrderServiceFacts
    {
        private readonly Product firstProduct;
        private readonly Product secondProduct;

        public OrderServiceFacts()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99 };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39 };
        }

        [Fact]
        public void WhenOneDiscountShouldPrintInvoice()
        {
            var items = new[] { new Item(this.firstProduct), new Item(this.secondProduct) };
            var order = new Order(items);

            var promotion = new Mock<IPromotion>();
            var invoiceService = new Mock<IInvoiceService>();

            _ = invoiceService.Setup(x => x.Generate(It.IsAny<Order>(), It.IsAny<IEnumerable<IPromotion>>()))
                .Returns(new Invoice(
                    items,
                    new Discount[] { new Discount { Item = items[0], Promotion = promotion.Object, Value = 0.5 } }));

            var orderService = new OrderService(invoiceService.Object);

            string printedInvoice = orderService.PrintInvoice(order, new[] { promotion.Object });

            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\nTotal 2.88", printedInvoice);
        }

        [Fact]
        public void WhenNoDiscountShouldPrintInvoice()
        {
            var items = new[] { new Item(this.firstProduct), new Item(this.secondProduct) };
            var order = new Order(items);

            var promotion = new Mock<IPromotion>();
            var invoiceService = new Mock<IInvoiceService>();

            _ = invoiceService.Setup(x => x.Generate(It.IsAny<Order>(), It.IsAny<IEnumerable<IPromotion>>()))
                .Returns(new Invoice(items, System.Array.Empty<Discount>()));

            var orderService = new OrderService(invoiceService.Object);

            string printedInvoice = orderService.PrintInvoice(order, new[] { promotion.Object });

            Assert.Equal("Apple 1.99\nOrange 1.39\nTotal 3.38", printedInvoice);
        }
    }
}