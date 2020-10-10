using GroceryCo.Library;
using Xunit;

namespace GroceryCo.Tests
{
    public class ProductSerializerFacts
    {
        private readonly Product firstProduct, secondProduct, thirdProduct;
        private readonly Product[] products;
 
        private const string Serialized =
            "[{\"Name\":\"Apple\",\"Price\":1.99,\"Categories\":[\"fruit\"]},{\"Name\":\"Orange\",\"Price\":1.39,\"Categories\":[\"fruit\"]},{\"Name\":\"Celery\",\"Price\":0.99,\"Categories\":[\"vegetable\"]}]";
        
        public ProductSerializerFacts()
        {
            firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" }};
            secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" }};
            thirdProduct = new Product { Name = "Celery", Price = 0.99, Categories = { "vegetable" }};
            
            products = new[] {firstProduct, secondProduct, thirdProduct};
        }
        
        [Fact]
        public void ShouldSerialize()
        {
            var serializer = new ProductsSerializer();

            var output = serializer.Serialize(products);
            
            Assert.Equal(Serialized, output);
        }
        
        [Fact]
        public void ShouldDeserialize()
        {
            var serializer = new ProductsSerializer();

            var deserialized = serializer.Deserialize(Serialized);
            
            Assert.Equal(products, deserialized);
        }
    }
}