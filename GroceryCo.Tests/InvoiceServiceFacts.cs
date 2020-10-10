using System.Collections.Generic;
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
            firstProduct = new Product() { Name = "Apple", Price = 1.99};
            secondProduct = new Product() { Name = "Orange", Price = 1.39};
        }
        
        [Fact]
        public void WhenPromotionApplies_ShouldGenerateInvoiceWithDiscount()
        {
            var promotion = new Mock<IPromotion>();
            promotion.Setup(x => x.Discount(It.IsAny<Order>())).Returns(0.5);
            promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(new[] {new ProductGroup(firstProduct, 1)});

            var products = new []{ firstProduct, secondProduct };
            var order = new Order(products);
            
            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] {promotion.Object});
            
            Assert.Equal(products, invoice.Products);
            Assert.Equal(1, invoice.Discounts.Count);
            Assert.Equal(promotion.Object, invoice.Discounts[0].Promotion);
            Assert.Equal(0.5, invoice.Discounts[0].Value);
            Assert.Equal(1, invoice.Discounts[0].ProductGroups.Count);
            Assert.Equal(firstProduct, invoice.Discounts[0].ProductGroups[0].Product);
            Assert.Equal(1, invoice.Discounts[0].ProductGroups[0].Quantity);
            
            promotion.Verify(x => x.Discount(It.IsAny<Order>()), Times.Once);
            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }
        
        [Fact]
        public void WhenPromotionDoesNotApplies_ShouldGenerateInvoiceWithNoDiscount()
        {
            var promotion = new Mock<IPromotion>();
            promotion.Setup(x => x.ApplyTo(It.IsAny<Order>())).Returns(new List<ProductGroup>());

            var products = new []{ firstProduct, secondProduct };
            var order = new Order(products);
            
            var invoiceService = new InvoiceService();
            var invoice = invoiceService.Generate(order, new[] {promotion.Object});
            
            Assert.Equal(products, invoice.Products);
            Assert.Equal(0, invoice.Discounts.Count);
            
            promotion.Verify(x => x.Discount(It.IsAny<Order>()), Times.Never);
            promotion.Verify(x => x.ApplyTo(It.IsAny<Order>()), Times.Once);
        }
    }
}