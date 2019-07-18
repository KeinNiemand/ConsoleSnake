using System;
using System.Collections.Generic;
using System.Text;

namespace KonsolenSnake
{
    class EnemySnake : SnakeHead
    {

        public EnemySnake(short x, short y, Game game) : base (x,y,game)
        {
            drawChar = 'E';
        }

        public override void GetInputs(ConsoleKeyInfo pressedKey) { }

        public override void DoFrame()
        {
            AiDirection();
            base.DoFrame();
        }

        private Food FindFood()
        {
            for (int i = 0; i < game.gameObjects.Count; i++)
            {
                GameObject currentObject = game.gameObjects[i];
                if (currentObject.GetType() == typeof(Food))
                    return (Food)currentObject;
            }
            return null;
        }

        private void AiDirection()
        {
            Food foundFood = FindFood();
            if (foundFood != null)
            {
                //Essen ist rechts von gegner schlange
                if (foundFood.x > x)
                {
                    //Bewege nach rechts
                    xSpeed = 1;
                    ySpeed = 0;
                }
                //Essen ist links von gegner schlange
                else if (foundFood.x < x)
                {
                    //Bewege nach links
                    xSpeed = -1;
                    ySpeed = 0;
                }
                //Essen ist unterhalb von gegner schlange
                else if (foundFood.y > y)
                {
                    //Bewege nach unten
                    xSpeed = 0;
                    ySpeed = 1;
                }
                //Essen ist oberhalb von gegner schlange
                else if (foundFood.y < y)
                {
                    //Bewege nach oben
                    xSpeed = 0;
                    ySpeed = -1;
                }
            }
        }

        internal override void EatFood(Food foodObject)
        {
            game.gameObjects.Remove(foodObject);
            game.generateFood();
            ExtendTail();
        }
    }
}
