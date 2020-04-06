using FuzzyApp.Commons;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyApp.Models
{
    public class Stack
    {   
        private int size;
        private int top;
        private List<char> stack;
        private List<Preposition> evaluationStack;
        
        public Stack(int size)
        {
            this.size = size;
            this.top = 0;
            stack = new List<char>();
            evaluationStack = new List<Preposition>();
        }

        public bool isFull()
        {
            return top.Equals(size);
        }

        public bool isEmpty()
        {
            return top.Equals(0);
        }

        public void push(char item)
        {
            this.stack.Add(item);
        }

        public void push(Preposition item)
        {
            this.evaluationStack.Insert(top, item);
            top++;
        }

        public Preposition pop()
        {
            return (top <= 0) ? null : evaluationStack[--top];
        }

        public Preposition getLastPreposition()
        {
            return evaluationStack[top - 1];
        }

        public string getAllItems()
        {
            string stackedItems = Constants.EMPTY_STRING;
            this.stack.Reverse();

            foreach(char item in this.stack.Where(stackItem => !stackItem.Equals(Constants.EMPTY_STRING)))
            {
                stackedItems += item;
            }

            this.top = 0;
            this.stack.Clear();

            return stackedItems;
        }

    }
}