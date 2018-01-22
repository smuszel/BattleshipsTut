using System;

namespace bships
{
    public class MyEventArgs : EventArgs 
    {
        public bool mask;

        public int coord;
        public char symbol;
        public bool cancelled = true;

        public MyEventArgs(char symbol)
        {
            this.symbol = symbol;
        }
        
        public MyEventArgs()
        {

        }
    }
    
}