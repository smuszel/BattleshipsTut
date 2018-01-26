using System;
using System.Linq;

namespace bships
{
    public class Board
    {
        private char[] core;

        public Board()
        {
            this.core = new char[Constants.boardSize2];
            for (int i=0; i<this.core.Length; i++)
            {
                this.core[i] = Constants.symVacant;
            }
        }

        private bool IsSymbol(int coord, char symbol)
        {
            return this.core[coord] == symbol;
        }

        private bool IsCoordValidForSymbol(int coord, char symbol)
        {
            bool targetedSame = IsSymbol(coord, symbol);
            bool targetedMiss = IsSymbol(coord, Constants.symMiss);
            return !(targetedSame || targetedMiss);
        }


        private void ChangeIfMiss(int coord, ref char symbol)
        {
            if (symbol == Constants.symHit && IsSymbol(coord, Constants.symVacant))
            {
                symbol = Constants.symMiss;
            }
        }

        private void CheckIfBoardDead(Player opponent)
        {
            if (this.core.All(item => item != Constants.symShip)) // && this.core.Any(item => item == 'X')
            {
                Console.Clear();
                OnDisplay(true);
                Console.WriteLine("Player " + opponent.name + " won the match!");
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }

        //subscriber
        public void OnChangeState(Player source, ActionEventArgs args)
        {
            if (IsCoordValidForSymbol(args.coord, args.symbol))
            {
                args.cancelled = false;
                ChangeIfMiss(args.coord, ref args.symbol);

                this.core[args.coord] = args.symbol;
                CheckIfBoardDead(source);
            }
            else 
            {
                args.cancelled = true;
            }
        }

        public void OnDisplay(bool mask)
        {
            for (int i=0; i<Constants.boardSize2; i++)
            {
                if (i%Constants.boardSize == 0 && i !=0)
                {
                    Console.WriteLine('\n');
                }

                if (mask && IsSymbol(i, Constants.symShip))
                {
                    Console.Write(Constants.symVacant.ToString() + " ");
                }

                else 
                {
                    Console.Write(this.core[i] + " ");
                }

                if (i==Constants.boardSize2-1)
                {
                    Console.WriteLine("\n\n\n");
                }
            }
        }
    }
}