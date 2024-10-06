using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class DialogPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private TextMeshProUGUI dialogTextComponent;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private Action<bool> _callback;

        public void ShowDialog(string message, Action<bool> callback)
        {
            dialogPanel.SetActive(true);
            
            if (dialogTextComponent != null)
            {
                dialogTextComponent.text = message;
            }
            
            _callback = callback;
            
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(OnYesButton);

            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(OnNoButton);
        }

        private void OnYesButton()
        {
            _callback?.Invoke(true);
            dialogPanel.SetActive(false);
        }

        private void OnNoButton()
        {
            _callback?.Invoke(false);
            dialogPanel.SetActive(false);
        }
    }
}