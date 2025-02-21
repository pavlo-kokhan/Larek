using Kitchen.Products;

namespace Kitchen.TrashContainer
{
    public class TrashContainerAcceptCondition : IAcceptProductCondition
    {
        public bool CanAcceptProduct(Product product) => true;
    }
}