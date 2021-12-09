namespace GroceryCo.Library
{
    public class Discount
    {
        public IPromotion Promotion { get; set; }

        public Item Item { get; set; }

        public double Value { get; set; }
    }
}