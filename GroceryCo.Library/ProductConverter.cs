namespace GroceryCo.Library
{
    using System;
    using Newtonsoft.Json;
    public class ProductConverter : JsonConverter
    {
        private readonly Product[] products;

        public ProductConverter(Product[] products)
        {
            this.products = products;
        }

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Product).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var productName = (string)reader.Value;

            return Array.Find(this.products, x => x.Name == productName);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not Product product)
            {
                return;
            }

            writer.WriteValue(product.Name);
        }
    }
}