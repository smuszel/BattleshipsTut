using System;

namespace bships
{
    public class Player 
    {
        public string name;
        public MyEventArgs args;

        public delegate void OnShootHandler(object source, MyEventArgs args);
        public event OnShootHandler Shoot;

        public delegate void OnShipSubmitHandler(object source, MyEventArgs args);
        public event OnShipSubmitHandler ShipSubmit;

        public delegate void OnDisplayHandler(Player source, MyEventArgs args);
        public event OnDisplayHandler DisplayOwn;
        public event OnDisplayHandler DisplayOpponent;


        public Player(string name)
        { 
            this.name = name;
            this.args = new MyEventArgs();
        }

        public void Deployment()
        {
            for (int i=0; i<GlobVars.numOfShips; i++)
            {
                Console.WriteLine("This is our board admiral " + this.name);
                OnDisplayOwn();
                Console.WriteLine("What coord to deploy admiral?");                
                MakeAction("deploy");
                Console.Clear();
            }

            OnDisplayOwn();
            Console.WriteLine("Admiral! Our deployment phase has finished.");
            Console.ReadLine();

        }

        public void Bombardment()
        {
            Console.WriteLine("This is our board admiral " + this.name);
            OnDisplayOwn();
            Console.WriteLine("This is shooting board");
            OnDisplayOpponent();
            Console.WriteLine("What coord to bombard admiral?");
            MakeAction("fire");

            Console.Clear();
            OnDisplayOpponent();
            Console.WriteLine("Admiral! Our bombardment round has finished.");
            Console.ReadLine();
        }

        public void MakeAction(string action)
        {
            int coord = Prompter.PromptCoord();

            try
            {
                switch (action)
                {
                    case "fire":
                        OnShoot(coord);
                        break;

                    case "deploy":
                        OnDeploy(coord);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Cant select used coord admiral. Try another one.");
                MakeAction(action);
            }
        }        

        // public void GetDamaged(int coord)
        // {
        //     if (this.board.IsSymbol(coord, '$'))
        //     {
        //         this.board.ChangeStateOnCoord(coord, 'X');
        //         this.opponent.Display();
        //         Console.WriteLine("Spang on the target!");
        //         Console.ReadLine();
        //     }
            
        //     else
        //     {
        //         this.board.ChangeStateOnCoord(coord, ' ');
        //         this.opponent.Display();
        //         Console.WriteLine("Massive barrage missed the target.");
        //         Console.ReadLine();
        //     }
        // }

        //subscriber
        public void OnOpponentKill(Board source, EventArgs args)
        {
            Console.Clear();
            OnDisplayOpponent();
            Console.WriteLine("Player " + this.name + " won the match!");
            Console.ReadLine();
            System.Environment.Exit(1);
        }

        // publisher
        protected virtual void OnShoot(int coord)
        {
            this.args.coord = coord;
            this.args.symbol = GlobVars.symHit;
            Shoot(this, this.args);
        }

        protected virtual void OnDeploy(int coord)
        {
            this.args.coord = coord;
            this.args.symbol = GlobVars.symShip;
            ShipSubmit(this, this.args);
        }

        protected virtual void OnDisplayOwn()
        {
            this.args.mask = false;
            DisplayOwn(this, this.args);
        }

        protected virtual void OnDisplayOpponent()
        {
            this.args.mask = true;
            DisplayOpponent(this, this.args);
        }

    }
}
