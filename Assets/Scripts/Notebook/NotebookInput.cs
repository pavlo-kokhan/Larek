using System;
using TMPro;
using UnityEngine;

namespace Notebook
{
    public class NotebookInput : MonoBehaviour
    {
        public event Action PageTurned;
        
        [SerializeField] private TMP_InputField _leftPageInputField;
        [SerializeField] private TMP_InputField _rightPageInputField;
        
        [SerializeField] private TMP_Text _leftPageNumber;
        [SerializeField] private TMP_Text _rightPageNumber;
        
        [SerializeField] private float _pageTurnCooldown = 0.5f;

        private const int TotalPages = 36;
        private readonly string[] _pages = new string[TotalPages];
        
        private int _currentPageIndex;
        private float _pageTurnTimer;
        
        private void Start()
        {
            _pageTurnTimer = _pageTurnCooldown;
            
            for (int i = 0; i < TotalPages; i++)
            {
                _pages[i] = "";
            }

            UpdatePageText();

            _leftPageInputField.text = "Художник лох";

            _leftPageInputField.textComponent.enableWordWrapping = true;
            _rightPageInputField.textComponent.enableWordWrapping = true;
        }

        private void Update()
        {
            _pageTurnTimer += Time.deltaTime;
        }

        public void OnNextButtonPressed()
        {
            if (_currentPageIndex < TotalPages - 2)
            {
                if (_pageTurnTimer >= _pageTurnCooldown)
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
            if (_currentPageIndex > 0 && _pageTurnTimer >= _pageTurnCooldown)
            {
                if (_pageTurnTimer >= _pageTurnCooldown)
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
            _pages[_currentPageIndex] = _leftPageInputField.text;
            _pages[_currentPageIndex + 1] = _rightPageInputField.text;
        }
        
        private void UpdatePageText()
        {
            _leftPageInputField.text = _pages[_currentPageIndex];
            _rightPageInputField.text = _pages[_currentPageIndex + 1];

            _leftPageNumber.text = (_currentPageIndex + 1).ToString();
            _rightPageNumber.text = (_currentPageIndex + 2).ToString();
        }
    }
}