using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroceryCo.Library
{
    public class ProductConverter : JsonConverter
    {
        private readonly Product[] products;

        public ProductConverter(Product[] products)
        {
            this.products = products;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Product).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, 
            Type objectType, 
            object existingValue, 
            JsonSerializer serializer)
        {
            var productName = (string)reader.Value;
                
            return products.FirstOrDefault(x => x.Name == productName);
        }
        
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var product = value as Product;

            if (product == null) return;

            writer.WriteValue(product.Name);
        }
    }
}