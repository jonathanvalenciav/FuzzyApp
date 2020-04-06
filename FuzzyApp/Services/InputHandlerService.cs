using ComplexApp.Services;
using FuzzyApp.Commons;
using FuzzyApp.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static FuzzyApp.Commons.OperationType;

namespace FuzzyApp.Services
{
    public class InputHandlerService
    {
        public const string PREPOSITION_EXPRESSION = "^[a-z]$";
        public const string OPERATOR_EXPRESSION = "[&|=>]";
        public const char CLOSING_GROUPER_SYMBOL = ')';
        public const int MAX_PREPOSITIONS_ALLOWED = 3;
        public const int MAX_USE_PREPOSITIONS_ALLOWED = 6;

        public static bool validateExpression(string input)
        {
            bool isValidExpression;
            string postfixExpression = convertInfixToPostfix(input);
            bool isValidAmountPrepositions = (getOnlyPrepositions(postfixExpression).Count <= MAX_PREPOSITIONS_ALLOWED) ? true : false;
            bool isValidUsePrepositions = (getExpressionWithoutOperators(postfixExpression).Length <= MAX_USE_PREPOSITIONS_ALLOWED) ? true : false;
            bool isValidSymbolTypeInARow = validateSymbolTypeInARow(input);
            isValidExpression = (isValidAmountPrepositions && isValidUsePrepositions && isValidSymbolTypeInARow) ? true : false;

            return isValidExpression;
        }

        public static void processInput(string input)
        {
            string standardizedInput = standardizeInput(input);
            string postfixExpression = convertInfixToPostfix(standardizedInput);            
            evaluatePostfixExpression(postfixExpression);

            System.Web.HttpContext.Current.Session["Postfix"] = postfixExpression;
        }

        public static string standardizeInput(string input)
        {
            return input.Replace(Constants.BLANK_SPACE, Constants.EMPTY_STRING);
        }

        public static bool validateSymbolTypeInARow(string input)
        {
            bool result = true;
            char[] inputTochar = input.ToCharArray();
            int n = 0;
            for(int i=1; i < inputTochar.Length; i++)
            {
                SymbolType symbolType1 = getSymbolType(inputTochar[i-1]);
                SymbolType symbolType2 = getSymbolType(inputTochar[i]);
                if (symbolType1 == symbolType2 && symbolType1 != SymbolType.CLOSING_GROUPER && symbolType2 != SymbolType.CLOSING_GROUPER && symbolType1 != SymbolType.UNDEFINED && symbolType2 != SymbolType.UNDEFINED)
                {
                    n++;
                }
            }
            if (n != 0)
            {
                result = false;
            }
            return result;
        }

        public static List<char> getOnlyPrepositions(string input)
        {
            string expressionWithoutOperators = getExpressionWithoutOperators(input);
            List<char> existingPrepositions = new List<char>();

            foreach (char preposition in expressionWithoutOperators)
            {
                if (!existingPrepositions.Contains(preposition))
                {
                    existingPrepositions.Add(preposition);
                }
            }

            return existingPrepositions;
        }

        public static string getExpressionWithoutOperators(string input) {
            return Regex.Replace(input, OPERATOR_EXPRESSION, Constants.EMPTY_STRING);
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

        public static List<Preposition> getPrepositionsWithThurthValues(List<char> prepositionsIdentifiers)
        {
            List<Preposition> prepositions = new List<Preposition>();
            bool thurthValue;
            int frequency = 0;
            int thurthValuesAmount = 0;
            int row = 1;
            int i = 0;
            int trueNumber = 2;

            thurthValuesAmount = Convert.ToInt32(Math.Pow(2, prepositionsIdentifiers.Count));

            foreach (char prepositionIdentifier in prepositionsIdentifiers)
            {
                Preposition preposition = new Preposition(prepositionIdentifier);
                i = 0;
                row = 1;
                frequency = thurthValuesAmount / trueNumber;
                do
                {
                    for (int fil = i; fil < i + frequency; fil++)
                    {
                        thurthValue = row % 2 != 0 ? true : false;
                        preposition.pushThurthValue(thurthValue);
                    }
                    row++;
                    i += frequency;
                } while (i < thurthValuesAmount);

                prepositions.Add(preposition);
                trueNumber *= 2;
            }

            return prepositions;
        }

        public static void evaluatePostfixExpression(string postfixExpression)
        {
            Stack evaluateStack = new Stack(20);
            List<char> prepositionsIdentifiers = getOnlyPrepositions(postfixExpression);
            List<Preposition> prepositions = getPrepositionsWithThurthValues(prepositionsIdentifiers);

            foreach (char symbol in postfixExpression)
            {
                SymbolType symbolType = getSymbolType(symbol);

                switch (symbolType)
                {
                    case SymbolType.OPERATOR:
                        Preposition prepositionTwo = evaluateStack.pop();
                        Preposition prepositionOne = evaluateStack.pop();
                        evaluateStack.push(operatePreposition(symbol, (prepositionOne, prepositionTwo)));
                        
                        break;
                    case SymbolType.PREPOSITION:
                        Preposition preposition = prepositions.Find(currentPreposition => currentPreposition.GetIdentifier().Equals(symbol));
                        evaluateStack.push(preposition);
                        break;
                }
            }
            System.Web.HttpContext.Current.Session["Result"] = evaluateStack.getLastPreposition();
        }

        public static Preposition operatePreposition(char symbol, (Preposition, Preposition) prepositions)
        {
            Operation operation = GetOperationType(symbol);
           
            switch (operation)
            {
                case Operation.AND:
                    return ClassicsSetsService.operatorAnd(prepositions);
                case Operation.OR:
                    return ClassicsSetsService.operatorOr(prepositions);
                case Operation.IMPLICATION:
                    return ClassicsSetsService.operatorImplication(prepositions);
                case Operation.DOUBLE_IMPLICATION:
                    return ClassicsSetsService.operatorDoubleImplication(prepositions);
                default:
                    return null;
            }
        }
    }
}