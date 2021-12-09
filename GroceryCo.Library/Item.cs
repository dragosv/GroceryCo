namespace GroceryCo.Library
{
    public class Item
    {
        public Item(Product product)
        {
            this.Product = product;
        }

        public Item()
        {
        }

        public Product Product { get; set; }
    }
}