using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class CollisionPair : DLink
    {
        public enum Name
        {
            Alien_Missile,
            Bird_Missile,
            Alien_Wall,
            Bird_Wall,
            Missile_Wall,
            Bomb_Wall,
            Misslie_Shield,
            Bomb_Shield,
            Bomb_Missile,
            Bomb_Player,
            Missile_Ufo,
            Ufo_Wall,
            NullObject,
            Not_Initialized
        }

        public CollisionPair()
            : base()
        {
            privClear();

            poSubject = new CollisionSubject();
            Debug.Assert(poSubject != null);
        }

        public void Set(Name inName, GameObject pRootA, GameObject pRootB)
        {
            Debug.Assert(pRootA != null);
            Debug.Assert(pRootB != null);

            treeA = pRootA;
            treeB = pRootB;
            name = inName;
        }

        public void Process()
        {
            Collide(treeA, treeB);
        }

        public static void Collide(GameObject pSafeA, GameObject pSafeB)
        {
            GameObject pNodeA = pSafeA;
            GameObject pNodeB = pSafeB;

            Debug.Assert(pNodeA != null);
            Debug.Assert(pNodeB != null);

            while (pNodeA != null)
            {
                pNodeB = pSafeB;

                while (pNodeB != null)
                {
                    //Debug.WriteLine("ColPair: test:  {0}, {1}", pNodeA.name, pNodeB.name);

                    if (CollisionRect.Intersect(pNodeA.GetCollisionObject().poColRect, pNodeB.GetCollisionObject().poColRect))
                    {
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)IteratorComposite.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)IteratorComposite.GetSibling(pNodeA);
            }
        }

        public void Subscribe(CollisionObserver observer)
        {
            poSubject.Subscribe(observer);
        }

        public void NotifyListeners()
        {
            poSubject.Broadcast();
        }

        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            poSubject.pObjA = pObjA;
            poSubject.pObjB = pObjB;
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());

            if (treeA != null)
            {
                Debug.WriteLine("       TreeA: {0}", treeA.GetName());
            }
            else
            {
                Debug.WriteLine("       TreeA: null");
            }

            if (treeB != null)
            {
                Debug.WriteLine("       TreeB: {0}", treeB.GetName());
            }
            else
            {
                Debug.WriteLine("       TreeB: null");
            }

            base.baseDump();
        }

        public override Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            privClear();
        }

        private void privClear()
        {
            treeA = null;
            treeB = null;
            name = Name.Not_Initialized;
        }

        public Name name;
        public GameObject treeA;
        public GameObject treeB;
        public CollisionSubject poSubject;
    }
}