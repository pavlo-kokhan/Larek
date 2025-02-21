using System.Collections.Generic;
using Kitchen.Products.Enums;
using Kitchen.Products.OnTable.Perspective;
using UnityEngine;

namespace Kitchen.Products
{
    public class Product
    {
        public ProductConfig Config { get; private set; }
        public ProductLocation Location { get; set; }
        
        public ProductType Type => Config.Type;
        public ProductCookingStage CookingStage => Config.CookingStage;
        public ProductChoppingStage ChoppingStage => Config.ChoppingStage;
        public GameObject Prefab => Config.Prefab;
        public bool CanBeSpawned => Config.CanBeSpawned;
        public bool CanBeFried => Config.CanBeFried;
        public bool CanBeInFridge => Config.CanBeInFridge;
        public bool CanBeInContainer => Config.CanBeInContainer;
        public bool CanBeChopped => Config.CanBeChopped;
        public int SlicesCount => Config.SlicesCount;
        public bool CanBeBaked => Config.CanBeBaked;
        public bool CanBeAssembled => Config.CanBeAssembled;
        public bool CanBeMixed => Config.CanBeMixed;
        public Sprite PickupCursorSprite => Config.PickupCursorSprite;
        public Dictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites => Config.ProductPerspectiveSprites;
        public Sprite InContainerSprite => Config.InContainerSprite;
        public Sprite OnChoppingBoardSprite => Config.OnChoppingBoardSprite;
        public Sprite OnChoppingBoardPanelSprite => Config.OnChoppingBoardPanelSprite;
        public Sprite OnAssemblyBoardSprite => Config.OnAssemblyBoardSprite;
        public Sprite OnOvenPanelSprite => Config.OnOvenPanelSprite;
        public Sprite OnHotplateSprite => Config.OnHotplateSprite;
        public Sprite[] InRefrigeratorSprites => Config.InRefrigeratorSprites;
        
        public Product(ProductConfig config, ProductLocation location)
        {
            Config = config;
            Location = location;
        }

        public bool IsTheSameAs(Product other)
        {
            return Type == other.Type
                   && CookingStage == other.CookingStage
                   && ChoppingStage == other.ChoppingStage;
        }
        
        public bool IsNotTheSameAs(Product other) => !IsTheSameAs(other);

        public ProductCookingStage GetNextCookingStage()
        {
            return CookingStage switch
            {
                ProductCookingStage.Raw => ProductCookingStage.Medium,
                ProductCookingStage.Medium => ProductCookingStage.Done,
                ProductCookingStage.Done => ProductCookingStage.Burned,
                _ => CookingStage
            };
        }

        public ProductChoppingStage GetNextChoppingStage()
        {
            return ChoppingStage switch
            {
                ProductChoppingStage.Unchopped => ProductChoppingStage.Chopped,
                _ => ChoppingStage
            };
        }
    }
}