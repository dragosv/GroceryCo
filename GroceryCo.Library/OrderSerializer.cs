using System.IO;
using Newtonsoft.Json;

namespace GroceryCo.Library
{
    public class OrderSerializer
    {
        private readonly JsonSerializer serializer;

        public OrderSerializer(Product[] products)
        {
            serializer = new JsonSerializer();
            serializer.Converters.Add(new ProductConverter(products));
            serializer.NullValueHandling = NullValueHandling.Ignore;
        }
        
        public string Serialize(Order order)
        {
            using (var sw = new StringWriter())
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, order);
                }

                return sw.ToString();
            }
        }

        public Order Deserialize(string order)
        {
            using (var sr = new StringReader(order))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<Order>(reader);
                }
            }
        } 
    }
}