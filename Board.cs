using System;
using System.Linq;

namespace bships
{
    public class Board
    {
        private char[] core;
        public delegate void OnBoardDeathHandler(Board source, MyEventArgs args);
        public event OnBoardDeathHandler BoardDeath; 
        public MyEventArgs args;

        public Board()
        {
            this.args = new MyEventArgs();
            this.core = new char[GlobVars.boardSize2];
            for (int i=0; i<this.core.Length; i++)
            {
                this.core[i] = GlobVars.symVacant;
            }
        }

        private bool IsSymbol(int coord, char symbol)
        {
            return this.core[coord] == symbol;
        }

        private bool IsCoordValidForSymbol(int coord, char symbol)
        {
            bool targetedSame = IsSymbol(coord, symbol);
            bool targetedMiss = IsSymbol(coord, symbol);
            return !(targetedSame || targetedMiss);

        }

        private void CheckIfBoardDead()
        {
            if (this.core.All(item => item != GlobVars.symShip)) // && this.core.Any(item => item == 'X')
            {
                OnBoardDeath();
            }
        }

        private void ChangeIfMiss(int coord, ref char symbol)
        {
            if (symbol == GlobVars.symHit && IsSymbol(coord, GlobVars.symVacant))
            {
                symbol = GlobVars.symMiss;
            }
        }

        //subscriber
        public void OnChangeState(object source, MyEventArgs args)
        {
            if (IsCoordValidForSymbol(args.coord, args.symbol))
            {
                args.cancelled = false;
                ChangeIfMiss(args.coord, ref args.symbol);

                this.core[args.coord] = args.symbol;
                CheckIfBoardDead();
            }
            else 
            {
                args.cancelled = true;
            }

        }

        public void OnDisplay(object source, MyEventArgs args)
        {
            for (int i=0; i<GlobVars.boardSize2; i++)
            {
                if (i%GlobVars.boardSize == 0 && i !=0)
                {
                    Console.WriteLine('\n');
                }

                if (args.mask && IsSymbol(i, GlobVars.symShip))
                {
                    Console.Write(GlobVars.symVacant.ToString() + " ");
                }

                else 
                {
                    Console.Write(this.core[i] + " ");
                }

                if (i==GlobVars.boardSize2-1)
                {
                    Console.WriteLine("\n\n\n");
                }
            }
        }

        // publisher
        protected virtual void OnBoardDeath()
        {
            BoardDeath(this, new MyEventArgs());
        }
    }
}