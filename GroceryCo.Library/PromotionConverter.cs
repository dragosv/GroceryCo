namespace GroceryCo.Library
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class PromotionConverter : JsonConverter
    {
        private readonly Product[] products;

        public PromotionConverter(Product[] products)
        {
            this.products = products;
        }

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IPromotion).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            // Create target object based on JObject
            Type type = Type.GetType("GroceryCo.Library." + jObject["Type"]);

            if (type == null)
            {
                return null;
            }

            var promotion = (IPromotion)Activator.CreateInstance(type);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), promotion);

            // foreach (var property in jObject.Properties())
            // {
            //     if (property.Name != "Type")
            //     {
            //         var typeProperty = type.GetProperty(property.Name);
            //
            //         if (typeProperty != null)
            //         {
            //             if (typeof(Product) == typeProperty.PropertyType)
            //             {
            //                 var product = products.FirstOrDefault(x => x.Name == property.Value.ToString());
            //
            //                 typeProperty.SetValue(promotion, product);
            //             }
            //             else
            //             {
            //                 typeProperty.SetValue(promotion, property.Value.ToObject(typeProperty.PropertyType));
            //             }
            //         }
            //     }
            // }
            return promotion;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JObject jo = new();
            Type type = value.GetType();
            jo.Add("Type", type.Name.Replace("GroceryCo.Library.", string.Empty));

            foreach (PropertyInfo prop in type.GetProperties().Where(x => x.CanRead))
            {
                object propVal = prop.GetValue(value, null);
                if (propVal != null)
                {
                    jo.Add(prop.Name, JToken.FromObject(propVal, serializer));
                }
            }

            jo.WriteTo(writer);
        }
    }
}