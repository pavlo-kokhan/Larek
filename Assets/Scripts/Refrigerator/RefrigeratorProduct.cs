
using UnityEngine;

namespace Refrigerator
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Refrigerator/Product")]
    public class RefrigeratorProduct : ScriptableObject
    {
        [field: SerializeField] public RefrigeratorProductType Type { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }
        [field: SerializeField] public Sprite SpriteZeroQuantity { get; private set; }
        [field: SerializeField] public Sprite SpriteOneQuantity { get; private set; }
        [field: SerializeField] public Sprite SpriteTwoQuantity { get; private set; }
        [field: SerializeField] public Sprite SpriteThreeQuantity { get; private set; }
        [field: SerializeField] public Sprite SpriteFourQuantity { get; private set; }
        [field: SerializeField] public Sprite SpriteFiveAndMoreQuantity { get; private set; }
    }
}