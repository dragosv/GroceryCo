using GroceryCo.Library;
using Moq;
using Xunit;

namespace GroceryCo.Tests
{
    public class InvoiceServiceFacts
    {
        private readonly Product firstProduct, secondProduct;
        
        public InvoiceServiceFacts()
        {                     
            firstProduct = new Product { Name = "Apple", Price = 1.99};
            secondProduct = new Product { Name = "Orange", Price = 1.39};
        }
        
        [Fact]
        public void WhenPromotionApplies_ShouldGenerateInvoiceWithDiscount()
        {
            var promotion = new Mock<IPromotion>();
            var discount = new Discount();
            promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(new[] { discount });

            var items = new[] {new Item(firstProduct), new Item(secondProduct)};
            var order = new Order(items);
            
            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] {promotion.Object});
            
            Assert.Equal(items, invoice.Items);
            Assert.Equal(1, invoice.Discounts.Count);
            Assert.Equal(discount, invoice.Discounts[0]);

            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }
        
        [Fact]
        public void WhenPromotionDoesNotApplies_ShouldGenerateInvoiceWithNoDiscount()
        {
            var promotion = new Mock<IPromotion>();
            promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(new Discount[] {} );

            var items = new[] {new Item(firstProduct), new Item(secondProduct)};
            var order = new Order(items);
            
            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] {promotion.Object});
            
            Assert.Equal(items, invoice.Items);
            Assert.Equal(0, invoice.Discounts.Count);
            
            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }
    }
}