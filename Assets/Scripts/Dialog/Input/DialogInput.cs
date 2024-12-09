using TMPro;
using UnityEngine;

namespace Dialog.Input
{
    public class DialogInput : MonoBehaviour
    {
        [SerializeField] private TMP_Text _displayMessage;

        private string _currentMessage;
        private string _previousMessage;

        public void OnNextButtonPressed()
        {
            Debug.Log("Next");
        }

        public void OnPreviousButtonPressed()
        {
            Debug.Log("Previous");
        }
        
        public void OnHideButtonPressed()
        {
            Debug.Log("Hide");
        }
    }
}
