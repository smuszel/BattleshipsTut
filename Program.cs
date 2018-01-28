using System;
using System.Collections.Generic;
using System.Linq;

namespace bships
{
    public static class Config
    {
        public static readonly int boardSize = 4;
        public static readonly int boardSize2 = 16;
        public static readonly int[] numberOfShipTypes = new int[] {2};

        public static readonly  string p1Name = "A";
        public static readonly  string p2Name = "B";
        public const char symbolMiss = ' ';
        public const char symbolHit = 'X';
        public const char symbolShip = '$';
        public const char symbolVacant = '.';

        public static (bool, char) SymbolChangingRules(char targetSymbol, char proposedSymbol)
        {
            switch (proposedSymbol)
            {
                case symbolShip:
                    if (targetSymbol == symbolVacant) {return (true, symbolShip);}
                    else return (false, symbolShip);

                case symbolHit:
                    if (targetSymbol == symbolVacant) return (true, symbolMiss);
                    else if (targetSymbol == symbolShip) return (true, symbolHit);
                    else return (false, symbolShip);
            }
            throw new Exception();
        }
    }

    public static class Program
    {
        public static bool TryModifyTile(ref char targetTile, char targetSymbol)
        {
            (bool valid, char newSymbol) = Config.SymbolChangingRules(targetTile, targetSymbol);
            
            if (!valid) return false;
            targetTile = newSymbol;
            return true;
        }
        static void Transition(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.ReadLine();
            Console.Clear();
        }
        static void Win(Player winner, char[] board)
        {                    
            Console.Clear();
            Core.DisplayBoard(board);
            Console.WriteLine("Player " + winner.name + " won the match!");
            Console.ReadLine();
            System.Environment.Exit(1);
        }
        static void Main(string[] ar)
        {
            Player[] players = new Player[] 
            {
                new Player(Config.p1Name), 
                new Player(Config.p2Name)
            };

            char[][] boards = new char[][] 
            {
                Core.GenerateBoard(Config.boardSize2, Config.symbolVacant), 
                Core.GenerateBoard(Config.boardSize2, Config.symbolVacant)
            };

            #region DeploymentPhase
            foreach (int numberOfShip in Config.numberOfShipTypes)
            {
                players[0].Deploy(boards[0]);
                players[0].Deploy(boards[0]);
            }

            Transition("Make ready " + players[1].name);
            foreach (int numberOfShip in Config.numberOfShipTypes)
            {
                players[1].Deploy(boards[1]);
                players[1].Deploy(boards[1]);
            }            
            #endregion

            Transition("Firing phase. Make ready " + players[0].name);

            #region ShootingPhase
            while (true)
            {
                players[0].Bombard(boards[1], boards[0]);

                if (Core.CheckIfNoSymbol(boards[1], Config.symbolShip))
                {
                    Win(players[0], boards[1]);
                }

                Array.Reverse(players);
                Array.Reverse(boards);
                Transition("Make ready " + players[0].name);
            }
            #endregion 
        }
    }
}
