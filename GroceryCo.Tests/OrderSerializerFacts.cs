namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Xunit;

    public class OrderSerializerFacts
    {
        private const string Serialized = "{\"Items\":[{\"Product\":\"Apple\"},{\"Product\":\"Orange\"}]}";
        private readonly Product[] products;
        private readonly Order order;

        public OrderSerializerFacts()
        {
            var firstProduct = new Product { Name = "Apple", Price = 1.99 };
            var secondProduct = new Product { Name = "Orange", Price = 1.39 };
            var items = new[] { new Item(firstProduct), new Item(secondProduct) };

            this.products = new[] { firstProduct, secondProduct };

            this.order = new Order(items);
        }

        [Fact]
        public void ShouldSerialize()
        {
            var serializer = new OrderSerializer(this.products);

            var output = serializer.Serialize(this.order);

            Assert.Equal(Serialized, output);
        }

        [Fact]
        public void ShouldDeserialize()
        {
            var serializer = new OrderSerializer(this.products);

            var deserialized = serializer.Deserialize(Serialized);

            Assert.Equal(this.order.Items.Count, deserialized.Items.Count);
            Assert.Equal(this.order.Items[0].Product, deserialized.Items[0].Product);
            Assert.Equal(this.order.Items[1].Product, deserialized.Items[1].Product);
        }
    }
}