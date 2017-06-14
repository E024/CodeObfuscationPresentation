using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringObfuscation
{
    class Program
    {

        protected String ClassString = "Be sure to drink your ovaltine.";

        static void Main(string[] args)
        {

            var program = new Program();

            program.Run();

            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();

        }

        void Run()
        {
            ValidateUserNameAndPassword();
        }

        string GetSecretString(bool isUserValid)
        {
            if (isUserValid)
            {
                return ClassString;
            }

            return String.Empty;
        }

        double GetRalphiesAge(DateTime atWhatDate)
        {
            return 5.0;
        }

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
