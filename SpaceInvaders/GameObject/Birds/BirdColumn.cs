using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdColumn : Composite
    {
        public BirdColumn()
            : base()
        {
            SetName(Name.BirdColumn);
            pCollisionObject.pColSprite.SetColor(1.0f, 0.0f, 0.0f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBirdColumn(this);
        }

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Column:");

            // walk through the list and render
            Iterator pIt = poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            GameObject pNode = (GameObject)pIt.First();

            while (!pIt.IsDone())
            {
                Debug.Assert(pNode != null);

                pNode.Dump();

                pNode = (GameObject)pIt.Next();
            }
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", m.GetGameObjectName(), name);
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }
    }
}