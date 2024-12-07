using System;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator
{
    public class CalculatorKeyboardInput : MonoBehaviour
    {
        [SerializeField] private CalculatorInput calculatorInput;
        
        private readonly Dictionary<KeyCode, Action> _keyActions = new();

        private void Awake()
        {
            for (int i = 0; i <= 9; i++)
            {
                int digit = i;
                _keyActions[KeyCode.Alpha0 + i] = () => calculatorInput.OnInputButtonPressed(digit.ToString());
                _keyActions[KeyCode.Keypad0 + i] = () => calculatorInput.OnInputButtonPressed(digit.ToString());
            }
            
            _keyActions[KeyCode.Period] = calculatorInput.OnDecimalPointPressed;
            _keyActions[KeyCode.Comma] = calculatorInput.OnDecimalPointPressed;
            
            _keyActions[KeyCode.Plus] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("+");
            _keyActions[KeyCode.KeypadPlus] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("+");
            
            _keyActions[KeyCode.Minus] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("-");
            _keyActions[KeyCode.KeypadMinus] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("-");
            
            _keyActions[KeyCode.Asterisk] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("*");
            _keyActions[KeyCode.KeypadMultiply] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("*");
            
            _keyActions[KeyCode.Slash] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("/");
            _keyActions[KeyCode.KeypadDivide] = () => calculatorInput.OnArithmeticalOperatorButtonPressed("/");
            
            _keyActions[KeyCode.Equals] = calculatorInput.OnEqualButtonPressed;
            _keyActions[KeyCode.KeypadEnter] = calculatorInput.OnEqualButtonPressed;
            
            _keyActions[KeyCode.Backspace] = calculatorInput.OnBackButtonPressed;
            _keyActions[KeyCode.C] = calculatorInput.OnClearButtonPressed;
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