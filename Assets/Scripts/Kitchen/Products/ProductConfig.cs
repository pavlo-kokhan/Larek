using System;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using Kitchen.Products.Enums;
using Kitchen.Products.OnTable.Perspective;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Configs/Product")]
    public class ProductConfig : ScriptableObject
    {
        //
        [field: SerializeField, Header("Unique identifiers combination")] 
        public ProductType Type { get; private set; }
        [field: SerializeField] public ProductCookingStage CookingStage { get; private set; }
        [field: SerializeField] public ProductChoppingStage ChoppingStage { get; private set; }
        
        [field: SerializeField, Header("Prefabs")] public GameObject Prefab { get; private set; }
        
        //
        [field: SerializeField, Header("Flags")] 
        public bool CanBeSpawned { get; private set; } = true;
        [field: SerializeField] public bool CanBeFried { get; private set; } = true;
        [field: SerializeField] public bool CanBeInFridge { get; private set; } = true;
        [field: SerializeField] public bool CanBeInContainer { get; private set; } = true;
        [field: SerializeField] public bool CanBeStackedInContainer { get; private set; } = true;
        [field: SerializeField] public bool CanBeChopped { get; private set; } = true;
        [field: SerializeField] public int SlicesCount { get; private set; } = 1;
        [field: SerializeField] public bool CanBeBaked { get; private set; } = true;
        [field: SerializeField] public bool CanBeAssembled { get; private set; } = true;
        [field: SerializeField] public bool CanBeMixed { get; private set; } = true;

        //
        [SerializedDictionary] [field: SerializeField, Header("Sprites")]
        public SerializedDictionary<TablePerspectiveType, ProductObjectSprite> ProductPerspectiveSprites { get; private set; } = new();
        [field: SerializeField] public Sprite PickupCursorSprite { get; private set; }
        [field: SerializeField] public Sprite InContainerSprite { get; private set; }
        [field: SerializeField] public Sprite OnChoppingBoardSprite { get; private set; }
        [field: SerializeField] public Sprite OnChoppingBoardPanelSprite { get; private set; }
        [field: SerializeField] public Sprite OnAssemblyBoardSprite { get; private set; }
        [field: SerializeField] public Sprite OnOvenPanelSprite { get; private set; }
        [field: SerializeField] public Sprite OnHotplateSprite { get; private set; }
        [field: SerializeField] public Sprite[] InRefrigeratorSprites { get; private set; } = Array.Empty<Sprite>();

        private void OnValidate()
        {
            if (CanBeSpawned == false)
            {
                Prefab = null;
                ProductPerspectiveSprites = null;
            }
            
            if (CanBeFried == false)
            {
                OnHotplateSprite = null;
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
            
            if (CanBeChopped == false)
            {
                SlicesCount = 1;
                OnChoppingBoardSprite = null;
                OnChoppingBoardPanelSprite = null;
            }

            if (CanBeBaked == false)
            {
                OnOvenPanelSprite = null;
            }
            
            if (CanBeAssembled == false)
            {
                OnAssemblyBoardSprite = null;
            }
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