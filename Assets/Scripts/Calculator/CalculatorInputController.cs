using TMPro;
using UnityEngine;

namespace Calculator
{
    public class CalculatorInputController : MonoBehaviour
    {
        [SerializeField] private TMP_Text resultDisplay;
        [SerializeField] private int maxCharactersCount = 10;
        private string _currentInput = "";
        private string _operator = "";
        private float _firstNumber;
        private float _memory;

        public void OnNumberButtonPressed(string number)
        {
            if (_currentInput.Length < maxCharactersCount)
            {
                _currentInput += number;
            }
            
            resultDisplay.text = _currentInput;
        }
        
        public void OnOperatorButtonPressed(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _firstNumber = float.Parse(_currentInput);
                _currentInput = "";
                resultDisplay.text = _currentInput;
                _operator = operatorSymbol;
            }
        }
        
        public void OnEqualButtonPressed()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                float secondNumber = float.Parse(_currentInput);
                float result = 0f;

                switch (_operator)
                {
                    case "+":
                        result = _firstNumber + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - secondNumber;
                        break;
                    case "*":
                        result = _firstNumber * secondNumber;
                        break;
                    case "/":
                        result = secondNumber != 0 ? _firstNumber / secondNumber : 0;
                        break;
                    case "sqrt":
                        result = Mathf.Sqrt(float.Parse(resultDisplay.text));
                        break;
                    case "%":
                        result = _firstNumber * (secondNumber / 100f);
                        break;
                    case "MU":
                        result = _firstNumber * ((secondNumber + 100f) / 100f);
                        break;
                    case "MR":
                        result = _memory;
                        break;
                    case "M+":
                        _memory += secondNumber;
                        result = _memory;
                        break;
                    case "M-":
                        _memory -= secondNumber;
                        result = _memory;
                        break;
                }

                _currentInput = result.ToString();
                resultDisplay.text = _currentInput;
            }
        }
        
        public void OnDecimalPointPressed()
        {
            if (!_currentInput.Contains(","))
            {
                _currentInput += ",";
                resultDisplay.text = _currentInput;
            }
        }
        
        public void OnClearButtonPressed()
        {
            _currentInput = "";
            _firstNumber = 0;
            _operator = "";
            resultDisplay.text = "0";
        }
        
        public void OnMemoryClearButtonPressed()
        {
            _memory = 0;
        }
    }
}