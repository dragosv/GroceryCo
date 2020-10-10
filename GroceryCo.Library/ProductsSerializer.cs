using Newtonsoft.Json;

namespace GroceryCo.Library
{
    public class ProductsSerializer
    {
        public string Serialize(Product[] products)
        {
            return JsonConvert.SerializeObject(products);
        }  
        
        public Product[] Deserialize(string products)
        {
            return JsonConvert.DeserializeObject<Product[]>(products);
        }  
    }
}