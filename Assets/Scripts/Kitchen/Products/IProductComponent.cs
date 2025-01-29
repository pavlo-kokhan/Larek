using System;
using Kitchen.Refrigerator.Products;

namespace Kitchen.Products
{
    public interface IProductComponent
    {
        event Action<ProductState> ProductTaken;
        event Action<ProductState> ProductReturned;
    }
}