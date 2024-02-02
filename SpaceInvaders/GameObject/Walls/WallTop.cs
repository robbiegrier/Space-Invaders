using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class WallTop : WallCategory
    {
        public WallTop(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, Type.Top)
        {
            pCollisionObject.poColRect.Set(posX, posY, width, height);
            x = posX;
            y = posY;
            pCollisionObject.pColSprite.SetColor(1, 1, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallTop(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitBirdGrid(BirdGrid a)
        {
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
        }
    }
}