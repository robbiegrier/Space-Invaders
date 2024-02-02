using System;

namespace SpaceInvaders
{
    class SLinkIterator : Iterator
    {
        override public NodeBase Next()
        {
            if (!IsDone())
            {
                pCurr = pCurr.pNext;
                return pCurr;
            }

            return null;
        }

        override public bool IsDone()
        {
            return pCurr == null;
        }

        override public NodeBase First()
        {
            pCurr = pFirst;
            return Current();
        }

        override public NodeBase Current()
        {
            return pCurr;
        }

        public void Reset(SLink pHead)
        {
            pFirst = pHead;
            pCurr = pFirst;
        }

        SLink pFirst = null;
        SLink pCurr = null;
    }
}
