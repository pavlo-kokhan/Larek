namespace Kitchen.Products
{
    public interface IProductState
    {
        Product Product { get; }
        int Quantity { get; set; }
    }
}