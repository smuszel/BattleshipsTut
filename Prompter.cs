using System;
using System.Text.RegularExpressions;

namespace bships
{
   public static class Prompter
    {
        public static int PromptCoord()
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
                return PromptCoord();
            }

            if (Y < Constants.boardSize || X < Constants.boardSize)
            {
                return X + Y*Constants.boardSize;
            }
            else
            {
                Console.WriteLine("Coordinate out of bounds");
                return PromptCoord();
            }
        }

        // public static int Test()
        // {
        //     if (true) {return 0;}
        //     else {return 1;}
        // }
    }
}