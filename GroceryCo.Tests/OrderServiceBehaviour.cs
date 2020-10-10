using System.Collections.Generic;
using GroceryCo.Library;
using Moq;
using Xunit;

namespace GroceryCo.Tests
{
    public class OrderServiceBehaviour
    {
        private readonly Product firstProduct, secondProduct;
        
        public OrderServiceBehaviour()
        {
            firstProduct = new Product { Name = "Apple", Price = 1.99};
            secondProduct = new Product { Name = "Orange", Price = 1.39};
        }
        
        [Fact]
        public void WhenOneDiscount_ShouldPrintInvoice()
        {
            var items = new[] {new Item(firstProduct), new Item(secondProduct)};
            var order = new Order(items);

            var promotion = new OnSalePromotion {Product = firstProduct, SalePrice = 1.49};
            
            var orderService = new OrderService();

            string printedInvoice = orderService.PrintInvoice(order, new[] {promotion});
            
            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\nTotal 2.88", printedInvoice);
        }
    }
}