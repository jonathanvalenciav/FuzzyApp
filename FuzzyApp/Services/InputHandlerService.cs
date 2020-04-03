using FuzzyApp.Commons;
using FuzzyApp.Models;
using System.Text.RegularExpressions;

namespace FuzzyApp.Services
{
    public class InputHandlerService
    {
        public const string PREPOSITION_EXPRESSION = "^[a-z]$";
        public const string OPERATOR_EXPRESSION = "^[&|=>]$";
        public const char CLOSING_GROUPER_SYMBOL = ')';

        public static void processInput(string input)
        {
            string standardizedInput = standardizeInput(input);
            string postfixExpression = convertInfixToPostfix(standardizedInput);
            System.Web.HttpContext.Current.Session["Postfix"] = postfixExpression;
        }

        public static string standardizeInput(string input)
        {
            return input.Replace(Constants.BLANK_SPACE, Constants.EMPTY_STRING);
        }

        public static string convertInfixToPostfix(string infix) {
            Stack postfixStack = new Stack(20);
            string postfix = Constants.EMPTY_STRING;

            foreach (char symbol in infix)
            {
                SymbolType symbolType = getSymbolType(symbol);

                switch (symbolType)
                {
                    case SymbolType.OPERATOR:
                        postfixStack.push(symbol);
                        break;
                    case SymbolType.PREPOSITION:
                        postfix += symbol;
                        break;
                    case SymbolType.CLOSING_GROUPER:
                        postfix += postfixStack.getAllItems();
                        break;
                    case SymbolType.UNDEFINED:
                        break;
                }
            }
            return postfix += postfixStack.getAllItems();
        }

        public static SymbolType getSymbolType(char symbol)
        {
            SymbolType symbolType = SymbolType.UNDEFINED;

            if (Regex.IsMatch(symbol.ToString(), PREPOSITION_EXPRESSION)) {
                symbolType = SymbolType.PREPOSITION;
            }

            if (Regex.IsMatch(symbol.ToString(), OPERATOR_EXPRESSION))
            {
                symbolType = SymbolType.OPERATOR;
            }

            if (symbol.Equals(CLOSING_GROUPER_SYMBOL))
            {
                symbolType = SymbolType.CLOSING_GROUPER;
            }

            return symbolType;
        }

        public static string evaluatePostfixExpression(string postfix)
        {
            Stack evaluateStack = new Stack(20);
            //AQUI VA CASI TODA LA LÓGICA
            foreach (char symbol in postfix)
            {
                SymbolType symbolType = getSymbolType(symbol);

                switch (symbolType)
                {
                    case SymbolType.OPERATOR:
                        //EVALUATE
                        break;
                    case SymbolType.PREPOSITION:
                        evaluateStack.push(symbol);
                        break;
                }
            }
            return "";
        }
    }
}