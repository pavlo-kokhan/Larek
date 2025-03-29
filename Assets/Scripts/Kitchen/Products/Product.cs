using System.Collections.Generic;
using Kitchen.Products.Enums;
using Kitchen.Table.Perspective;
using Unity.VisualScripting;
using UnityEngine;

namespace Kitchen.Products
{
    public class Product
    {
        public ProductConfig Config { get; }
        
        public ProductId Id => Config.Id;
        public ProductType Type => Config.Id.Type;
        public ProductCookingStage CookingStage => Config.Id.CookingStage;
        public ProductChoppingStage ChoppingStage => Config.Id.ChoppingStage;
        public ProductShapeType ShapeType => Config.Id.ShapeType;
        public ProductAssemblyType AssemblyType => Config.Id.AssemblyType;
        
        public GameObject Prefab => Config.Prefab;
        
        public ProductConfig NextFryingConfig => Config.NextFryingConfig;
        public ProductConfig NextBakingConfig => Config.NextBakingConfig;
        public IReadOnlyList<ProductConfig> ChoppingParticles => Config.ChoppingParticles.AsReadOnly();
        
        public float FryingTime => Config.FryingTime;
        public float BakingTime => Config.BakingTime;
        public int SlicesCount => Config.SlicesCount;
        public bool CanBeAssembled => Config.CanBeAssembled;
        public IReadOnlyList<Sprite> PickupCursorSprites => Config.PickupCursorSprites.AsReadOnly();
        public IReadOnlyDictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites => Config.ProductPerspectiveSprites;
        public IReadOnlyList<Sprite> InContainerSprites => Config.InContainerSprites.AsReadOnly();
        public Sprite OnChoppingBoardPanelSprite => Config.OnChoppingBoardPanelSprite;
        public Sprite OnOvenPanelSprite => Config.OnOvenPanelSprite;
        public Sprite[] InRefrigeratorSprites => Config.InRefrigeratorSprites;

        public Product(ProductConfig config)
        {
            Config = config;
        }
    }
}