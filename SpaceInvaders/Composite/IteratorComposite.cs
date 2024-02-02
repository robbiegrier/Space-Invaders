using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class IteratorComposite : IteratorCompositeBase
    {
        public IteratorComposite()
        {
        }

        public IteratorComposite(Component pStart)
        {
            Reset(pStart);
        }

        public void Reset(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            pCurr = pStart;
            pRoot = pStart;
        }

        public override Component Curr()
        {
            return pCurr;
        }

        public override Component First()
        {
            Debug.Assert(pRoot != null);
            pCurr = pRoot;
            return pCurr;
        }

        public override bool IsDone()
        {
            return pCurr == null;
        }

        public override Component Next()
        {
            Debug.Assert(pCurr != null);

            Component pParent = GetParent(pCurr);
            Component pChild = GetChild(pCurr);
            Component pSibling = GetSibling(pCurr);

            pCurr = privNextStep(pCurr, pParent, pChild, pSibling);

            return pCurr;
        }

        private Component privNextStep(Component pNode, Component pParent, Component pChild, Component pSibling)
        {
            pNode = null;

            if (pChild != null)
            {
                pNode = pChild;
            }
            else if (pSibling != null)
            {
                pNode = pSibling;
            }
            else
            {
                while (pParent != null)
                {
                    if (pParent == pRoot)
                    {
                        pNode = null;
                        break;
                    }

                    pNode = GetSibling(pParent);

                    if (pNode != null)
                    {
                        return pNode;
                    }
                    else
                    {
                        pParent = GetParent(pParent);
                    }
                }
            }

            return pNode;
        }

        public static Component GetParent(Component pNode)
        {
            Debug.Assert(pNode != null);
            return pNode.pParent;
        }

        public static Component GetChild(Component pNode)
        {
            Debug.Assert(pNode != null);

            if (pNode.type == Component.Container.COMPOSITE)
            {
                return ((Composite)pNode).GetHead();
            }

            return null;
        }

        public static Component GetSibling(Component pNode)
        {
            Debug.Assert(pNode != null);
            return (Component)pNode.pNext;
        }

        private Component pCurr;
        private Component pRoot;
    }
}