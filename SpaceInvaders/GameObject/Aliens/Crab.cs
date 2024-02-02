using System;

namespace SpaceInvaders
{
    internal class Crab : AlienBase
    {
        public Crab(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.CrabAlien, spriteName, posX, posY)
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitCrab(this);
        }
    }
}