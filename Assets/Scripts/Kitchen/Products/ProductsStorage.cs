using System.Collections.Generic;
using Core.Data.SaveLoad;
using Kitchen.Products.Enums;

namespace Kitchen.Products
{
    public class ProductsStorage
    {
        private const string RelativePath = "/products.json";
        
        private readonly IDataService _dataService;
        private readonly ProductConfigsStorage _productConfigsStorage;
        
        private readonly List<Product> _products = new();
        
        public IEnumerable<Product> Products => _products;

        public ProductsStorage(IDataService dataService, ProductConfigsStorage productConfigsStorage)
        {
            _dataService = dataService;
            _productConfigsStorage = productConfigsStorage;
        }

        public void LoadDefaultProducts()
        {
            var tomatoConfig = _productConfigsStorage.GetConfig(ProductType.Tomato, ProductCookingStage.Raw, 
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None);
            var beefConfig = _productConfigsStorage.GetConfig(ProductType.Beef, ProductCookingStage.Raw, 
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None);
            var saladConfig = _productConfigsStorage.GetConfig(ProductType.Salad, ProductCookingStage.Raw, 
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None);
            var doughConfig = _productConfigsStorage.GetConfig(ProductType.Dough, ProductCookingStage.Raw, 
                ProductChoppingStage.Unchopped, ProductShapeType.None, ProductAssemblyType.None);
            
            _products.Add(new Product(tomatoConfig));
            _products.Add(new Product(tomatoConfig));
            _products.Add(new Product(tomatoConfig));
            
            _products.Add(new Product(beefConfig));
            _products.Add(new Product(beefConfig));
            _products.Add(new Product(beefConfig));
            
            _products.Add(new Product(saladConfig));
            _products.Add(new Product(saladConfig));
            _products.Add(new Product(saladConfig));
            
            _products.Add(new Product(doughConfig));
            _products.Add(new Product(doughConfig));
            _products.Add(new Product(doughConfig));
        }
    }
}