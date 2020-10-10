namespace GroceryCo.Library
{
    public class Item
    {
        public Item(Product product)
        {
            Product = product;
        }

        public Item() 
        {
        }

        public Product Product { get; set; }
    }
}