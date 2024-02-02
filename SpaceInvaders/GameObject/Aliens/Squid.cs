using System;

namespace SpaceInvaders
{
    internal class Squid : AlienBase
    {
        public Squid(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.SquidAlien, spriteName, posX, posY)
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitSquid(this);
        }
    }
}