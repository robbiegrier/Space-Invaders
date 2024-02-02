using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class CollisionSubject
    {
        public CollisionSubject()
        {
            pSLinkMan = new SLinkMan();
            Debug.Assert(pSLinkMan != null);
        }

        public void Subscribe(CollisionObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            pObserver.pSubject = this;

            Debug.Assert(pSLinkMan != null);
            pSLinkMan.AddToFront(pObserver);
        }

        public void Broadcast()
        {
            Iterator pIt = pSLinkMan.GetIterator();

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                CollisionObserver pCurr = (CollisionObserver)pIt.Current();
                Debug.Assert(pCurr != null);
                pCurr.Notify();
            }
        }

        public void Detach(CollisionObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            pSLinkMan.Remove(pObserver);
        }

        private SLinkMan pSLinkMan;
        public GameObject pObjA = null;
        public GameObject pObjB = null;
    }
}