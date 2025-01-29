using Kitchen.Products;

namespace Kitchen.Refrigerator.Products
{
    public class ProductState : IProductState
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public ProductState(Product product)
        {
            Product = product;
            Quantity = product.InitialQuantity;
        }
    }
}