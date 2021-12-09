namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Xunit;

    public class PromotionFacts
    {
        private readonly Product firstProduct;
        private readonly Product secondProduct;
        private readonly Product thirdProduct;

        public PromotionFacts()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" } };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" } };
            this.thirdProduct = new Product { Name = "Celery", Price = 0.99, Categories = { "vegetable" } };
        }

        [Fact]
        public void OnSalePromotionWithProductShouldApplyOnOrder()
        {
            var order = new Order(new[] { new Item(this.firstProduct), new Item(this.secondProduct) });

            var onSalePromotion = new OnSalePromotion { Product = this.firstProduct, SalePrice = 1.49 };

            var discounts = onSalePromotion.ApplyTo(order);

            Assert.Equal(1, discounts.Count);
            Assert.Equal(order.Items[0], discounts[0].Item);
            Assert.Equal(this.firstProduct.Price - 1.49, discounts[0].Value);
        }

        [Fact]
        public void OnSalePromotionWithoutProductShouldNotApplyOnOrder()
        {
            var order = new Order(new[] { new Item(this.secondProduct) });

            var onSalePromotion = new OnSalePromotion { Product = this.firstProduct, SalePrice = 1.49 };

            var discounts = onSalePromotion.ApplyTo(order);

            Assert.Equal(0, discounts.Count);
        }

        [Fact]
        public void GroupPromotionBelowQuantityShouldNotApplyNo()
        {
            var order = new Order(new[] { new Item(this.firstProduct), new Item(this.secondProduct) });

            var groupPromotion = new GroupPromotion { Product = this.firstProduct, Quantity = 2, Price = 3 };

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }

        [Fact]
        public void GroupPromotionNoProductShouldNotApplyNo()
        {
            var order = new Order(new[] { new Item(this.secondProduct) });

            var groupPromotion = new GroupPromotion { Product = this.firstProduct, Quantity = 2, Price = 3 };

            Assert.Equal(0, groupPromotion.ApplyTo(order).Count);
        }

        [Fact]
        public void GroupPromotionMultipleProductsShouldApply()
        {
            var order = new Order(new[] { new Item(this.firstProduct), new Item(this.secondProduct), new Item(this.firstProduct), new Item(this.firstProduct), new Item(this.firstProduct) });

            var groupPromotion = new GroupPromotion { Product = this.firstProduct, Quantity = 2, Price = 3 };

            var discounts = groupPromotion.ApplyTo(order);
            var discountValue = this.firstProduct.Price - 1.5;

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
        public void AdditionalPromotionNoMultipleShouldNotApply()
        {
            var order = new Order(new[] { new Item(this.firstProduct) });

            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            Assert.Equal(0, additionalPromotion.ApplyTo(order).Count);
        }

        [Fact]
        public void AdditionalPromotionAdditionalProductsShouldApply()
        {
            var order = new Order(new[] { new Item(this.firstProduct), new Item(this.secondProduct) });

            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var discounts = additionalPromotion.ApplyTo(order);

            Assert.Equal(1, discounts.Count);
            Assert.Equal(order.Items[1], discounts[0].Item);
            Assert.Equal(this.secondProduct.Price / 2, discounts[0].Value);
        }

        [Fact]
        public void AdditionalPromotionMultipleProductsShouldApply()
        {
            var order = new Order(new[] { new Item(this.firstProduct), new Item(this.secondProduct), new Item(this.firstProduct), new Item(this.secondProduct), new Item(this.thirdProduct) });

            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };

            var discounts = additionalPromotion.ApplyTo(order);

            Assert.Equal(2, discounts.Count);
            Assert.Equal(order.Items[2], discounts[0].Item);
            Assert.Equal(this.firstProduct.Price / 2, discounts[0].Value);
            Assert.Equal(order.Items[3], discounts[1].Item);
            Assert.Equal(this.secondProduct.Price / 2, discounts[1].Value);
        }
    }
}