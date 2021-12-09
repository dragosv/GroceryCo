namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Moq;
    using Xunit;

    public class InvoiceServiceFacts
    {
        private readonly Product firstProduct;
        private readonly Product secondProduct;

        public InvoiceServiceFacts()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99 };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39 };
        }

        [Fact]
        public void WhenPromotionAppliesShouldGenerateInvoiceWithDiscount()
        {
            var promotion = new Mock<IPromotion>();
            var discount = new Discount();
            _ = promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(new[] { discount });

            var items = new[] { new Item(this.firstProduct), new Item(this.secondProduct) };
            var order = new Order(items);

            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] { promotion.Object });

            Assert.Equal(items, invoice.Items);
            Assert.Equal(1, invoice.Discounts.Count);
            Assert.Equal(discount, invoice.Discounts[0]);

            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void WhenPromotionDoesNotAppliesShouldGenerateInvoiceWithNoDiscount()
        {
            var promotion = new Mock<IPromotion>();
            _ = promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(System.Array.Empty<Discount>());

            var items = new[] { new Item(this.firstProduct), new Item(this.secondProduct) };
            var order = new Order(items);

            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] { promotion.Object });

            Assert.Equal(items, invoice.Items);
            Assert.Equal(0, invoice.Discounts.Count);

            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }
    }
}