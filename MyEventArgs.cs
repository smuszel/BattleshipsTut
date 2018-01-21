using System;

namespace bships
{
    public class MyEventArgs : EventArgs 
    {
        public int coord;
        public char symbol;
        public bool mask;
        public const dynamic empty = null;

    }
    
}