using System;

namespace bships
{
   public static class Prompter
    {

        private static int ParseRawCoord(string[] rawXY)
        {
            (int X, int Y) = (Int32.Parse(rawXY[0]), Int32.Parse(rawXY[1]));
            
            if (X >= GlobVars.boardSize || Y >= GlobVars.boardSize)
            {
                throw new ArgumentException();
            }
            else if (rawXY.Length !=2)
            {
                throw new ArgumentException();
            }

            return X + Y*GlobVars.boardSize;
        }

        public static int PromptCoord()
        {
            try
            {
                return ParseRawCoord(Console.ReadLine().Split(' '));
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Coordinate out of bounds.");
                return PromptCoord();                
            }
            catch (Exception)
            {
                Console.WriteLine("I only understand coordinates in format 'x y' admiral.");
                return PromptCoord();
            }

        }

        // public static int PromptDeploy(Board board)
        // {
        //     Console.WriteLine("Where do you wish us to deploy admiral?");

        //     int coord = PromptCoord();

        //     if (board.IsSymbol(coord, '.'))
        //     {
        //         return coord;
        //     }
        //     else
        //     {
        //         Console.WriteLine("This tile is occupied. Choose another.");
        //         return PromptDeploy(board);
        //     }
        // }

        // public static int PromptFire(Board board)
        // {
        //     Console.WriteLine("What coord to bombard admiral?");

        //     int coord = PromptCoord();

        //     if (board.IsSymbol(coord, '$') || board.IsSymbol(coord, '.'))
        //     {
        //         return coord;
        //     }
        //     else
        //     {
        //         Console.WriteLine("This coordinate was already shot at. Choose another.");
        //         return PromptFire(board);
        //     }
        // }
    }
}