namespace GroceryCo.Library
{
    using System.IO;
    using Newtonsoft.Json;

    public class OrderSerializer
    {
        private readonly JsonSerializer serializer;

        public OrderSerializer(Product[] products)
        {
            this.serializer = new JsonSerializer();
            this.serializer.Converters.Add(new ProductConverter(products));
            this.serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        public string Serialize(Order order)
        {
            using var sw = new StringWriter();
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                this.serializer.Serialize(writer, order);
            }

            return sw.ToString();
        }

        public Order Deserialize(string order)
        {
            using var sr = new StringReader(order);
            using JsonReader reader = new JsonTextReader(sr);
            return this.serializer.Deserialize<Order>(reader);
        }
    }
}