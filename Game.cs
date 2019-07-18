using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace KonsolenSnake
{
    class Game
    {
        public readonly short FieldWidth;
        public readonly short FieldHeight;
        public readonly ushort fps;
        public List<GameObject> gameObjects;
        public Game(short FieldWidth, short FieldHeight, ushort fps)
        {
            this.FieldHeight = FieldHeight;
            this.FieldWidth = FieldWidth;
            this.fps = fps;
            gameObjects = new List<GameObject>();
            initGame();
        }
        public Game(short FieldWidth, short FieldHeight, ushort fps, bool enableEnemy)
        {
            this.FieldHeight = FieldHeight;
            this.FieldWidth = FieldWidth;
            this.fps = fps;
            gameObjects = new List<GameObject>();
            initGame(enableEnemy);
        }
        public ushort Score { get; set; }
        private bool GameOver { get; set; } = false;
        public void gameLoop()
        {
            while (!GameOver)
            {
                getInput();
                doFrame();
                Draw();
                DrawWalls();
                DrawUI();
                Thread.Sleep(new TimeSpan((10L * 1000L * 1000L) / fps));
            }
            ShowGameOverScreen();
        }

        private void getInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.GetInputs(pressedKey);
                }
            }

        }

        private void doFrame()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                gameObject.DoFrame();
            }
        }

        private void Draw()
        {
            Console.Clear();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                if (gameObject.X <= FieldWidth && gameObject.Y <= FieldHeight && gameObject.X >= 0 && gameObject.Y >= 0)
                {
                    Console.CursorLeft = (int)(gameObject.X);
                    Console.CursorTop = (int)(gameObject.Y);
                    Console.Write(gameObject.DrawChar);
                }
            }
        }

        private void DrawUI()
        {
            if (FieldHeight + 2 < Console.BufferHeight)
            {
                Console.SetCursorPosition(0, FieldHeight + 2);
                Console.WriteLine($"Score:\t{Score}");
            }
        }

        private void DrawWalls()
        {
            const char HORIZWALLCHAR = '─';
            const char VERTWALLCHAR = '│';
            const char CORNERCHAR = '┘';
            bool heightCheck = FieldHeight + 1 < Console.BufferHeight;
            bool widthCheck = FieldWidth < Console.BufferWidth;
            if (heightCheck)
            {
                Console.SetCursorPosition(0, FieldHeight + 1);
                Console.WriteLine("".PadRight(FieldWidth, HORIZWALLCHAR));
            }
            if (widthCheck)
            {
                for (int y = 0; y <= FieldHeight; y++)
                {
                    Console.SetCursorPosition(FieldWidth, y);
                    Console.Write(VERTWALLCHAR);
                }
            }

            if (heightCheck && widthCheck)
            {
                Console.SetCursorPosition(FieldWidth, FieldHeight + 1);
                Console.Write(CORNERCHAR);
            }
        }

        private void initGame()
        {
            initGame(false);
        }

        private void initGame(bool enableEnemy)
        {
            //Create SnakeHead
            SnakeHead snake = new SnakeHead((short)(FieldWidth / 2), (short)(FieldHeight / 2), this);
            snake.Death += GameOverHandler;
            gameObjects.Add(snake);
            Random rng = new Random();
            if (enableEnemy)
            {
                EnemySnake evilSnake = new EnemySnake((short)rng.Next(0, FieldWidth), (short)rng.Next(0, FieldHeight), this);
                gameObjects.Add(evilSnake);
            }

            //Create First Food
            generateFood();
        }

        public void generateFood()
        {
            Random rng = new Random();
            gameObjects.Add(new Food((short)rng.Next(0, FieldWidth), (short)rng.Next(0, FieldHeight), this));
        }

        private void GameOverHandler(object sender, EventArgs e)
        {
            GameOver = true;
        }

        private void ShowGameOverScreen()
        {
            Console.WriteLine($"Game Over your Score: {Score}");
            Console.WriteLine("Bitte Drücken sie eine beliebige taste zum beenden");
            Console.ReadKey();
        }
    }
}
