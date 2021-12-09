namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Moq;
    using Xunit;

    public class InvoiceFacts
    {
        private readonly Product firstProduct;
        private readonly Product secondProduct;

        public InvoiceFacts()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" } };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" } };
        }

        [Fact]
        public void ShouldCalculateTotalWhenDiscount()
        {
            var promotion = new Mock<IPromotion>();

            var firstItem = new Item(this.firstProduct);
            var secondItem = new Item(this.secondProduct);

            var invoice = new Invoice(
                new[] { firstItem, secondItem },
                new[] { new Discount() { Item = firstItem, Promotion = promotion.Object, Value = 0.1 } });

            Assert.Equal(3.28, invoice.Total);
        }

        [Fact]
        public void ShouldCalculateTotalWhenNoDiscount()
        {
            var firstItem = new Item(this.firstProduct);
            var secondItem = new Item(this.secondProduct);

            var invoice = new Invoice(new[] { firstItem, secondItem }, System.Array.Empty<Discount>());

            Assert.Equal(3.38, invoice.Total);
        }

        [Fact]
        public void ShouldCalculateTotalWhenOneProduct()
        {
            var firstItem = new Item(this.firstProduct);

            var invoice = new Invoice(new[] { firstItem }, System.Array.Empty<Discount>());

            Assert.Equal(1.99, invoice.Total);
        }
    }
}