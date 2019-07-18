using System;
using System.Text;

namespace KonsolenSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WindowWidth = 70;
                Console.WindowHeight = 60;
                Console.BufferHeight = 60;
                Console.BufferWidth = 70;

                Console.OutputEncoding = UTF8Encoding.UTF8;

                Game snakeGame = new Game(70, 55, 10);
                snakeGame.gameLoop();
            }
            else if (args.Length == 4)
            {
                Console.OutputEncoding = UTF8Encoding.UTF8;
                Game snakeGame = new Game(short.Parse(args[0]),short.Parse(args[1]),ushort.Parse(args[2]),bool.Parse(args[3]));
                snakeGame.gameLoop();
            }

        }
    }
}
