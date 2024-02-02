using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienColumn : Composite
    {
        public AlienColumn()
            : base()
        {
            SetName(Name.AlienColumn);
            pCollisionObject.pColSprite.SetColor(1.0f, 0.0f, 0.0f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void BeginPlay()
        {
            base.BeginPlay();
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

        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }
    }
}