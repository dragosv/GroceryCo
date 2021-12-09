namespace GroceryCo.Library
{
    using Newtonsoft.Json;

    public class ProductsSerializer
    {
        public static string Serialize(Product[] products)
        {
            return JsonConvert.SerializeObject(products);
        }

        public static Product[] Deserialize(string products)
        {
            return JsonConvert.DeserializeObject<Product[]>(products);
        }
    }
}