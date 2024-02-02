using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class WallBottom : WallCategory
    {
        public WallBottom(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, Type.Bottom)
        {
            GetCollisionObject().poColRect.Set(posX, posY, width, height);
            x = posX;
            y = posY;
            GetCollisionObject().pColSprite.SetColor(0, 1, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallBottom(this);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }
    }
}