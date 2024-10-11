using System;
using UnityEngine;

namespace Core
{
    public class PanelController : MonoBehaviour, IPanelController
    {
        public event Action<bool> PanelActivationChanged;

        private void OnEnable()
        {
            PanelActivationChanged?.Invoke(true);
        }

        private void OnDisable()
        {
            PanelActivationChanged?.Invoke(false);
        }
    }
}