using System.Collections.Generic;

namespace GroceryCo.Library
{
    public interface IPromotion
    {
        IList<ProductGroup> ApplyTo(Order order);
        double Discount(Order order);
    }
}