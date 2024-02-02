using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdGreen : BirdBase
    {
        public BirdGreen(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.GreenBird, spriteName, posX, posY)
        {
        }

        public override void Update()
        {
            //this.y += this.delta;

            //if (this.y > 500.0f || this.y < 100.0f)
            //{
            //    this.delta *= -1.0f;
            //}

            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitGreenBird(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, name);
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(m);
            CollisionPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        //private float delta = 3.0f;
    }
}