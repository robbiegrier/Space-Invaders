using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdWhite : BirdBase
    {
        public BirdWhite(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.WhiteBird, spriteName, posX, posY)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWhiteBird(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", m.GetGameObjectName(), name);
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(m);
            CollisionPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
    }
}