namespace Kitchen.Products
{
    public interface IAcceptProductCondition
    {
        bool CanAcceptProduct(Product product);
    }
}