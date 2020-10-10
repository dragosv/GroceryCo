using GroceryCo.Library;
using Moq;
using Xunit;

namespace GroceryCo.Tests
{
    public class InvoiceFacts
    {
        private readonly Product firstProduct, secondProduct;
        
        public InvoiceFacts()
        {
            firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" }};
            secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" }};
        }
        
        [Fact]
        public void ShouldCalculateTotal_WhenDiscount()
        {
            var promotion = new Mock<IPromotion>();

            var firstItem = new Item(firstProduct);
            var secondItem = new Item(secondProduct);
            
            var invoice = new Invoice(new [] { firstItem, secondItem }, 
                new [] { new Discount() { Item = firstItem, Promotion = promotion.Object, Value = 0.1 } });
            
            Assert.Equal(3.28, invoice.Total);
        }
        
        [Fact]
        public void ShouldCalculateTotal_WhenNoDiscount()
        {
            var firstItem = new Item(firstProduct);
            var secondItem = new Item(secondProduct);

            var invoice = new Invoice(new[] {firstItem, secondItem}, new Discount[] { });
            
            Assert.Equal(3.38, invoice.Total);
        }

        [Fact]
        public void ShouldCalculateTotal_WhenOneProduct()
        {
            var firstItem = new Item(firstProduct);

            var invoice = new Invoice(new[] {firstItem}, new Discount[] { });
            
            Assert.Equal(1.99, invoice.Total);
        }
    }
}