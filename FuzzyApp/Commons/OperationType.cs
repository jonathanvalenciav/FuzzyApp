using System;

namespace FuzzyApp.Commons
{
    public static class OperationType
    {
        public enum Operation
        {
            AND = Constants.OPERATOR_AND,
            OR = Constants.OPERATOR_OR,
            IMPLICATION = Constants.OPERATOR_IMPLICATION,
            DOUBLE_IMPLICATION = Constants.OPERATOR_DOUBLE_IMPLICATION,
            UNDEFINED
        }

        public static Operation GetOperationType(char symbol)
        {
            switch (symbol)
            {
                case Constants.OPERATOR_AND:
                    return Operation.AND;
                case Constants.OPERATOR_OR:
                    return Operation.OR;
                case Constants.OPERATOR_IMPLICATION:
                    return Operation.IMPLICATION;
                case Constants.OPERATOR_DOUBLE_IMPLICATION:
                    return Operation.DOUBLE_IMPLICATION;
                default:
                    return Operation.UNDEFINED;
            }
        }
    }
}