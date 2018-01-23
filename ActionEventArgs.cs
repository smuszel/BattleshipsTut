using System;

namespace bships
{
    public class ActionEventArgs : EventArgs 
    {
        public int coord;
        public char symbol;
        public bool cancelled = true;

        public ActionEventArgs(char symbol)
        {
            this.symbol = symbol;
        }
        
    }
    
}