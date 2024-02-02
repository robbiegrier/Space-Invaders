using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SLinkMan : ListBase
    {
        public override void AddToFront(NodeBase pNode)
        {
            Debug.Assert(pNode != null);

            SLink _pNode = (SLink)pNode;

            _pNode.pNext = pHead;
            pHead = _pNode;
        }

        public override void Remove(NodeBase pNode)
        {
            Debug.Assert(pNode != null);

            SLink _pNode = (SLink)pNode;

            SLink pPrev = null;
            SLink pCurr = pHead;

            while (pCurr != null)
            {
                if (pCurr == _pNode)
                {
                    if (pPrev != null)
                    {
                        pPrev.pNext = pCurr.pNext;
                    }
                    else
                    {
                        pHead = pHead.pNext;
                    }

                    _pNode.Clear();
                    return;
                }

                pPrev = pCurr;
                pCurr = pCurr.pNext;
            }
        }

        public override NodeBase RemoveFromFront()
        {
            SLink pOutput = pHead;

            if (pHead != null)
            {
                pHead = pHead.pNext;
            }

            pOutput.Clear();
            return pOutput;
        }

        public override Iterator GetIterator()
        {
            Debug.Assert(pIterator != null);
            pIterator.Reset(pHead);
            return pIterator;
        }

        public override Iterator GetSearchIterator()
        {
            Debug.Assert(this.pSearchIterator != null);
            this.pSearchIterator.Reset(this.pHead);
            return this.pSearchIterator;
        }

        public override void Relocate(NodeBase _pNode, NodeBase _pBeforeMe)
        {
            // must have at least one to relocate
            Debug.Assert(pHead != null);
            Debug.Assert(_pNode != null);

            // Remove for now
            Remove(_pNode);
            Insert(_pNode, _pBeforeMe);
        }

        public override void Insert(NodeBase _pNode, NodeBase _pBeforeMe)
        {
            Debug.Assert(_pNode != null);

            SLink pNode = (SLink)_pNode;

            // Base case - first insert
            if (pHead == null)
            {
                pHead = pNode;
                pHead.pNext = null;
            }
            // If 'beforeMe' is null, add to end (before nothing is at the end)
            else if (_pBeforeMe == null)
            {
                SLink pCurr = pHead.pNext;
                SLink pPrev = pHead;

                while (pCurr != null)
                {
                    pPrev = pCurr;
                    pCurr = pCurr.pNext;
                }

                pPrev.pNext = pNode;
                pNode.pNext = null;
            }
            // Otherwise, stitch the node into the list before 'beforeMe'
            else
            {
                SLink pCurr = pHead;
                SLink pBeforeMe = (SLink)_pBeforeMe;
                SLink pAfterMe = null;

                while (pCurr != pBeforeMe && pCurr != null)
                {
                    pAfterMe = pCurr;
                    pCurr = pCurr.pNext;
                }

                // This would mean 'beforeMe' was not in the list (bad call)
                Debug.Assert(pCurr != null);

                // Desired output: ..... -> afterMe -> node -> beforeMe -> .....
                pNode.pNext = pBeforeMe;

                // Edge case - insert at the front or not
                if (pBeforeMe == pHead)
                {
                    pHead = pNode;
                }
                else
                {
                    pAfterMe.pNext = pNode;
                }
            }
        }

        private SLink pHead = null;

        // LTN - Own and provide an iterator
        public SLinkIterator pIterator = new SLinkIterator();

        // LTN - Own and provide a search iterator
        public SLinkIterator pSearchIterator = new SLinkIterator();
    }
}