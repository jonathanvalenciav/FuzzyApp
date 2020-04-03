using System.Collections.Generic;

namespace ComplexApp.Models
{
    public class Sequences
    {

        public Sequences()
        {
            this.Elements = new List<double>();
            this.ElementsReverse = new List<double>();
        }

        public int Id { get; set; }
        public List<double> Elements { get; set; }
        public List<double> ElementsReverse { get; set; }

    }
}