using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShieldGrid : Composite
    {
        public ShieldGrid(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
            SetCollisionColor(0.0f, 1.0f, 1.0f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldGrid(this);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitBomb(Bomb b)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public void Resurrect(float posX, float posY)
        {
            x = posX;
            y = posY;

            base.Resurrect();

            SetCollisionColor(0.0f, 1.0f, 1.0f);
        }
    }
}