using System;
using System.Text;

namespace KonsolenSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 70;
            Console.WindowHeight = 60;
            Console.BufferHeight = 60;
            Console.BufferWidth = 70;

            Console.OutputEncoding = UTF8Encoding.UTF8;

            Game snakeGame = new Game(70,55,10);
            snakeGame.gameLoop();
        }
    }
}
