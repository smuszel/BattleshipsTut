using System;
using System.Linq;

namespace bships
{
    public static class Constants
    {
        public const int boardSize = 4;
        public const int boardSize2 = 16;
        public const int numOfShips = 2;

        public const string p1Name = "A";
        public const string p2Name = "B";

        public const char symMiss = ' ';
        public const char symHit = 'X';
        public const char symShip = '$';
        public const char symVacant = '.';
    }

    class Program
    {

        static void Transition(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.ReadLine();
            Console.Clear();
        }

        static void DeploymentPhase(Player p1, Player p2)
        {
            p1.Deployment();
            Transition("Make ready " + Constants.p2Name);
            p2.Deployment();
            Transition("Firing phase. Player " + Constants.p1Name + " starting");
        }

        static void ShootingPhase(Player p1, Player p2)
        {
            Player[] players = new Player[] {p1, p2};

            while (true)
            {
                players[0].Bombardment();
                Array.Reverse(players);
                Transition("Make ready");        
            }
        }

        static void Main(string[] ar)
        {
            Player player1 = new Player(Constants.p1Name);
            Player player2 = new Player(Constants.p2Name);
            Board board1 = new Board();
            Board board2 = new Board();

            player2.DisplayOpponent += board1.OnDisplay;
            player2.DisplayOwn += board2.OnDisplay;
            player1.DisplayOpponent += board2.OnDisplay;
            player1.DisplayOwn += board1.OnDisplay;

            player2.Fire += board1.OnChangeState;
            player2.Deploy += board2.OnChangeState;
            player1.Fire += board2.OnChangeState;
            player1.Deploy += board1.OnChangeState;

            DeploymentPhase(player1, player2);
            ShootingPhase(player1, player2);
        }
    }
}
