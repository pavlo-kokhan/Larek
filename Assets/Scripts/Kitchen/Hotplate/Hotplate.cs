using Kitchen.Products.ProductGameObject;
using UnityEngine;

namespace Kitchen.Hotplate
{
    [RequireComponent(typeof(Collider2D))]
    public class Hotplate : MonoBehaviour
    {
        private readonly CookingType _cookingType = CookingType.Frying;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductCookingBehaviour>(out var cookingBehaviour))
            {
                if (cookingBehaviour.Product.FryingTime > 0f)
                {
                    cookingBehaviour.StartCookingProcess(_cookingType);
                    Debug.Log($"{cookingBehaviour.Product.Type} started frying on {nameof(Hotplate)}");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<ProductCookingBehaviour>(out var cookingBehaviour))
            {
                cookingBehaviour.PauseCookingProcess();
                Debug.Log($"{cookingBehaviour.Product.Type} exited {nameof(Hotplate)}");
            }
        }
    }
}