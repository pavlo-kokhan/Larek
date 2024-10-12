using UnityEngine;

namespace Calculator
{
    public class CalculatorKeyboardInput : MonoBehaviour
    {
        [SerializeField] private CalculatorInput calculatorInput;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    calculatorInput.OnInputButtonPressed(i.ToString());
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.Comma))
            {
                calculatorInput.OnDecimalPointPressed();
            }
            
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                calculatorInput.OnArithmeticalOperatorButtonPressed("+");
            }
            
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                calculatorInput.OnArithmeticalOperatorButtonPressed("-");
            }
            
            if (Input.GetKeyDown(KeyCode.Asterisk) || Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                calculatorInput.OnArithmeticalOperatorButtonPressed("*");
            }
            
            if (Input.GetKeyDown(KeyCode.Slash) || Input.GetKeyDown(KeyCode.KeypadDivide))
            {
                calculatorInput.OnArithmeticalOperatorButtonPressed("/");
            }
            
            if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                calculatorInput.OnEqualButtonPressed();
            }
            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                calculatorInput.OnBackButtonPressed();
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                calculatorInput.OnClearButtonPressed();
            }
        }
    }
}