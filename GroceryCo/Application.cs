using System.IO;
using GroceryCo.Library;

namespace GroceryCo
{
    public class Application
    {
        public string PrintInvoice(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new  FileNotFoundException("Directory does not exists");
            }

            var productsJsonFile = Path.Join(directory, "Products.json");

            if (!File.Exists(productsJsonFile))
            {
                throw new  FileNotFoundException("Products file does not exists");
            }
            
            var promotionsJsonFile = Path.Join(directory, "Promotions.json");

            if (!File.Exists(promotionsJsonFile))
            {
                throw new  FileNotFoundException("Promotions file does not exists");
            }
            
            var orderJsonFile = Path.Join(directory, "Order.json");

            if (!File.Exists(orderJsonFile))
            {
                throw new  FileNotFoundException("Order file does not exists");
            }
            
            var productsSerializer = new ProductsSerializer();
            var productsText = File.ReadAllText(productsJsonFile);
            var products = productsSerializer.Deserialize(productsText);

            var promotionsSerializer = new PromotionsSerializer(products);
            var orderSerializer = new OrderSerializer(products);

            var promotionsText = File.ReadAllText(promotionsJsonFile);
            var promotions = promotionsSerializer.Deserialize(promotionsText);

            var orderText = File.ReadAllText(orderJsonFile);
            var order = orderSerializer.Deserialize(orderText);
            
            var orderService = new OrderService();
            
            return orderService.PrintInvoice(order, promotions);
        }
    }
}