using Data;
using UnityEngine;

namespace Kitchen.Products
{
    [CreateAssetMenu(fileName = "Product", menuName = "Scriptable Objects/Refrigerator/Product")]
    public class Product : HoldableProduct
    {
        [field: SerializeField] public ProductType Type { get; private set; }
        
        [field: Header("For Refrigerator")]
        [field: SerializeField] public GameObject PrefabForRefrigerator { get; private set; }
        
        [field: Header("For Products Container")]
        [field: SerializeField] public Sprite InContainerSprite { get; private set; }
        // [field: SerializeField] public GameObject PrefabForProductsContainer { get; private set; }
    }
}