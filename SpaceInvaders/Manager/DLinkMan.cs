//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DLinkMan : ListBase
    {
        public DLinkMan()
            : base()
        {
            // LTN - own and provide iterator long term
            this.poIterator = new DLinkIterator();

            // LTN - own and provide search iterator long term
            this.poSearchIterator = new DLinkIterator();

            this.poHead = null;
        }

        public override void AddToFront(NodeBase _pNode)
        {
            // add to front
            Debug.Assert(_pNode != null);

            DLink pNode = (DLink)_pNode;
            // add node
            if (poHead == null)
            {
                // push to the front
                poHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = poHead;

                // update head
                poHead.pPrev = pNode;
                poHead = pNode;
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(poHead != null);
        }

        public void AddToEnd(NodeBase _pNode)
        {
            // add to front
            Debug.Assert(_pNode != null);
            DLink pNode = (DLink)_pNode;

            // add node
            if (poHead == null)
            {
                // none on list... so add it
                poHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // spin until end
                DLink pTmp = poHead;
                DLink pLast = pTmp;
                while (pTmp != null)
                {
                    pLast = pTmp;
                    pTmp = pTmp.pNext;
                }

                // push to front
                pLast.pNext = pNode;
                pNode.pPrev = pLast;
                pNode.pNext = null;
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(poHead != null);
        }

        public override void Remove(NodeBase _pNode)
        {
            // There should always be something on list
            Debug.Assert(poHead != null);
            Debug.Assert(_pNode != null);
            DLink pNode = (DLink)_pNode;

            // four cases

            if (pNode.pPrev == null && pNode.pNext == null)
            {   // Only node
                poHead = null;
            }
            else if (pNode.pPrev == null && pNode.pNext != null)
            {   // First node
                poHead = pNode.pNext;
                pNode.pNext.pPrev = pNode.pPrev;
            }
            else if (pNode.pPrev != null && pNode.pNext == null)
            {   // Last node
                pNode.pPrev.pNext = pNode.pNext;
            }
            else // (pNode.pPrev != null && pNode.pNext != null)
            {   // Middle node
                pNode.pNext.pPrev = pNode.pPrev;
                pNode.pPrev.pNext = pNode.pNext;
            }

            // remove any lingering links
            // HUGELY important - otherwise its crossed linked
            pNode.Clear();
        }

        public override NodeBase RemoveFromFront()
        {
            // There should always be something on list
            Debug.Assert(poHead != null);

            // return node
            DLink pNode = poHead;

            // Update head (OK if it points to NULL)
            poHead = poHead.pNext;
            if (poHead != null)
            {
                poHead.pPrev = null;
                // do not change pEnd
            }
            else
            {
                // only one on the list
                // pHead == null
            }

            // remove any lingering links
            // HUGELY important - otherwise its crossed linked
            pNode.Clear();

            return pNode;
        }

        public override Iterator GetIterator()
        {
            Debug.Assert(this.poIterator != null);
            this.poIterator.Reset(this.poHead);
            return this.poIterator;
        }

        public override Iterator GetSearchIterator()
        {
            Debug.Assert(this.poSearchIterator != null);
            this.poSearchIterator.Reset(this.poHead);
            return this.poSearchIterator;
        }

        public override void Relocate(NodeBase _pNode, NodeBase _pBeforeMe)
        {
            // must have at least one to relocate
            Debug.Assert(poHead != null);
            Debug.Assert(_pNode != null);

            // Remove for now
            Remove(_pNode);
            Insert(_pNode, _pBeforeMe);
        }

        public override void Insert(NodeBase _pNode, NodeBase _pBeforeMe)
        {
            Debug.Assert(_pNode != null);

            DLink pNode = (DLink)_pNode;

            // Base case - first insert
            if (poHead == null)
            {
                poHead = pNode;
                poHead.pPrev = null;
                poHead.pNext = null;
            }
            // If 'beforeMe' is null, add to end (before nothing is at the end)
            else if (_pBeforeMe == null)
            {
                DLink pCurr = poHead.pNext;
                DLink pPrev = poHead;

                while (pCurr != null)
                {
                    pPrev = pCurr;
                    pCurr = pCurr.pNext;
                }

                pPrev.pNext = pNode;
                pNode.pPrev = pPrev;
                pNode.pNext = null;
            }
            // Otherwise, stitch the node into the list before 'beforeMe'
            else
            {
                // We want  [ ... -> pAfterMe -> pNode -> pBeforeMe -> ... ]
                DLink pBeforeMe = (DLink)_pBeforeMe;
                DLink pAfterMe = pBeforeMe.pPrev;

                pBeforeMe.pPrev = pNode;
                pNode.pNext = pBeforeMe;
                pNode.pPrev = pAfterMe;

                // Edge case - insert at the front or not
                if (pBeforeMe == poHead)
                {
                    poHead = pNode;
                }
                else
                {
                    pAfterMe.pNext = pNode;
                }
            }
        }

        // ---------------------------------------
        // DO not add/modify variables
        // ---------------------------------------
        // Data:
        public DLink poHead;

        public DLinkIterator poIterator;
        public DLinkIterator poSearchIterator;
    }
}

// --- End of File ---