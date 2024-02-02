using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShieldCategory.Type.Brick)
        {
            x = posX;
            y = posY;
            SetCollisionColor(1.0f, 1.0f, 1.0f);
        }

        public override void EndPlay()
        {
            base.EndPlay();

            SplatterRoot.MakeSplatter(SpriteGame.Name.AlienPullYA, x, y);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldBrick(this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public void Resurrect(float posX, float posY)
        {
            x = posX;
            y = posY;
            SetCollisionColor(1.0f, 1.0f, 1.0f);
            base.Resurrect();
            SetCollisionColor(1.0f, 1.0f, 1.0f);
        }
    }
}