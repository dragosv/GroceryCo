namespace GroceryCo.Library
{
    using System.IO;
    using Newtonsoft.Json;

    public class PromotionsSerializer
    {
        private readonly JsonSerializer serializer;

        public PromotionsSerializer(Product[] products)
        {
            this.serializer = new JsonSerializer();
            this.serializer.Converters.Add(new PromotionConverter(products));
            this.serializer.Converters.Add(new ProductConverter(products));
            this.serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        public string Serialize(IPromotion[] promotions)
        {
            using var sw = new StringWriter();
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                this.serializer.Serialize(writer, promotions);
            }

            return sw.ToString();
        }

        public IPromotion[] Deserialize(string promotions)
        {
            using var sr = new StringReader(promotions);
            using JsonReader reader = new JsonTextReader(sr);
            return this.serializer.Deserialize<IPromotion[]>(reader);
        }
    }
}