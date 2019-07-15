using System;
using System.Collections.Generic;
using System.Text;

namespace KonsolenSnake
{
    class GameObject
    {
        internal short x;
        internal short y;
        internal char drawChar;
        internal short xSpeed;
        internal short ySpeed;
        internal short lastX;
        internal short lastY;
        internal Game game;
        
        public char DrawChar { get { return drawChar; } }
        public float X { get { return x; } }
        public float Y { get { return y; } }

        public GameObject(char drawChar, short x, short y,Game game)
        {
            this.drawChar = drawChar;
            this.x = x; 
            this.y = y;
            this.lastX = x;
            this.lastY = y;
            this.game = game;
        }


        public event EventHandler OnCollision;

        public virtual void GetInputs(ConsoleKeyInfo pressedKey)
        {

        }

        public virtual void DoFrame()
        {
            Move();
            CheckCollision();
        }

        internal virtual void Move()
        {
            lastX = x;
            lastY = y;
            if ((x +xSpeed < game.FieldWidth && xSpeed > 0) || (x +xSpeed  >= 0 && xSpeed < 0))
            {
                
                x += xSpeed;
            }
            if ((y + ySpeed  <= game.FieldHeight && ySpeed > 0) || (y +ySpeed  >= 0 && ySpeed < 0))
            {
                
                y += ySpeed;
            }


        }
        
        internal void CheckCollision()
        {
            for (int i = 0; i < game.gameObjects.Count; i++)
            {
                GameObject currentGameObject = game.gameObjects[i];
                bool xCollision = (currentGameObject.x == x);
                bool yCollision = (currentGameObject.y == y);
                if (xCollision && yCollision && currentGameObject != this && currentGameObject != null)
                {
                    OnCollision?.Invoke(currentGameObject, new EventArgs());
                    //gameObject.OnCollision(this, new EventArgs());
                }
            }
        }
    }
}
