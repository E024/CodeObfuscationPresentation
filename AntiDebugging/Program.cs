using System;
using PreEmptive.Attributes;

namespace AntiDebugging
{
    class Program
    {

        protected String ClassString = "Be sure to drink your ovaltine.";

        static void Main(string[] args)
        {

            var program = new Program();

            Console.WriteLine("Press <Enter> to start.");
            Console.ReadLine();
            program.Run();

            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();

        }

        void Run()
        {
            ValidateUserNameAndPassword();
        }

        [PreEmptive.Attributes.DebuggingCheck(Action = CheckAction.Hang)]
        void ValidateUserNameAndPassword()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            if (name.Equals("Ralphie", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("What is your quest?");
                var quest = Console.ReadLine();

                if (quest.Equals("To seek the Ovaltine", StringComparison.OrdinalIgnoreCase))
                {

                    Console.WriteLine("What is your favorite color?");

                    if (Console.ReadLine().Equals("The color of a red rider BB gun",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("You'll shoot your eye out kid.");
                        return;
                    }

                }

            }

            Console.WriteLine("You are not the real Ralphie.  Try again.");

        }


    }
}
