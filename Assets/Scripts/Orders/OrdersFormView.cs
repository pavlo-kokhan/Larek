using Orders.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Orders
{
    public class OrdersFormView : MonoBehaviour
    {
        [SerializeField] private OrderFormInput _orderFormInput;
        
        [SerializeField] private Sprite _humanPanelSprite;
        [SerializeField] private Sprite _monsterPanelSprite;
        [SerializeField] private Sprite _humanCornerSprite;
        [SerializeField] private Sprite _monsterCornerSprite;
        
        [SerializeField] private Image _panelImage;
        [SerializeField] private Image _cornerImage;
        [SerializeField] private Image _signatureImage;
        
        private void OnEnable()
        {
            _orderFormInput.PageTurned += OnPageTurned;
            _orderFormInput.OrderConfirmed += OnOrderConfirmed;
        }
        
        private void OnDisable()
        {
            _orderFormInput.PageTurned -= OnPageTurned;
            _orderFormInput.OrderConfirmed -= OnOrderConfirmed;
            
            SetSignatureImage(false);
        }
        
        private void OnPageTurned(bool isHumanPage)
        {
            SetSignatureImage(false);
            
            if (isHumanPage)
            {
                _panelImage.sprite = _humanPanelSprite;
                _cornerImage.sprite = _humanCornerSprite;
            }
            else
            {
                _panelImage.sprite = _monsterPanelSprite;
                _cornerImage.sprite = _monsterCornerSprite;
            }
        }
        
        private void OnOrderConfirmed(bool isHumanPage)
        {
            SetSignatureImage(true);
        }

        private void SetSignatureImage(bool status)
        {
            _signatureImage.color = status 
                ? new Color(1f, 1f, 1f, 1f) 
                : new Color(1f, 1f, 1f, 0f);
        }
    }
}