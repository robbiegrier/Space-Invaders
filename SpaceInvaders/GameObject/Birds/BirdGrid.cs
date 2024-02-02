using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdGrid : Composite
    {
        public BirdGrid()
            : base()
        {
            SetName(Name.BirdGrid);
            pCollisionObject.pColSprite.SetColor(0.0f, 1.0f, 0.0f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBirdGrid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", m.GetGameObjectName(), name);
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }

        public void SetDelta(float x)
        {
            delta = x;
        }

        public void MoveGrid()
        {
            IteratorComposite pFor = new IteratorComposite(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += delta;
                pNode = pFor.Next();
            }
        }

        public float GetDelta()
        {
            return delta;
        }

        private float delta = 0.5f;
    }
}