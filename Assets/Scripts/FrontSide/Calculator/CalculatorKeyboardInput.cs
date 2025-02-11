using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrontSide.Calculator
{
    public class CalculatorKeyboardInput : MonoBehaviour
    {
        [SerializeField] private CalculatorInput _calculatorInput;
        
        private readonly Dictionary<KeyCode, Action> _keyActions = new();

        private void Awake()
        {
            for (int i = 0; i <= 9; i++)
            {
                int digit = i;
                _keyActions[KeyCode.Alpha0 + i] = () => _calculatorInput.OnInputButtonPressed(digit.ToString());
                _keyActions[KeyCode.Keypad0 + i] = () => _calculatorInput.OnInputButtonPressed(digit.ToString());
            }
            
            _keyActions[KeyCode.Period] = _calculatorInput.OnDecimalPointPressed;
            _keyActions[KeyCode.Comma] = _calculatorInput.OnDecimalPointPressed;
            
            _keyActions[KeyCode.Plus] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("+");
            _keyActions[KeyCode.KeypadPlus] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("+");
            
            _keyActions[KeyCode.Minus] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("-");
            _keyActions[KeyCode.KeypadMinus] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("-");
            
            _keyActions[KeyCode.Asterisk] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("*");
            _keyActions[KeyCode.KeypadMultiply] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("*");
            
            _keyActions[KeyCode.Slash] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("/");
            _keyActions[KeyCode.KeypadDivide] = () => _calculatorInput.OnArithmeticalOperatorButtonPressed("/");
            
            _keyActions[KeyCode.Equals] = _calculatorInput.OnEqualButtonPressed;
            _keyActions[KeyCode.KeypadEnter] = _calculatorInput.OnEqualButtonPressed;
            
            _keyActions[KeyCode.Backspace] = _calculatorInput.OnBackButtonPressed;
            _keyActions[KeyCode.C] = _calculatorInput.OnClearButtonPressed;
        }

        private void Update()
        {
            foreach (var action in _keyActions)
            {
                if (Input.GetKeyDown(action.Key))
                {
                    action.Value.Invoke();
                }
            }
        }
    }
}