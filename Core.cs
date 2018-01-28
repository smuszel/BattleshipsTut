using System;
using System.Linq;

namespace bships
{
    public class Core
    {
        public static char[] GenerateBoard(int size, char vacant)
        {
            return new char[size].Select(c => vacant).ToArray();
        }
        public static bool CheckIfNoSymbol(char[] board, char target)
        {
            return board.All(item => item != target);            
        }
        public static void DisplayBoard(char[] board, char symbolToMask, char symbolOfMask)
        {
            for (int i=0; i<board.Length; i++)
            {
                if (i%(int)Math.Sqrt(board.Length) == 0 && i !=0)
                {
                    Console.WriteLine('\n');
                }

                if (board[i] == symbolToMask)
                {
                    Console.Write(symbolOfMask.ToString() + " ");
                }
                else 
                {
                    Console.Write(board[i] + " ");
                }
            }
            Console.WriteLine("\n\n\n");
        }
        public static void DisplayBoard(char[] board)
        {
            for (int i=0; i<board.Length; i++)
            {
                if (i%(int)Math.Sqrt(board.Length) == 0 && i !=0)
                {
                    Console.WriteLine('\n');
                }
                Console.Write(board[i] + " ");
            }
            Console.WriteLine("\n\n\n");
        }
    }
}