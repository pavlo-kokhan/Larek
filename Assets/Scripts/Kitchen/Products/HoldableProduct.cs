using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Refrigerator/HoldableProduct")]
    public abstract class HoldableProduct : ScriptableObject
    {
        [field: SerializeField] public int InitialQuantity { get; private set; }
        [field: SerializeField] public Sprite PickupCursorIcon { get; private set; }
    }
}