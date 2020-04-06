using System.Collections.Generic;

namespace FuzzyApp.Models
{
    public class Preposition
    {   
        private char identifier;
        private List<bool> truthValues;
        private Preposition firstPreposition;
        private Preposition secondPreposition;
        private char operatorResult;        

        public Preposition(char identifier) {
            this.identifier = identifier;
            this.truthValues = new List<bool>();
        }

        public char GetIdentifier()
        {
            return identifier;
        }

        public List<bool> GetAllTruthValues()
        {
            return truthValues;
        }

        public bool GetTruthValueByPosition(int position)
        {
            return truthValues[position];
        }

        public void pushThurthValue(bool thurthValue)
        {
            this.truthValues.Add(thurthValue);
        }

        public void SetOperationSource(Preposition firstPreposition, Preposition secondPreposition, char operatorResult)
        {
            this.firstPreposition = firstPreposition;
            this.secondPreposition = secondPreposition;
            this.operatorResult = operatorResult;
        }

        public char GetOperator()
        {
            return this.operatorResult;
        }

        public Preposition GetFirstPreposition()
        {
            return this.firstPreposition;
        }

        public Preposition GetSecondPreposition()
        {
            return this.secondPreposition;
        }

    }
}