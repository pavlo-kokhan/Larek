using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    public class Product
    {
        public ProductConfig Config { get; private set; }
        public ProductLocation Location { get; set; }
        
        public ProductType Type => Config.Type;
        public ProductFryingStage FryingStage => Config.FryingStage;
        public bool CanBeFried => Config.CanBeFried;
        public bool CanBeInFridge => Config.CanBeInFridge;
        public bool CanBeChopped => Config.CanBeChopped;
        public bool CanBeMixed => Config.CanBeMixed;
        public bool CanBeInContainer => Config.CanBeInContainer;
        public Sprite PickupCursorSprite => Config.PickupCursorSprite;
        public Sprite InContainerSprite => Config.InContainerSprite;
        public Sprite OnCuttingBoardSprite => Config.OnChoppingBoardSprite;
        public Sprite OnHotplateSprite => Config.OnHotplateSprite;
        public Sprite[] InRefrigeratorSprites => Config.InRefrigeratorSprites;
        
        public Product(ProductConfig config, ProductLocation location)
        {
            Config = config;
            Location = location;
        }

        // private ProductType GetChoppedType()
        // {
        //     return Type switch
        //     {
        //         ProductType.Tomato => ProductType.TomatoChopped,
        //         ProductType.Onion => ProductType.OnionChopped,
        //         ProductType.Potato => ProductType.FrenchFriesRaw,
        //         ProductType.Beef => ProductType.BeefPattyRaw,
        //         ProductType.Chicken => ProductType.ChickenChopped,
        //         _ => Type
        //     };
        // }
        //
        // private ProductType GetFriedType()
        // {
        //     return Type switch
        //     {
        //         ProductType.Beef => ProductType.BeefPattyRaw,
        //         ProductType.Chicken => ProductType.ChickenChopped,
        //         _ => Type
        //     };
        // }
    }
}