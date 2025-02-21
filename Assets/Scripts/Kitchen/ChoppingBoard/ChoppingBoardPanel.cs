using Kitchen.Products;
using UnityEngine;

namespace Kitchen.ChoppingBoard
{
    public class ChoppingBoardPanel : MonoBehaviour
    {
        private Product _product;

        public Product Product
        {
            get => _product;
            set
            {
                if (value is null || _product is not null) return;
                _product = value;
            }
        }
        
        
    }
}