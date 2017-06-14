using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOutKeyWord
{
    class Program
    {
        static void Main(string[] args)
        {

            string saying;

            ShowNoOut(out saying);

            string secondSaying = "";

            ShowIsRef(ref secondSaying);
        }


        static void ShowNoOut(out string saying)
        {
            saying = "See I don't exist.";
        }

        static void ShowIsRef(ref string saying)
        {
            saying = "I do exist.";
        }
    }
}
