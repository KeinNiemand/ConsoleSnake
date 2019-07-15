using System;
using System.Collections.Generic;
using System.Text;

namespace KonsolenSnake
{
    class Food : GameObject
    {
        public Food(short x, short y, Game game) : base('F', x, y, game)
        {

        }
    }
}
