using System;
using GroceryCo.Library;
using Xunit;

namespace GroceryCo.Tests
{
    public class PromotionsSerializerFacts
    {
        private readonly Product[] products;
        private readonly IPromotion[] promotions;
        
        private const string Serialized =
            "[{\"Type\":\"OnSalePromotion\",\"Product\":\"Apple\",\"SalePrice\":1.49},{\"Type\":\"GroupPromotion\",\"Product\":\"Orange\",\"Price\":3.0,\"Quantity\":2},{\"Type\":\"AdditionalPromotion\",\"Category\":\"fruit\",\"Percent\":50}]";
        
        public PromotionsSerializerFacts()
        {
            var firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" }};
            var secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" }};
            var thirdProduct = new Product { Name = "Celery", Price = 0.99, Categories = { "vegetable" }};
            
            var onSalePromotion = new OnSalePromotion { Product = firstProduct, SalePrice = 1.49};
            var groupPromotion = new GroupPromotion { Product = secondProduct, Quantity =  2, Price = 3};
            var additionalPromotion = new AdditionalPromotion { Category = "fruit", Percent = 50 };
            
            products = new[] {firstProduct, secondProduct, thirdProduct};
            promotions = new IPromotion[] {onSalePromotion, groupPromotion, additionalPromotion};
        }
        
        [Fact]
        public void ShouldSerialize()
        {
            var serializer = new PromotionsSerializer(products);

            string output = serializer.Serialize(promotions);
            
            Assert.Equal(Serialized, output);
        }
        
        [Fact]
        public void ShouldDeserialize()
        {
            var serializer = new PromotionsSerializer(products);

            var deserialized  = serializer.Deserialize(Serialized);
            
            Assert.Equal(promotions, deserialized);
        }

    }
}