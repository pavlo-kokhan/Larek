using System.Collections.Generic;
using System.Linq;
using Kitchen.Products;
using Kitchen.Products.Enums;

namespace Kitchen.ProductsContainer
{
    public class ProductsContainerSlotAcceptCondition : IAcceptProductCondition
    {
        private readonly IReadOnlyCollection<Product> _currentProductsInSlot;

        public ProductsContainerSlotAcceptCondition(IReadOnlyCollection<Product> currentProductsInSlot)
        {
            _currentProductsInSlot = currentProductsInSlot;
        }

        public bool CanAcceptProduct(Product product)
        {
            // todo: вимагає іншої логіки перевірки:
            // Не нарезанные продукты занимают весь контейнер.
            // Нарезанные можно положить в количестве трёх штук.
            // Продукты разных видов можно смешивать создавая уникальные комбинации.
            
            // покишо так:
            if (_currentProductsInSlot.Count == 0)
            {
                return product.CanBeInContainer;
            }
            
            var lastProduct = _currentProductsInSlot.Last();

            return product.CanBeInContainer
                   && product.ChoppingStage == ProductChoppingStage.Chopped
                   && product.IsTheSameAs(lastProduct);
        }
    }
}