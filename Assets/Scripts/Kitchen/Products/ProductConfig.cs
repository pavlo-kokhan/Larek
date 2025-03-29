using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using Kitchen.Products.Enums;
using Kitchen.Table.Perspective;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Configs/Products/Product")]
    public class ProductConfig : ScriptableObject
    {
        [field: SerializeField] public ProductId Id { get; private set; }
        
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public GameObject OnChoppingBoardPrefab { get; private set; }
        
        [field: SerializeField] public ProductConfig NextFryingConfig { get; private set; }
        [field: SerializeField] public ProductConfig NextBakingConfig { get; private set; }
        [field: SerializeField] public List<ProductConfig> ChoppingParticles { get; private set; }
        
        [field: SerializeField] public float FryingTime { get; private set; } = 5f;
        [field: SerializeField] public float BakingTime { get; private set; } = 5f;
        [field: SerializeField] public int SlicesCount { get; private set; } = 1;
        [field: SerializeField] public bool CanBeAssembled { get; private set; } = true;
        
        [SerializedDictionary] [field: SerializeField, Header("Sprites")]
        public SerializedDictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites { get; private set; } = new();
        [field: SerializeField] public List<Sprite> PickupCursorSprites { get; private set; } = new();
        [field: SerializeField] public List<Sprite> InContainerSprites { get; private set; }
        [field: SerializeField] public Sprite OnChoppingBoardPanelSprite { get; private set; }
        [field: SerializeField] public Sprite OnOvenPanelSprite { get; private set; }
        [field: SerializeField] public Sprite[] InRefrigeratorSprites { get; private set; } = Array.Empty<Sprite>();
    }

    [Serializable]
    public struct ProductId : IEquatable<ProductId>
    {
        [field: SerializeField] public ProductType Type { get; private set; }
        [field: SerializeField] public ProductCookingStage CookingStage { get; private set; }
        [field: SerializeField] public ProductChoppingStage ChoppingStage { get; private set; }
        [field: SerializeField] public ProductShapeType ShapeType { get; private set; }
        [field: SerializeField] public ProductAssemblyType AssemblyType { get; private set; }

        public ProductId(ProductType type, ProductCookingStage cookingStage, ProductChoppingStage choppingStage, 
            ProductShapeType shapeType, ProductAssemblyType assemblyType)
        {
            Type = type;
            CookingStage = cookingStage;
            ChoppingStage = choppingStage;
            ShapeType = shapeType;
            AssemblyType = assemblyType;
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}" +
                   $"{nameof(CookingStage)}: {CookingStage}" +
                   $"{nameof(ChoppingStage)}: {ChoppingStage}" +
                   $"{nameof(ShapeType)}: {ShapeType}" +
                   $"{nameof(AssemblyType)}: {AssemblyType}";
        }

        public bool Equals(ProductId other)
        {
            return Type == other.Type 
                   && CookingStage == other.CookingStage 
                   && ChoppingStage == other.ChoppingStage
                   && ShapeType == other.ShapeType
                   && AssemblyType == other.AssemblyType;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Type, 
                (int)CookingStage, 
                (int)ChoppingStage, 
                (int)ShapeType, 
                (int)AssemblyType);
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