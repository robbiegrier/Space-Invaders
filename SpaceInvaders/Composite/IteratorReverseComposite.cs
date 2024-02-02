using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class IteratorReverseComposite : IteratorCompositeBase
    {
        public IteratorReverseComposite(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            IteratorComposite pFoward = new IteratorComposite(pStart);

            Component pPrevNode = null;

            for (pFoward.First(); !pFoward.IsDone(); pFoward.Next())
            {
                Component pNode = pFoward.Curr();

                if (pNode != null)
                {
                    pNode.pReverse = pPrevNode;

                    //String debugReverse = (pNode.pReverse != null) ? pNode.pReverse.GetName().ToString() : "null";
                    //Debug.WriteLine("n:{0} r:{1}", pNode.GetName(), debugReverse);
                }

                pPrevNode = pNode;
            }

            pRoot = pStart;
            pInit = pPrevNode;
            pCurr = pInit;
            pPrev = null;
        }

        public override Component First()
        {
            Debug.Assert(pRoot != null);
            pCurr = pInit;
            return pCurr;
        }

        public override Component Curr()
        {
            return pCurr;
        }

        public override bool IsDone()
        {
            return pPrev == pRoot;
        }

        public override Component Next()
        {
            Debug.Assert(pCurr != null);

            pPrev = pCurr;
            pCurr = pCurr.pReverse;
            return pCurr;
        }

        private Component pRoot;
        private Component pCurr;
        private Component pPrev;
        private Component pInit;
    }
}