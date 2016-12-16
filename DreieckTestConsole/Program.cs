using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests;

namespace DreieckTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dreieck = new Dreieck(new GetTypLoggerMock());

            Assert(1,1,1, DreieckTyp.Gleichseitig);
            Assert(1,2,2, DreieckTyp.Gleichschenklig);
            Assert(1,2,4, DreieckTyp.Normal);

            Console.WriteLine(Add(long.MaxValue, 1));

            Console.ReadKey();
        }
        
        public static long Add(long z1, long z2)
        {
            return checked(z1 + z2);
        }

        static void Assert(int a, int b, int c, DreieckTyp erwartet)
        {
            var logger = new GetTypLoggerMock();
            var dreieck = new Dreieck(logger);
            var result = dreieck.GetTyp(a, b, c);
            var oldColor = Console.ForegroundColor;
            if (result != erwartet)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} {1} {2} ist {3} und nicht nicht {4}", a, b, c, result, erwartet);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0} {1} {2} ist {3}",a,b,c,erwartet);
            }
        }
    }
}
