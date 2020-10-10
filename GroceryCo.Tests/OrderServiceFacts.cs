using System.Collections.Generic;
using GroceryCo.Library;
using Moq;
using Xunit;

namespace GroceryCo.Tests
{
    public class OrderServiceFacts
    {
        private readonly Product firstProduct, secondProduct;
        
        public OrderServiceFacts()
        {
            firstProduct = new Product { Name = "Apple", Price = 1.99};
            secondProduct = new Product { Name = "Orange", Price = 1.39};
        }
        
        [Fact]
        public void WhenOneDiscount_ShouldPrintInvoice()
        {
            var items = new[] {new Item(firstProduct), new Item(secondProduct)};
            var order = new Order(items);
            
            var promotion = new Mock<IPromotion>();
            var invoiceService = new Mock<IInvoiceService>();

            invoiceService.Setup(x => x.Generate(It.IsAny<Order>(), It.IsAny<IEnumerable<IPromotion>>()))
                .Returns(new Invoice(items,
                    new Discount[] {new Discount {Item = items[0], Promotion = promotion.Object, Value = 0.5}}));
            
            var orderService = new OrderService(invoiceService.Object);

            string printedInvoice = orderService.PrintInvoice(order, new[] {promotion.Object});
            
            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\nTotal 2.88", printedInvoice);
        }
        
        [Fact]
        public void WhenNoDiscount_ShouldPrintInvoice()
        {
            var items = new[] {new Item(firstProduct), new Item(secondProduct)};
            var order = new Order(items);
            
            var promotion = new Mock<IPromotion>();
            var invoiceService = new Mock<IInvoiceService>();

            invoiceService.Setup(x => x.Generate(It.IsAny<Order>(), It.IsAny<IEnumerable<IPromotion>>()))
                .Returns(new Invoice(items, new Discount[] {}));
            
            var orderService = new OrderService(invoiceService.Object);

            string printedInvoice = orderService.PrintInvoice(order, new[] {promotion.Object});
            
            Assert.Equal("Apple 1.99\nOrange 1.39\nTotal 3.38", printedInvoice);
        }   
    }
}