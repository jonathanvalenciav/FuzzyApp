using FuzzyApp.Commons;
using FuzzyApp.Models;
using System.Collections.Generic;

namespace ComplexApp.Services
{
    public class ClassicsSetsService
    {
        public static Preposition operatorAnd((Preposition, Preposition) prepositions)
        {
            Preposition evaluationResult = new Preposition(Constants.RESULT_OPERATION_PARTIAL);
            int truthValuesAmount = prepositions.Item1.GetAllTruthValues().Count;

            for (int position = 0; position < truthValuesAmount; position++)
            {
                evaluationResult.pushThurthValue(prepositions.Item1.GetTruthValueByPosition(position) && prepositions.Item2.GetTruthValueByPosition(position));
            }

            return evaluationResult;
        }

        public static Preposition operatorOr((Preposition, Preposition) prepositions)
        {
            Preposition evaluationResult = new Preposition(Constants.RESULT_OPERATION_PARTIAL);
            int truthValuesAmount = prepositions.Item1.GetAllTruthValues().Count;

            for (int position = 0; position < truthValuesAmount; position++)
            {
                evaluationResult.pushThurthValue(prepositions.Item1.GetTruthValueByPosition(position) || prepositions.Item2.GetTruthValueByPosition(position));
            }

            return evaluationResult;
        }

        public static Preposition operatorImplication((Preposition, Preposition) prepositions)
        {
            Preposition evaluationResult = new Preposition(Constants.RESULT_OPERATION_PARTIAL);
            int truthValuesAmount = prepositions.Item1.GetAllTruthValues().Count;
            bool truthValueResult;

            for (int position = 0; position < truthValuesAmount; position++)
            {
                truthValueResult = (prepositions.Item1.GetTruthValueByPosition(position).Equals(true) & prepositions.Item2.GetTruthValueByPosition(position).Equals(false)) ? false : true;
                evaluationResult.pushThurthValue(truthValueResult);
            }

            return evaluationResult;
        }
        public static Preposition operatorDoubleImplication((Preposition, Preposition) prepositions)
        {
            Preposition evaluationResult = new Preposition(Constants.RESULT_OPERATION_PARTIAL);
            int truthValuesAmount = prepositions.Item1.GetAllTruthValues().Count;
            bool truthValueResult;

            for (int position = 0; position < truthValuesAmount; position++)
            {
                truthValueResult = (prepositions.Item1.GetTruthValueByPosition(position).Equals(prepositions.Item2.GetTruthValueByPosition(position))) ? true : false;
                evaluationResult.pushThurthValue(truthValueResult);
            }

            return evaluationResult;
        }
    }
}