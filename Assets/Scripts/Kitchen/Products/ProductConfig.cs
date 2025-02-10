using Kitchen.Products.Enums;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Configs/Product")]
    public class ProductConfig : ScriptableObject
    {
        [field: SerializeField] public ProductType Type { get; private set; }
        [field: SerializeField] public ProductFryingStage FryingStage { get; private set; }
        [field: SerializeField] public bool CanBeFried { get; private set; } = true;
        [field: SerializeField] public bool CanBeInFridge { get; private set; } = true;
        [field: SerializeField] public bool CanBeChopped { get; private set; } = true;
        [field: SerializeField] public bool CanBeMixed { get; private set; } = true;
        [field: SerializeField] public bool CanBeInContainer { get; private set; } = true;
        [field: SerializeField] public Sprite PickupCursorSprite { get; private set; }
        [field: SerializeField] public Sprite InContainerSprite { get; private set; }
        [field: SerializeField] public Sprite OnChoppingBoardSprite { get; private set; }
        [field: SerializeField] public Sprite OnHotplateSprite { get; private set; }
        [field: SerializeField] public Sprite[] InRefrigeratorSprites { get; private set; }

        private void OnValidate()
        {
            if (CanBeFried == false)
            {
                OnHotplateSprite = null;
            }

            if (CanBeInFridge == false)
            {
                InRefrigeratorSprites = null;
            }

            if (CanBeChopped == false)
            {
                OnChoppingBoardSprite = null;
            }

            if (CanBeInContainer == false)
            {
                InContainerSprite = null;
            }
        }
    }
}