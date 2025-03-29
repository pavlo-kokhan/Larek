using Kitchen.Products.ProductGameObject;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.ChoppingBoard
{
    public class ChoppingBoardPanel : MonoBehaviour
    {
        [SerializeField] private Image _productImage;
        
        private ChoppingBoard _board;
        public ChoppingBoard Board => _board;

        private bool _isInitialized;

        public void Initialize(ChoppingBoard choppingBoard)
        {
            if (_isInitialized) return;
            
            _board = choppingBoard;
            
            _isInitialized = true;
        }
    }
}