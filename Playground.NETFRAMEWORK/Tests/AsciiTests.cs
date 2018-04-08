using System;

namespace Playground.NETFRAMEWORK.Tests
{
    public class AsciiTests
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine();
            const int maxX = 15;
            const int maxY = 15;
            for (int y = 0; y < maxY; y++)
            {
                Console.Write(' ');
                if (y == 0 || y == maxY - 1)
                {
                    var sharps = new String('#', maxX);
                    Console.Write(sharps);
                }
                else
                {

                    Console.Write("#");
                    for (int x = 0; x < maxX - 2; x++)
                    {
                        var leftDiagonal = x == (y - 1);
                        var rightDiagonal = ((maxX - x) - 3) == y - 1;
                        Console.Write(leftDiagonal || rightDiagonal ? "#" : " ");
                    }
                    Console.Write("#");
                }


                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine();
        }
    }
}