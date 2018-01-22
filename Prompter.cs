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
    }
}