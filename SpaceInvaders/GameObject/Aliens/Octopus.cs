using System;

namespace SpaceInvaders
{
    internal class Octopus : AlienBase
    {
        public Octopus(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.OctopusAlien, spriteName, posX, posY)
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitOctopus(this);
        }
    }
}