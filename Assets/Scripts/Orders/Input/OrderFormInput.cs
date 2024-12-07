using System;
using UnityEngine;

namespace Orders.Input
{
    public class OrderFormInput : MonoBehaviour
    {
        public event Action<bool> PageTurned;
        public event Action<bool> OrderConfirmed;

        private bool _isHumanPage = true;
        
        public void OnPageTurned()
        {
            _isHumanPage = !_isHumanPage;
            PageTurned?.Invoke(_isHumanPage);
        }

        public void OnOrderConfirmed()
        {
            OrderConfirmed?.Invoke(_isHumanPage);
        }
    }
}