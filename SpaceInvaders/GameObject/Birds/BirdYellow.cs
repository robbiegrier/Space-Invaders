using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdYellow : BirdBase
    {
        public BirdYellow(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.YellowBird, spriteName, posX, posY)
        {
        }

        public override void Update()
        {
            //this.x += this.delta;

            //if (this.x > 600.0f || this.x < 200.0f)
            //{
            //    this.delta *= -1.0f;
            //}

            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitYellowBird(this);
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

        //private float delta = 3.0f;
    }
}