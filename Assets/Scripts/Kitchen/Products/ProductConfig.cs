using System;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using Kitchen.Products.Enums;
using Kitchen.Table.Perspective;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Configs/Product")]
    public class ProductConfig : ScriptableObject
    {
        // --------------------------------------------------------------------------------------------------------
        [field: SerializeField] public ProductId Id { get; private set; }
        // --------------------------------------------------------------------------------------------------------
        
        // --------------------------------------------------------------------------------------------------------
        [field: SerializeField, Header("Prefabs")] public GameObject Prefab { get; private set; }
        // --------------------------------------------------------------------------------------------------------
        
        // --------------------------------------------------------------------------------------------------------
        [field: SerializeField, Header("Properties")] 
        public bool CanBeSpawned { get; private set; } = true;
        [field: SerializeField] public bool CanBeFried { get; private set; } = true;
        [field: SerializeField] public float FryingTime { get; private set; } = 5f;
        [field: SerializeField] public bool CanBeBaked { get; private set; } = true;
        [field: SerializeField] public float BakingTime { get; private set; } = 5f;
        [field: SerializeField] public bool CanBeInFridge { get; private set; } = true;
        [field: SerializeField] public bool CanBeInContainer { get; private set; } = true;
        [field: SerializeField] public bool CanBeStackedInContainer { get; private set; } = true;
        [field: SerializeField] public bool CanBeChopped { get; private set; } = true;
        [field: SerializeField] public int SlicesCount { get; private set; } = 1;
        [field: SerializeField] public bool CanBeAssembled { get; private set; } = true;
        [field: SerializeField] public bool CanBeMixed { get; private set; } = true;
        // --------------------------------------------------------------------------------------------------------
        
        // --------------------------------------------------------------------------------------------------------
        [SerializedDictionary] [field: SerializeField, Header("Sprites")]
        public SerializedDictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites { get; private set; } = new();
        [field: SerializeField] public Sprite PickupCursorSprite { get; private set; }
        [field: SerializeField] public Sprite InContainerSprite { get; private set; }
        [field: SerializeField] public Sprite OnChoppingBoardPanelSprite { get; private set; }
        [field: SerializeField] public Sprite OnOvenPanelSprite { get; private set; }
        [field: SerializeField] public Sprite[] InRefrigeratorSprites { get; private set; } = Array.Empty<Sprite>();
        // --------------------------------------------------------------------------------------------------------
        
        private void OnValidate()
        {
            if (CanBeSpawned == false)
            {
                Prefab = null;
                ProductPerspectiveSprites = null;
            }
            
            if (CanBeInFridge == false)
            {
                InRefrigeratorSprites = Array.Empty<Sprite>();
            }

            if (CanBeInFridge == false)
            {
                InRefrigeratorSprites = null;
            }
            
            if (CanBeInContainer == false)
            {
                InContainerSprite = null;
            }
            
            if (CanBeBaked == false)
            {
                OnOvenPanelSprite = null;
            }
        }
    }

    [Serializable]
    public struct ProductId : IEquatable<ProductId>
    {
        [field: SerializeField] public ProductType Type { get; private set; }
        [field: SerializeField] public ProductCookingStage CookingStage { get; private set; }
        [field: SerializeField] public ProductChoppingStage ChoppingStage { get; private set; }

        public ProductId(ProductType type, ProductCookingStage cookingStage, ProductChoppingStage choppingStage)
        {
            Type = type;
            CookingStage = cookingStage;
            ChoppingStage = choppingStage;
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}" +
                   $"{nameof(CookingStage)}: {CookingStage}" +
                   $"{nameof(ChoppingStage)}: {ChoppingStage}";
        }

        public bool Equals(ProductId other)
        {
            return Type == other.Type 
                   && CookingStage == other.CookingStage 
                   && ChoppingStage == other.ChoppingStage;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Type, (int)CookingStage, (int)ChoppingStage);
        }
    }
    
    [Serializable]
    public struct ProductObjectSprite
    {
        [field: SerializeField] [CanBeNull] public Sprite ProductSprite { get; private set; }
        [field: SerializeField] [CanBeNull] public Sprite ShadowSprite { get; private set; }
        [field: SerializeField] [CanBeNull] public Sprite JuiceSprite { get; private set; }
            
        public ProductObjectSprite(Sprite productSprite, Sprite shadowSprite, Sprite juiceSprite)
        {
            ProductSprite = productSprite;
            ShadowSprite = shadowSprite;
            JuiceSprite = juiceSprite;
        }
    }
}