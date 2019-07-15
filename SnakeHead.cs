using System;
using System.Collections.Generic;
using System.Text;

namespace KonsolenSnake
{
    class SnakeHead : GameObject
    {
        SnakeTailPiece lastSnakeTailPiece;
        public SnakeHead(short x, short y, Game game) : base('S', x, y, game)
        {
            xSpeed = 1;
            OnCollision += HandleCollision;
        }

        public event EventHandler Death;

        public override void GetInputs(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (!(ySpeed > 0))
                    {
                        xSpeed = 0;
                        ySpeed = -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (!(ySpeed < 0))
                    {
                        xSpeed = 0;
                        ySpeed = 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (!(xSpeed > 0))
                    {
                        xSpeed = -1;
                        ySpeed = 0;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!(xSpeed < 0))
                    {
                        xSpeed = 1;
                        ySpeed = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        private void HandleCollision(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Food))
            {
                eatFood((Food)sender);
            }
            else if (sender.GetType() == typeof(SnakeTailPiece))
            {
                if (Death != null)
                    Death(this, new EventArgs());
            }
        }

        private void eatFood(Food foodObject)
        {
            game.gameObjects.Remove(foodObject);
            game.generateFood();
            game.Score++;
            ExtendTail();
        }

        private void ExtendTail()
        {
            SnakeTailPiece newPiece;
            if (lastSnakeTailPiece == null)
            {
                newPiece = new SnakeTailPiece(this, game);
            }
            else
            {
                newPiece = new SnakeTailPiece(lastSnakeTailPiece, game);
            }

            game.gameObjects.Add(newPiece);
            lastSnakeTailPiece = newPiece;
        }
    }
}
