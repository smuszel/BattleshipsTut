using System;

namespace bships
{
    public class Player 
    {
        public string name;

        public delegate void OnShootHandler(Player source, ActionEventArgs args);
        public event OnShootHandler Fire;

        public delegate void OnShipSubmitHandler(Player source, ActionEventArgs args);
        public event OnShipSubmitHandler Deploy;

        public delegate void OnDisplayHandler(bool mask);
        public event OnDisplayHandler DisplayOwn;
        public event OnDisplayHandler DisplayOpponent;


        public Player(string name)
        { 
            this.name = name;
        }

        public void Deployment()
        {
            for (int i=0; i<Constants.numOfShips; i++)
            {
                Console.WriteLine("This is our board admiral " + this.name);
                OnDisplayOwn();
                Console.WriteLine("What coord to deploy admiral?");                
                OnActionDeclared(new ActionEventArgs(Constants.symShip));
                // NotifyActionCompleted(Constants.symShip);
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

            var args = new ActionEventArgs(Constants.symHit);
            OnActionDeclared(args);
            Console.Clear();
            OnDisplayOpponent();
            NotifyActionCompleted(args.symbol);
            Console.ReadLine();
        }

        private void NotifyActionCompleted(char symbol)
        {
            switch (symbol)
            {
                case Constants.symMiss:
                    Console.WriteLine("Massive barrage missed the target.");
                    break;

                case Constants.symHit:
                    Console.WriteLine("Spang on the target!");
                    break;
                
                // case Constants.symShip:
                //     Console.WriteLine("Ship deployment succeded");
                //     break;
            }
        }

        // publisher
        protected virtual void OnActionDeclared(ActionEventArgs args)
        {
            while (true)
            {
                args.coord = Prompter.PromptCoord();

                switch (args.symbol)
                {
                    case Constants.symHit:
                        Fire(this, args); 
                        break;
                    case Constants.symShip:  
                        Deploy(this, args); 
                        break;
                    default:  
                        break;
                }

                if (args.cancelled) 
                {
                    Console.WriteLine("Cant select used coord admiral. Try another one.");
                }
                else 
                {
                    break;
                }
            }
        }

        protected virtual void OnDisplayOwn()
        {
            DisplayOwn(false);
        }
        protected virtual void OnDisplayOpponent()
        {
            DisplayOpponent(true);
        }

    }
}
