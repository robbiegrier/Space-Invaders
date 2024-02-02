using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SLink : NodeBase
    {
        public SLink pNext;

        public void Clear()
        {
            pNext = null;
        }

        protected void baseClear()
        {
            Clear();
        }

        protected void baseDump()
        {
            if (pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                NodeBase pTmp = (NodeBase)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }
    }
}
