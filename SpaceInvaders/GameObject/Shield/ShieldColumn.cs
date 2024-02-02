using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShieldColumn : Composite
    {
        public ShieldColumn(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldColumn(this);
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
            SetCollisionColor(1.0f, 0.0f, 0.0f);
            base.Resurrect();
        }
    }
}