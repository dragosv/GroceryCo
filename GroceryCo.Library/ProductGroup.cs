namespace GroceryCo.Library
{
    public class ProductGroup
    {
        public ProductGroup(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}