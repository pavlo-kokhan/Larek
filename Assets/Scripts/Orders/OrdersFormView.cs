using Orders.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Orders
{
    public class OrdersFormView : MonoBehaviour
    {
        [SerializeField] private OrderFormInput orderFormInput;
        
        [SerializeField] private Sprite humanPanelSprite;
        [SerializeField] private Sprite monsterPanelSprite;
        [SerializeField] private Sprite humanCornerSprite;
        [SerializeField] private Sprite monsterCornerSprite;
        
        [SerializeField] private Image panelImage;
        [SerializeField] private Image cornerImage;
        [SerializeField] private Image signatureImage;
        
        private void OnEnable()
        {
            orderFormInput.PageTurned += OnPageTurned;
            orderFormInput.OrderConfirmed += OnOrderConfirmed;
        }
        
        private void OnDisable()
        {
            orderFormInput.PageTurned -= OnPageTurned;
            orderFormInput.OrderConfirmed -= OnOrderConfirmed;
            
            SetSignatureImage(false);
        }
        
        private void OnPageTurned(bool isHumanPage)
        {
            SetSignatureImage(false);
            
            if (isHumanPage)
            {
                panelImage.sprite = humanPanelSprite;
                cornerImage.sprite = humanCornerSprite;
            }
            else
            {
                panelImage.sprite = monsterPanelSprite;
                cornerImage.sprite = monsterCornerSprite;
            }
        }
        
        private void OnOrderConfirmed(bool isHumanPage)
        {
            SetSignatureImage(true);
        }

        private void SetSignatureImage(bool status)
        {
            if (status)
            {
                signatureImage.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                signatureImage.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}