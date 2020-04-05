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

        public void pushThurthValue(bool thurthValue)
        {
            this.truthValues.Add(thurthValue);
        }
    }
}