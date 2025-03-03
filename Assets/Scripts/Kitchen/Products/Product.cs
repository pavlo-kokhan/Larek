using System.Collections.Generic;
using Kitchen.Products.Enums;
using Kitchen.Table.Perspective;
using UnityEngine;

namespace Kitchen.Products
{
    public class Product
    {
        public ProductConfig Config { get; private set; }
        
        public ProductType Type => Config.Id.Type;
        public ProductCookingStage CookingStage => Config.Id.CookingStage;
        public ProductChoppingStage ChoppingStage => Config.Id.ChoppingStage;
        public GameObject Prefab => Config.Prefab;
        public bool CanBeSpawned => Config.CanBeSpawned;
        public bool CanBeFried => Config.CanBeFried;
        public float FryingTime => Config.FryingTime;
        public bool CanBeBaked => Config.CanBeBaked;
        public float BakingTime => Config.BakingTime;
        public bool CanBeInFridge => Config.CanBeInFridge;
        public bool CanBeInContainer => Config.CanBeInContainer;
        public bool CanBeChopped => Config.CanBeChopped;
        public int SlicesCount => Config.SlicesCount;
        public bool CanBeAssembled => Config.CanBeAssembled;
        public bool CanBeMixed => Config.CanBeMixed;
        public Sprite PickupCursorSprite => Config.PickupCursorSprite;
        public Dictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites => Config.ProductPerspectiveSprites;
        public Sprite InContainerSprite => Config.InContainerSprite;
        public Sprite OnChoppingBoardPanelSprite => Config.OnChoppingBoardPanelSprite;
        public Sprite OnOvenPanelSprite => Config.OnOvenPanelSprite;
        public Sprite[] InRefrigeratorSprites => Config.InRefrigeratorSprites;

        public ProductCookingStage NextCookingStage => 
            CookingStage switch
            {
                ProductCookingStage.Raw => ProductCookingStage.Medium,
                ProductCookingStage.Medium => ProductCookingStage.Done,
                ProductCookingStage.Done => ProductCookingStage.Burned,
                _ => CookingStage
            };
        
        public ProductChoppingStage NextChoppingStage => 
            ChoppingStage switch
            {
                ProductChoppingStage.Unchopped => ProductChoppingStage.Chopped,
                _ => ChoppingStage
            };

        public Product(ProductConfig config)
        {
            Config = config;
        }

        public bool IsTheSameAs(Product other)
        {
            return Type == other.Type
                   && CookingStage == other.CookingStage
                   && ChoppingStage == other.ChoppingStage;
        }
        
        public bool IsNotTheSameAs(Product other) => !IsTheSameAs(other);
    }
}