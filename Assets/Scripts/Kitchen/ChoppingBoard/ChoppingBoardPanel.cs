using Kitchen.Products;
using Kitchen.Products.ProductGameObject;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.ChoppingBoard
{
    public class ChoppingBoardPanel : MonoBehaviour
    {
        [SerializeField] private Image _productImage;
        
        private ChoppingBoard _choppingBoard;
        public ProductObject ProductObject => _choppingBoard.FirstProductObject;

        private bool _isInitialized;

        public void Initialize(ChoppingBoard choppingBoard)
        {
            if (_isInitialized) return;
            
            _choppingBoard = choppingBoard;
            
            _isInitialized = true;
        }
    }
}