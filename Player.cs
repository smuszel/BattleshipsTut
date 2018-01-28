using System;
using System.Text.RegularExpressions;

namespace bships
{
    public class Player 
    {
        public string name;
        public delegate (bool, char) RulesDelegate(char targetSymbol, char proposedSymbol);
        RulesDelegate RuleMethod = new RulesDelegate(Config.SymbolChangingRules);
        public Player(string name)
        { 
            this.name = name;
        }

        public void Deploy(char[] board)
        {
            Console.WriteLine("This is our board admiral " + this.name);
            Core.DisplayBoard(board);

            Console.WriteLine("What coord to deploy admiral?");

            while (!Program.TryModifyTile(ref board[PromptCoord(Config.boardSize)], Config.symbolShip))
            {
                Console.WriteLine("This coordinate is occupied!");
            }
            Console.Clear();
            Core.DisplayBoard(board);
            Console.WriteLine("Deployment succesful.");
            Console.ReadLine();
            // Console.WriteLine("Admiral! Our deployment phase has finished.");
        }

        public void Bombard(char[] targetBoard, char[] ownBoard)
        {
            Console.WriteLine("This is our board admiral " + this.name);
            Core.DisplayBoard(ownBoard);
            Console.WriteLine("This is shooting board");
            Core.DisplayBoard(targetBoard, Config.symbolShip, Config.symbolVacant);
            Console.WriteLine("What coord to bombard admiral?");

            while (!Program.TryModifyTile(ref targetBoard[PromptCoord(Config.boardSize)], Config.symbolHit))
            {
                Console.WriteLine("This coord we arleady shot at!");
            }
            
            Console.Clear();
            Core.DisplayBoard(targetBoard, Config.symbolShip, Config.symbolVacant);
            //Console.WriteLine("Spang on target");
            Console.ReadLine();
        }

        public static int PromptCoord(int bound)
        {
            string rawInput = Console.ReadLine();
            string[] rawXY;
            int X = -1, Y = -1;

            Regex regex = new Regex(@"\d+ \d+");
            Match match = regex.Match(rawInput);
            
            if (match.Success)
            {
                rawXY =  match.Value.Split(" ");
                (X, Y) = (Int32.Parse(rawXY[0]), Int32.Parse(rawXY[1]));
            }
            else
            {
                Console.WriteLine("Input not parsable, try passing two integers (\\d \\d)");
                return PromptCoord(bound);
            }

            if (Y < bound && X < bound)
            {
                return X + Y*bound;
            }
            else
            {
                Console.WriteLine("Coordinate out of bounds");
                return PromptCoord(bound);
            }
        }
    }
}
