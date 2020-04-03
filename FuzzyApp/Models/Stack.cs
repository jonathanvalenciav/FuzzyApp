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
        
        public Stack(int size)
        {
            this.size = size;
            this.top = 0;
            stack = new List<char>();
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

        public char pop()
        {
            return (top <= 0) ? Constants.ZERO_ELEMENT : stack[--top];
        }

        public string getAllItems()
        {
            string stackedItems = Constants.EMPTY_STRING;
            this.stack.Reverse();

            foreach(char item in this.stack.Where(stackItem => !stackItem.Equals(Constants.EMPTY_STRING)))
            {
                stackedItems += item;
            }

            this.stack.Clear();

            return stackedItems;
        }

    }
}