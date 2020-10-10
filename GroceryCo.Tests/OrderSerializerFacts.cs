using GroceryCo.Library;
using Xunit;

namespace GroceryCo.Tests
{
    public class OrderSerializerFacts
    {
        private readonly Product[] products;
        private readonly Order order;
        private const string Serialized = "{\"Items\":[{\"Product\":\"Apple\"},{\"Product\":\"Orange\"}]}";

        public OrderSerializerFacts()
        {
            var firstProduct = new Product { Name = "Apple", Price = 1.99};
            var secondProduct = new Product { Name = "Orange", Price = 1.39};
            var items = new[] {new Item(firstProduct), new Item(secondProduct)};

            products = new[] {firstProduct, secondProduct};
            
            order = new Order(items);
        }
        
        [Fact]
        public void ShouldSerialize()
        {
            var serializer = new OrderSerializer(products);

            var output = serializer.Serialize(order);
            
            Assert.Equal(Serialized, output);
        }
        
        [Fact]
        public void ShouldDeserialize()
        {
            var serializer = new OrderSerializer(products);

            var deserialized = serializer.Deserialize(Serialized);
            
            Assert.Equal(order.Items.Count, deserialized.Items.Count);
            Assert.Equal(order.Items[0].Product, deserialized.Items[0].Product);
            Assert.Equal(order.Items[1].Product, deserialized.Items[1].Product);
        }
    }
}