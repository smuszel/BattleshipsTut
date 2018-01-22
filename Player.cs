using System;

namespace bships
{
    public class Player 
    {
        public string name;
        public MyEventArgs args;

        public delegate void OnShootHandler(object source, MyEventArgs args);
        public event OnShootHandler Fire;

        public delegate void OnShipSubmitHandler(object source, MyEventArgs args);
        public event OnShipSubmitHandler Deploy;

        public delegate void OnDisplayHandler(Player source, MyEventArgs args);
        public event OnDisplayHandler DisplayOwn;
        public event OnDisplayHandler DisplayOpponent;


        public Player(string name)
        { 
            this.name = name;
        }

        public void Deployment()
        {
            for (int i=0; i<GlobVars.numOfShips; i++)
            {
                Console.WriteLine("This is our board admiral " + this.name);
                OnDisplayOwn();
                Console.WriteLine("What coord to deploy admiral?");                
                OnActionDeclared(new MyEventArgs(GlobVars.symShip));
                // NotifyActionCompleted(GlobVars.symShip);
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

            var args = new MyEventArgs(GlobVars.symHit);
            OnActionDeclared(args);
            Console.Clear();
            OnDisplayOpponent();
            NotifyActionCompleted(args.symbol);
            //Console.WriteLine("Admiral! Our bombardment round has finished.");
            Console.ReadLine();
        }

        //subscriber
        public void OnOpponentKill(Board source, EventArgs args)
        {
            Console.Clear();
            OnDisplayOpponent();
            Console.WriteLine("Player " + this.name + " won the match!");
            Console.ReadLine();
            System.Environment.Exit(1);
        }

        private void NotifyActionCompleted(char symbol)
        {
            switch (symbol)
            {
                case GlobVars.symMiss:
                    Console.WriteLine("Massive barrage missed the target.");
                    break;

                case GlobVars.symHit:
                    Console.WriteLine("Spang on the target!");
                    break;
                
                // case GlobVars.symShip:
                //     Console.WriteLine("Ship deployment succeded");
                //     break;
            }
        }

        private void NotifyActionCancelled()
        {
            Console.WriteLine("Cant select used coord admiral. Try another one.");
        }

        // publisher
        protected virtual void OnActionDeclared(MyEventArgs args)
        {
            while (true)
            {
                args.coord = Prompter.PromptCoord();

                switch (args.symbol)
                {
                    case GlobVars.symHit:
                        System.Console.WriteLine("A");  
                        Fire(this, args); 
                        break;
                    case GlobVars.symShip:
                        System.Console.WriteLine("B");  
                        Deploy(this, args); 
                        break;
                    default:
                        System.Console.WriteLine("BC");  
                        break;
                }

                if (args.cancelled) 
                {
                    NotifyActionCancelled();
                }
                else 
                {
                    break;
                }
            }
        }

        protected virtual void OnDisplayOwn()
        {
            var args = new MyEventArgs();
            args.mask = false;
            DisplayOwn(this, args);
        }

        protected virtual void OnDisplayOpponent()
        {
            var args = new MyEventArgs();
            args.mask = true;
            DisplayOpponent(this, args);
        }

    }
}
