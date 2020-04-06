using System.Collections.Generic;

namespace FuzzyApp.Models
{
    public class Preposition
    {   
        private char identifier;
        private List<bool> truthValues;

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
    }
}