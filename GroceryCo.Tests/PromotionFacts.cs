using GroceryCo.Library;
using Xunit;

namespace GroceryCo.Tests
{
    public class PromotionFacts
    {
        private readonly Product firstProduct, secondProduct, thirdProduct;
        
        public PromotionFacts()
        {                     
            firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" }};
            secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" }};
            thirdProduct = new Product { Name = "Celery", Price = 0.99, Categories = { "vegetable" }};
        }
        
        [Fact]
        public void OnSalePromotion_WithProduct_ShouldApplyOnOrder()
        {
            var order = new Order(new []{ new Item(firstProduct), new Item(secondProduct) });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            var discounts = onSalePromotion.ApplyTo(order);
            
            Assert.Equal(1, discounts.Count);
            Assert.Equal(order.Items[0], discounts[0].Item);
            Assert.Equal(firstProduct.Price - 1.49, discounts[0].Value);
        }
        
        [Fact]
        public void OnSalePromotion_WithoutProduct_ShouldNotApplyOnOrder()
        {
            var order = new Order(new []{ new Item(secondProduct) });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            var discounts = onSalePromotion.ApplyTo(order);
            
            Assert.Equal(0, discounts.Count);
        }
        
        [Fact]
        public void GroupPromotion_BelowQuantity_ShouldNotApplyNo()
        {
            var order = new Order(new []{ new Item(firstProduct), new Item(secondProduct) });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }

        [Fact]
        public void GroupPromotion_NoProduct_ShouldNotApplyNo()
        {
            var order = new Order(new []{ new Item(secondProduct) });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }
        
        [Fact]
        public void GroupPromotion_MultipleProducts_ShouldApply()
        {
            var order = new Order(new []{ new Item(firstProduct), new Item(secondProduct), new Item(firstProduct), new Item(firstProduct), new Item(firstProduct) });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            var discounts = groupPromotion.ApplyTo(order);
            var discountValue = firstProduct.Price - 1.5;
            
            Assert.Equal(4, discounts.Count);
            Assert.Equal(order.Items[0], discounts[0].Item);
            Assert.Equal(discountValue, discounts[0].Value);
            Assert.Equal(order.Items[2], discounts[1].Item);
            Assert.Equal(discountValue, discounts[1].Value); 
            Assert.Equal(order.Items[3], discounts[2].Item);
            Assert.Equal(discountValue, discounts[2].Value); 
            Assert.Equal(order.Items[4], discounts[3].Item);
            Assert.Equal(discountValue, discounts[3].Value); 
        }
     
        [Fact]
        public void AdditionalPromotion_NoMultiple_ShouldNotApply()
        {
            var order = new Order(new []{ new Item(firstProduct) });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal(0, additionalPromotion.ApplyTo(order).Count);
        }
        
        [Fact]
        public void AdditionalPromotion_AdditionalProducts_ShouldApply()
        {
            var order = new Order(new []{ new Item(firstProduct), new Item(secondProduct) });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var discounts = additionalPromotion.ApplyTo(order);
            
            Assert.Equal(1, discounts.Count);
            Assert.Equal(order.Items[1], discounts[0].Item);
            Assert.Equal(secondProduct.Price / 2, discounts[0].Value);
        }
        
        [Fact]
        public void AdditionalPromotion_MultipleProducts_ShouldApply()
        {
            var order = new Order(new []{ new Item(firstProduct), new Item(secondProduct), new Item(firstProduct), new Item(secondProduct), new Item(thirdProduct) });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var discounts = additionalPromotion.ApplyTo(order);
            
            Assert.Equal(2, discounts.Count);
            Assert.Equal(order.Items[2], discounts[0].Item);
            Assert.Equal(firstProduct.Price / 2, discounts[0].Value);
            Assert.Equal(order.Items[3], discounts[1].Item);
            Assert.Equal(secondProduct.Price / 2, discounts[1].Value);   
        }
    }
}