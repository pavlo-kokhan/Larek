using System;
using TMPro;
using UnityEngine;

namespace Notebook
{
    public class NotebookInput : MonoBehaviour
    {
        public event Action PageTurned;
        
        [SerializeField] private TMP_InputField leftPageInputField;
        [SerializeField] private TMP_InputField rightPageInputField;
        [SerializeField] private TMP_Text leftPageNumber;
        [SerializeField] private TMP_Text rightPageNumber;
        [SerializeField] private float pageTurnCooldown;

        private const int TotalPages = 36;
        private readonly string[] _pages = new string[TotalPages];
        private int _currentPageIndex;
        private float _pageTurnTimer;
        
        private void Start()
        {
            _pageTurnTimer = pageTurnCooldown;
            
            for (int i = 0; i < TotalPages; i++)
            {
                _pages[i] = "";
            }

            UpdatePageText();

            leftPageInputField.text = "Hello! You can type here whatever you want";

            leftPageInputField.textComponent.enableWordWrapping = true;
            rightPageInputField.textComponent.enableWordWrapping = true;
        }

        private void Update()
        {
            _pageTurnTimer += Time.deltaTime;
        }

        public void OnNextButtonPressed()
        {
            if (_currentPageIndex < TotalPages - 2)
            {
                if (_pageTurnTimer >= pageTurnCooldown)
                {
                    TurnPage(2); 
                } 
            }
            else
            {
                Debug.Log("Last pages");
            }
        }

        public void OnPreviousButtonPressed()
        {
            if (_currentPageIndex > 0 && _pageTurnTimer >= pageTurnCooldown)
            {
                if (_pageTurnTimer >= pageTurnCooldown)
                {
                    TurnPage(-2); 
                }   
            }
            else
            {
                Debug.Log("First pages");
            }
        }

        private void TurnPage(int indexChange)
        {
            SaveCurrentPages();
            _currentPageIndex += indexChange;
            UpdatePageText();
            _pageTurnTimer = 0f;
            PageTurned?.Invoke();
        }
        
        private void SaveCurrentPages()
        {
            _pages[_currentPageIndex] = leftPageInputField.text;
            _pages[_currentPageIndex + 1] = rightPageInputField.text;
        }
        
        private void UpdatePageText()
        {
            leftPageInputField.text = _pages[_currentPageIndex];
            rightPageInputField.text = _pages[_currentPageIndex + 1];

            leftPageNumber.text = (_currentPageIndex + 1).ToString();
            rightPageNumber.text = (_currentPageIndex + 2).ToString();
        }
    }
}