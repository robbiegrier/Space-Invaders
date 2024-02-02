using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class DeathMan
    {
        private DeathMan()
        {
            pPurgatory = new SLinkMan();
            Debug.Assert(pPurgatory != null);
        }

        public static void Attach(CollisionObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            privGetInstance().pPurgatory.AddToFront(pObserver);
        }

        public static void Process()
        {
            privGetInstance().privProcess();
        }

        private void privProcess()
        {
            Iterator pIt = pPurgatory.GetIterator();

            CollisionObserver pNode;
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pNode = (CollisionObserver)pIt.Current();
                Debug.Assert(pNode != null);
                pNode.Execute();
            }

            pNode = (CollisionObserver)pIt.First();
            while (!pIt.IsDone())
            {
                CollisionObserver pTmp = pNode;
                pNode = (CollisionObserver)pIt.Next();
                pPurgatory.Remove(pTmp);
            }
        }

        private static DeathMan privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new DeathMan();
            }

            Debug.Assert(pInstance != null);
            return pInstance;
        }

        private SLinkMan pPurgatory;
        private static DeathMan pInstance = null;
    }
}