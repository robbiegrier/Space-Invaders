using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipRoot : Composite
    {
        public ShipRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
            GetCollisionObject().pColSprite.SetColor(0, 0, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShipRoot(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb b)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}