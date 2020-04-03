using ComplexApp.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ComplexApp.Services
{
    public class SequencesServices
    {
        public const char SEPARATOR_EXPONENT = '^';
        public const string EMPTY_STRING = "";

        Sequences globalSequence = (Sequences)System.Web.HttpContext.Current.Session["Sequences"];
        Sequences globalSequenceReverse = (Sequences)System.Web.HttpContext.Current.Session["SequencesReverse"];
        string patternExponent = @"^[+-]?(?:\d+\.\d+|\d+)(?:\^[+-]?(?:\d+\.\d+|\d+)|)$";
        string patternSquareRoot = @"^(?:sqrt[(](?:\d+\.\d+|\d+)[)])$";

        public bool validateInput(string value) {
            return Regex.IsMatch(value, patternExponent) || Regex.IsMatch(value, patternSquareRoot);
        }

        public double getValueFromInput(string inputString)
        {
            double value;
            if (Regex.IsMatch(inputString, patternExponent))
            {
                value = calculateExponent(inputString);
            }
            else if (Regex.IsMatch(inputString, patternSquareRoot))
            {
                value = calculateSquareRoot(inputString);
            }
            else {
                value = double.Parse(inputString);
            }

            return value;
        }

        public double calculateExponent(string exponent)
        {
            exponent = exponent.Replace(".", ",");
            string[] values = exponent.Split(SEPARATOR_EXPONENT);
            double value;

            if (values.Length.Equals(2))
            {
                value = Math.Pow(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]));
            }
            else
            {
                value = double.Parse(values.First());
            }

            return Math.Round(value, 3);
        }

        public double calculateSquareRoot(string squareRoot)
        {
            squareRoot = squareRoot.Replace(".", ",");
            string valueFromString = squareRoot.Replace("sqrt(", EMPTY_STRING).Replace(")", EMPTY_STRING);
            double value = Math.Sqrt(Convert.ToDouble(valueFromString));

            return Math.Round(value, 3);
        }

        public void addElementToSequence(double element)
        {
            globalSequence.Elements.Add(element);
            globalSequence.Elements.Sort();
            globalSequenceReverse.ElementsReverse.Add(element);
            globalSequenceReverse.ElementsReverse.Sort();
            globalSequenceReverse.ElementsReverse.Reverse();
        }

        public void removeElementFromSequence(int index)
        {
            globalSequence.Elements.RemoveAt(index);
            int size = globalSequence.Elements.Count();
            globalSequenceReverse.ElementsReverse.RemoveAt(size - index);
        }

    }
}