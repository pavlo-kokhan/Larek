using System;
using System.Collections.Generic;
using Core.Data.PersistantData;
using Core.Data.SaveLoad;
using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    public class ProductsStorage
    {
        private const string RelativePath = "/products.json";
        
        private readonly IDataService _dataService;
        private readonly ProductConfigsStorage _productConfigsStorage;
        
        private List<Product> _products = new();
        
        public IEnumerable<Product> Products => _products;

        public ProductsStorage(IDataService dataService, ProductConfigsStorage productConfigsStorage)
        {
            _dataService = dataService;
            _productConfigsStorage = productConfigsStorage;
        }

        public void SaveProducts()
        {
            var productsSaveData = new ProductsSaveData(_products);
            
            if (_dataService.SaveData(RelativePath, productsSaveData) == false)
            {
                Debug.Log("Failed to save products.");
                return;
            }
            
            Debug.Log("Products are saved successfully.");
        }

        public void LoadProducts()
        {
            try
            {
                var result = _dataService.LoadData<ProductsSaveData>(RelativePath);

                if (result == null)
                {
                    Debug.Log("Loaded products are null.");
                    _products.Clear();
                    return;
                }

                foreach (var productSaveData in result.Products)
                {
                    var type = productSaveData.Type;
                    var fryingStage = productSaveData.FryingStage;
                    var config = _productConfigsStorage.GetConfigOfProductType(type, fryingStage);

                    if (config == null)
                    {
                        Debug.LogError($"Product config of type {type} " +
                                       $"and frying stage {fryingStage} is not found.");
                        continue;
                    }
                    
                    _products.Add(new Product(config, productSaveData.Location));
                }
            }
            catch (Exception)
            {
                Debug.Log("Failed to load products");
            }
        }

        public void LoadDefaultProducts()
        {
            var tomatoConfig = _productConfigsStorage.GetConfigOfProductType(ProductType.Tomato, ProductFryingStage.Raw);
            var beefConfig = _productConfigsStorage.GetConfigOfProductType(ProductType.Beef, ProductFryingStage.Raw);
            var saladConfig = _productConfigsStorage.GetConfigOfProductType(ProductType.Salad, ProductFryingStage.Raw);
            
            _products.Add(new Product(tomatoConfig, ProductLocation.Refrigerator));
            _products.Add(new Product(beefConfig, ProductLocation.Refrigerator));
            _products.Add(new Product(saladConfig, ProductLocation.Refrigerator));
        }
    }
}