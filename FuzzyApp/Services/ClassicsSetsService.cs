using System.Collections.Generic;

namespace ComplexApp.Services
{
    public class ClassicsSetsService
    {
        public static List<bool> operatorAnd((List<bool>, List<bool>) prepositions)
        {
            List<bool> evaluationResult = new List<bool>();

            for (int position = 0; position < prepositions.Item1.Count; position++)
            {
                evaluationResult.Add(prepositions.Item1[position] & prepositions.Item2[position]);
            }

            return evaluationResult;
        }

        public static List<bool> operatorOr((List<bool>, List<bool>) prepositions)
        {
            List<bool> evaluationResult = new List<bool>();

            return null;
        }

        public static List<bool> operatorImplication((List<bool>, List<bool>) prepositions)
        {
            List<bool> evaluationResult = new List<bool>();

            return null;
        }
        public static List<bool> operatorDoubleImplication((List<bool>, List<bool>) prepositions)
        {
            List<bool> evaluationResult = new List<bool>();

            return null;
        }
    }
}