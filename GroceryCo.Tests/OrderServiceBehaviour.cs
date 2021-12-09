namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Xunit;

    public class OrderServiceBehaviour
    {
        private readonly Product firstProduct;
        private readonly Product secondProduct;

        public OrderServiceBehaviour()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99 };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39 };
        }

        [Fact]
        public void WhenOneDiscountShouldPrintInvoice()
        {
            var items = new[] { new Item(this.firstProduct), new Item(this.secondProduct) };
            var order = new Order(items);

            var promotion = new OnSalePromotion { Product = this.firstProduct, SalePrice = 1.49 };

            var orderService = new OrderService();

            string printedInvoice = orderService.PrintInvoice(order, new[] { promotion });

            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\nTotal 2.88", printedInvoice);
        }
    }
}