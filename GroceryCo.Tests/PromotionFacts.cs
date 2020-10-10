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
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            var productGroups = onSalePromotion.ApplyTo(order);
            
            Assert.Equal(1, productGroups.Count);
            Assert.Equal(firstProduct, productGroups[0].Product);
            Assert.Equal(1, productGroups[0].Quantity);
        }
        
        [Fact]
        public void OnSalePromotion_WithoutProduct_ShouldNotApplyOnOrder()
        {
            var order = new Order(new []{ secondProduct });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            var productGroups = onSalePromotion.ApplyTo(order);
            
            Assert.Equal(0, productGroups.Count);
        }
        
        [Fact]
        public void OnSalePromotion_ShouldDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            Assert.Equal(0.5, onSalePromotion.Discount(order));
        }

        [Fact]
        public void OnSalePromotion_MultipleProducts_ShouldDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct, firstProduct });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            Assert.Equal(1, onSalePromotion.Discount(order));
        }

        [Fact]
        public void OnSalePromotion_NoProduct_ShouldZeroDiscount()
        {
            var order = new Order(new []{ secondProduct });
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};

            Assert.Equal(0, onSalePromotion.Discount(order));
        }
        
        [Fact]
        public void GroupPromotion_BelowQuantity_ShouldZeroDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.Discount(order));
        }

        [Fact]
        public void GroupPromotion_MultipleProducts_ShouldDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct, firstProduct, firstProduct, firstProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(1.96, groupPromotion.Discount(order));
        }

        [Fact]
        public void GroupPromotion_NoProduct_ShouldZeroDiscount()
        {
            var order = new Order(new []{ secondProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.Discount(order));
        }
        
        [Fact]
        public void GroupPromotion_BelowQuantity_ShouldNotApplyNo()
        {
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }

        [Fact]
        public void GroupPromotion_NoProduct_ShouldNotApplyNo()
        {
            var order = new Order(new []{ secondProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }
        
        [Fact]
        public void GroupPromotion_MultipleProducts_ShouldApply()
        {
            var order = new Order(new []{ firstProduct, secondProduct, firstProduct, firstProduct, firstProduct });
            
            var groupPromotion = new GroupPromotion { Product = firstProduct, Quantity =  2, Price = 3};

            var productGroups = groupPromotion.ApplyTo(order);
            
            Assert.Equal(1, productGroups.Count);
            Assert.Equal(firstProduct, productGroups[0].Product);
            Assert.Equal(4, productGroups[0].Quantity);            
        }

        [Fact]
        public void AdditionalPromotion_NoMultiple_ShouldNotDiscount()
        {
            var order = new Order(new []{ firstProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal(0, additionalPromotion.Discount(order));
        }
        
        [Fact]
        public void AdditionalPromotion_AdditionalProducts_ShouldDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal(secondProduct.Price / 2, additionalPromotion.Discount(order));
        }
        
        [Fact]
        public void AdditionalPromotion_MultipleProducts_ShouldDiscount()
        {
            var order = new Order(new []{ firstProduct, secondProduct, firstProduct, secondProduct, thirdProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal((firstProduct.Price + secondProduct.Price) / 2, additionalPromotion.Discount(order));
        }
        
        [Fact]
        public void AdditionalPromotion_NoMultiple_ShouldNotApply()
        {
            var order = new Order(new []{ firstProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal(0, additionalPromotion.ApplyTo(order).Count);
        }
        
        [Fact]
        public void AdditionalPromotion_AdditionalProducts_ShouldApply()
        {
            var order = new Order(new []{ firstProduct, secondProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var productGroups = additionalPromotion.ApplyTo(order);
            
            Assert.Equal(2, productGroups.Count);
            Assert.Equal(firstProduct, productGroups[0].Product);
            Assert.Equal(1, productGroups[0].Quantity);
            Assert.Equal(secondProduct, productGroups[1].Product);
            Assert.Equal(1, productGroups[1].Quantity);            
        }
        
        [Fact]
        public void AdditionalPromotion_MultipleProducts_ShouldApply()
        {
            var order = new Order(new []{ firstProduct, secondProduct, firstProduct, secondProduct, thirdProduct });
            
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var productGroups = additionalPromotion.ApplyTo(order);
            
            Assert.Equal(2, productGroups.Count);
            Assert.Equal(firstProduct, productGroups[0].Product);
            Assert.Equal(2, productGroups[0].Quantity);
            Assert.Equal(secondProduct, productGroups[1].Product);
            Assert.Equal(2, productGroups[1].Quantity);   
        }
    }
}