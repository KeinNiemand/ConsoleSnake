using System;
using System.Collections.Generic;
using System.Text;

namespace KonsolenSnake
{
    class SnakeTailPiece : GameObject
    {
        private GameObject FollowObject { get; set; }
        public SnakeTailPiece(GameObject followObject, Game game) : base('s', followObject.lastX, followObject.lastY, game)
        {
            this.FollowObject = followObject;
        }

        internal override void Move()
        {
            lastX = x;
            lastY = y;
            x = FollowObject.lastX;
            y = FollowObject.lastY;
        }

        public override void DoFrame()
        {
            Move();
        }
    }
}
