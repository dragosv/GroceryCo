namespace GroceryCo
{
    using System.IO;
    using System.IO.Abstractions;
    using GroceryCo.Library;

    public class Application
    {
        private readonly IFileSystem fileSystem;

        public Application(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Application()
            : this(
            fileSystem: new FileSystem()) // use default implementation which calls System.IO
        {
        }

        public string PrintInvoice(string directory)
        {
            if (!this.fileSystem.Directory.Exists(directory))
            {
                throw new FileNotFoundException("Directory does not exists");
            }

            var productsJsonFile = Path.Join(directory, "Products.json");

            if (!this.fileSystem.File.Exists(productsJsonFile))
            {
                throw new FileNotFoundException("Products file does not exists");
            }

            var promotionsJsonFile = Path.Join(directory, "Promotions.json");

            if (!this.fileSystem.File.Exists(promotionsJsonFile))
            {
                throw new FileNotFoundException("Promotions file does not exists");
            }

            var orderJsonFile = Path.Join(directory, "Order.json");

            if (!this.fileSystem.File.Exists(orderJsonFile))
            {
                throw new FileNotFoundException("Order file does not exists");
            }

            var productsSerializer = new ProductsSerializer();
            var productsText = this.fileSystem.File.ReadAllText(productsJsonFile);
            var products = ProductsSerializer.Deserialize(productsText);

            var promotionsSerializer = new PromotionsSerializer(products);
            var orderSerializer = new OrderSerializer(products);

            var promotionsText = this.fileSystem.File.ReadAllText(promotionsJsonFile);
            var promotions = promotionsSerializer.Deserialize(promotionsText);

            var orderText = this.fileSystem.File.ReadAllText(orderJsonFile);
            var order = orderSerializer.Deserialize(orderText);

            var orderService = new OrderService();

            return orderService.PrintInvoice(order, promotions);
        }
    }
}