namespace GroceryCo.Library
{
    using System.Collections.Generic;

    public interface IPromotion
    {
        IList<Discount> ApplyTo(Order order);
    }
}