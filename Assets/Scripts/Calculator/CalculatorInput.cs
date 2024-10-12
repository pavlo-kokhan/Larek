using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Calculator
{
    public class CalculatorInput : MonoBehaviour
    {
        [SerializeField] private TMP_Text expressionDisplay;
        [SerializeField] private TMP_Text resultDisplay;
        
        private string _expression = "";
        private float _memory;

        public void OnInputButtonPressed(string input)
        {
            if (IsOperator(input) && (string.IsNullOrEmpty(_expression) || IsOperator(_expression[^1].ToString())))
            {
                return;
            }
            
            _expression += input;
            expressionDisplay.text = _expression;
        }
        
        public void OnArithmeticalOperatorButtonPressed(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(_expression) && !IsOperator(_expression[^1].ToString()))
            {
                _expression += operatorSymbol;
                expressionDisplay.text = _expression;
            }
        }

        public void OnSquareRootButtonPressed()
        {
            if (!string.IsNullOrEmpty(_expression))
            {
                _expression = $"sqrt({_expression})";
                expressionDisplay.text = _expression;
            }
        }
        
        public void OnEqualButtonPressed()
        {
            string result = CalculateResult(_expression);
            resultDisplay.text = "= " + result;
        }

        public void OnBackButtonPressed()
        {
            if (_expression.Length > 0)
            {
                _expression = _expression.Remove(_expression.Length - 1);
                expressionDisplay.text = string.IsNullOrEmpty(_expression) ? "0" : _expression;
            }
        }

        public void OnDecimalPointPressed()
        {
            string[] parts = _expression.Split(new char[] { '+', '-', '*', '/' });
            if (!parts[^1].Contains("."))
            {
                _expression += ".";
                expressionDisplay.text = _expression;
            }
        }
        
        public void OnClearButtonPressed()
        {
            ClearExpression();
            resultDisplay.text = "=";
        }
        
        private void ClearExpression()
        {
            _expression = "";
            expressionDisplay.text = "0";
        }

        public void OnMemoryClearButtonPressed()
        {
            _memory = 0;
        }

        public void OnMemoryRecallButtonPressed()
        {
            _expression = _memory.ToString(CultureInfo.InvariantCulture);
            expressionDisplay.text = _expression;
        }

        public void OnMemoryAddButtonPressed()
        {
            string result = CalculateResult(_expression);
            
            if (float.TryParse(result, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedResult))
            {
                _memory += parsedResult;
            }
        }

        public void OnMemorySubtractButtonPressed()
        {
            string result = CalculateResult(_expression);
            
            if (float.TryParse(result, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedResult))
            {
                _memory -= parsedResult;
            }
        }

        private string CalculateResult(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                string processedExpression = PreprocessExpression(expression);
                string result = EvaluateExpression(processedExpression);
                result = result.Replace(",", ".");

                return result;
            }
            
            return string.Empty;
        }
        
        private string PreprocessExpression(string expression)
        {
            string pattern = @"sqrt\((.*?)\)";
            MatchEvaluator evaluator = match =>
            {
                string innerExpression = match.Groups[1].Value;
                string innerResult = EvaluateExpression(innerExpression.Replace(",", "."));
                if (float.TryParse(innerResult, out float number))
                {
                    return Math.Sqrt(number).ToString().Replace(",", ".");
                }
                return "Error";
            };

            return Regex.Replace(expression.Replace(",", "."), pattern, evaluator);
        }

        private string EvaluateExpression(string expression)
        {
            string formattedExpression = expression.Replace(",", ".");

            DataTable table = new DataTable();
    
            try
            {
                table.Columns.Add("expression", typeof(string), formattedExpression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                var result = row["expression"];
                return result.ToString();
            }
            catch (Exception)
            {
                return "Error";
            }
        }
        
        private bool IsOperator(string input)
        {
            return input == "+" || input == "-" || input == "*" || input == "/";
        }
    }
}