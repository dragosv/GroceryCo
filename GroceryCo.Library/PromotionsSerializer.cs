using System.IO;
using Newtonsoft.Json;

namespace GroceryCo.Library
{
    public class PromotionsSerializer
    {
        private readonly JsonSerializer serializer;

        public PromotionsSerializer(Product[] products)
        {
            serializer = new JsonSerializer();
            serializer.Converters.Add(new PromotionConverter(products));
            serializer.Converters.Add(new ProductConverter(products));
            serializer.NullValueHandling = NullValueHandling.Ignore;
        }
        
        public string Serialize(IPromotion[] promotions)
        {
            using (var sw = new StringWriter())
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, promotions);
                }

                return sw.ToString();
            }
        }

        public IPromotion[] Deserialize(string promotions)
        {
            using (var sr = new StringReader(promotions))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<IPromotion[]>(reader);
                }
            }
        } 
    }
}