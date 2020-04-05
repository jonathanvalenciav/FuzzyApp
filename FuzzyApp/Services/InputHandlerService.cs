using FuzzyApp.Commons;
using FuzzyApp.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FuzzyApp.Services
{
    public class InputHandlerService
    {
        public const string PREPOSITION_EXPRESSION = "^[a-z]$";
        public const string OPERATOR_EXPRESSION = "[&|=>]";
        public const char CLOSING_GROUPER_SYMBOL = ')';
        public const int MAX_PREPOSITIONS_ALLOWED = 3;

        public static void processInput(string input)
        {
            string standardizedInput = standardizeInput(input);
            string postfixExpression = convertInfixToPostfix(standardizedInput);
            evaluatePostfixExpression(postfixExpression);
            //int amountPrepositions = prepositions.Count;
            //bool isValidAmountPrepositions = amountPrepositions <= MAX_PREPOSITIONS_ALLOWED ? true : false;

            System.Web.HttpContext.Current.Session["Postfix"] = postfixExpression;
        }

        public static string standardizeInput(string input)
        {
            return input.Replace(Constants.BLANK_SPACE, Constants.EMPTY_STRING);
        }

        public static List<char> getOnlyPrepositions(string input)
        {
            string expressionWithoutOperators = Regex.Replace(input, OPERATOR_EXPRESSION, Constants.EMPTY_STRING);
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
            int razon = 0;
            int thurthValuesAmount = 0;
            int cont = 1;
            int i = 0;
            int div = 2;

            thurthValuesAmount = Convert.ToInt32(Math.Pow(2, prepositionsIdentifiers.Count));

            foreach(char prepositionIdentifier in prepositionsIdentifiers) {
                Preposition preposition = new Preposition(prepositionIdentifier);
                i = 0;
                cont = 1;
                razon = thurthValuesAmount / div;
                do
                {
                    for (int fil = i; fil < i + razon; fil++)
                    {
                        thurthValue = cont % 2 != 0 ? true : false;
                        preposition.pushThurthValue(thurthValue);
                    }
                    cont++;
                    i += razon;
                } while (i < thurthValuesAmount);

                prepositions.Add(preposition);
                div *= 2;
            }

            return prepositions;
        }

        public static string evaluatePostfixExpression(string postfixExpression)
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
                        //OPERAR ENTRE PREPOSICIONES DE LA PILA
                        break;
                    case SymbolType.PREPOSITION:
                        Preposition preposition = prepositions.Find(currentPreposition => currentPreposition.GetIdentifier().Equals(symbol));
                        evaluateStack.push(preposition);
                        break;
                }
            }
            return "";
        }
    }
}