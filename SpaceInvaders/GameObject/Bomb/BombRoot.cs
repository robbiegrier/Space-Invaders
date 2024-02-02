using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BombRoot : Composite
    {
        public BombRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
            pCollisionObject.pColSprite.SetColor(1f, 0.5f, 0.5f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBombRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }
    }
}