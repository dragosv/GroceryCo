using System.Collections.Generic;

namespace GroceryCo.Library
{
    public interface IPromotion
    {
        IList<Discount> ApplyTo(Order order);
    }
}