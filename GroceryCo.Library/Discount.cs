using System.Collections.Generic;

namespace GroceryCo.Library
{
    public class Discount
    {
        public IPromotion Promotion { get; set; }
        
        public IList<ProductGroup> ProductGroups { get; set; }
        
        public double Value { get; set; }
    }
}