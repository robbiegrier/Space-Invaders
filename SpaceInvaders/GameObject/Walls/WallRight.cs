using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class WallRight : WallCategory
    {
        public WallRight(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, Type.Right)
        {
            pCollisionObject.poColRect.Set(posX, posY, width, height);
            x = posX;
            y = posY;
            pCollisionObject.pColSprite.SetColor(1, 1, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallRight(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitBirdGrid(BirdGrid a)
        {
            Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            Debug.WriteLine("               --->DONE<----");

            //a.SetDelta(-2.0f);

            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitUfoRoot(UfoRoot a)
        {
        }

        public override void VisitUfo(Ufo a)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitMissileGroup(MissileGroup a)
        {
        }

        public override void VisitMissile(Missile a)
        {
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }
    }
}