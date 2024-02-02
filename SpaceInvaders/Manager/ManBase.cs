//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // Whole class should have NO knowledge of Node
    public abstract class ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public ManBase(ListBase _poActive, ListBase _poReserve, int InitialNumReserved = 5, int DeltaGrow = 2)
        {
            // Check now or pay later
            Debug.Assert(_poActive != null);
            Debug.Assert(_poReserve != null);
            Debug.Assert(InitialNumReserved >= 0);
            Debug.Assert(DeltaGrow > 0);

            // Initialize all variables
            this.mDeltaGrow = DeltaGrow;
            this.mNumReserved = 0;
            this.mNumActive = 0;
            this.mTotalNumNodes = 0;
            this.poActive = _poActive;
            this.poReserve = _poReserve;

            // Preload the reserve
            this.privFillReservedPool(InitialNumReserved);

            // LTN - own a long term comparator for the virtual compare
            psNodeBaseComparator = new NodeBaseVirtualCompare();
        }

        protected void baseSetReserve(int reserveNum, int reserveGrow)
        {
            mDeltaGrow = reserveGrow;

            if (reserveNum > mNumReserved)
            {
                privFillReservedPool(reserveNum - mNumReserved);
            }
        }

        //----------------------------------------------------------------------
        // Base methods - called in Derived class but lives in Base
        //----------------------------------------------------------------------
        public NodeBase baseAdd()
        {
            // Always take from the reserve list
            NodeBase pNodeBase = privPopReserveNode();

            // copy to active
            poActive.AddToFront(pNodeBase);
            this.mNumActive++;

            // YES - here's your new one (may its reused from reserved)
            return pNodeBase;
        }

        protected NodeBase baseInsert(BinaryComparator pLessThanCommand)
        {
            NodeBase pNodeBase = privPopReserveNode();

            poActive.Insert(pNodeBase, baseFindFirstInstanceOfByPredicate(pLessThanCommand));
            this.mNumActive++;

            return pNodeBase;
        }

        protected void baseRelocate(NodeBase pNode, BinaryComparator pLessThanCommand)
        {
            poActive.Relocate(pNode, baseFindFirstInstanceOfByPredicate(pLessThanCommand));
        }

        protected NodeBase baseFind(NodeBase pNodeTarget)
        {
            // Call the more general implementation with the NodeBase comparator
            return baseFindFirstInstanceOfByPredicate(psNodeBaseComparator.With(pNodeTarget));
        }

        // Find the first instance of a node where the predicate is satisfied.
        protected NodeBase baseFindFirstInstanceOfByPredicate(Predicate predicate)
        {
            Iterator pIt = poActive.GetSearchIterator();
            Debug.Assert(pIt != null);

            NodeBase pNode = null;

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                NodeBase pTmp = pIt.Current();
                if (predicate.Check(pTmp))
                {
                    return pTmp;
                }
            }

            return pNode;
        }

        public void baseRemove(NodeBase pNode)
        {
            Debug.Assert(pNode != null);

            // Don't do the work here... delegate it
            poActive.Remove(pNode);

            // wash it before returning to reserve list
            pNode.Wash();

            // add it to the return list
            poReserve.AddToFront(pNode);

            // stats update
            this.mNumActive--;
            this.mNumReserved++;
        }

        protected bool baseIsReserveEmpty()
        {
            Iterator pIt = poReserve.GetIterator();
            Debug.Assert(pIt != null);
            return pIt.First() == null;
        }

        protected void baseDumpStats()
        {
            Debug.WriteLine("");
            Debug.WriteLine("         mDeltaGrow: {0} ", mDeltaGrow);
            Debug.WriteLine("     mTotalNumNodes: {0} ", mTotalNumNodes);
            Debug.WriteLine("       mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("         mNumActive: {0} \n", mNumActive);
        }

        protected void baseDump()
        {
            this.baseDumpStats();

            Iterator pItActive = poActive.GetIterator();
            Debug.Assert(pItActive != null);

            NodeBase pNodeActive = (NodeBase)pItActive.First();
            if (pNodeActive == null)
            {
                Debug.WriteLine("    Active Head: null");
            }
            else
            {
                Debug.WriteLine("    Active Head:  {0} ({1})", pNodeActive.GetName(), pNodeActive.GetHashCode());
            }

            Iterator pItReserve = poReserve.GetIterator();
            Debug.Assert(pItReserve != null);

            NodeBase pNodeReserve = (NodeBase)pItReserve.First();
            if (pNodeReserve == null)
            {
                Debug.WriteLine("   Reserve Head: null\n");
            }
            else
            {
                if (pNodeReserve.GetName() == null)
                {
                    Debug.WriteLine("   Reserve Head:  null ({0})\n", pNodeReserve.GetHashCode());
                }
                else
                {
                    Debug.WriteLine("   Reserve Head:  {0} ({1})\n", pNodeReserve.GetName(), pNodeReserve.GetHashCode());
                }
            }

            Debug.WriteLine("   ------ Active List: -----------\n");

            int i = 0;

            // iterate through the nodes
            for (pItActive.First(); !pItActive.IsDone(); pItActive.Next())
            {
                Debug.WriteLine("   {0}: -------------", i);
                NodeBase pTmp = (NodeBase)pItActive.Current();

                pTmp.Dump();
                i++;
            }

            Debug.WriteLine("");
            Debug.WriteLine("   ------ Reserve List: ----------\n");

            i = 0;
            // iterate through the nodes
            for (pItReserve.First(); !pItReserve.IsDone(); pItReserve.Next())
            {
                Debug.WriteLine("   {0}: -------------", i);
                NodeBase pTmp = (NodeBase)pItReserve.Current();

                pTmp.Dump();
                i++;
            }

            Debug.WriteLine("\n   ------ End ------\n");
        }

        protected Iterator baseGetIterator()
        {
            return poActive.GetIterator();
        }

        //----------------------------------------------------------------------
        // Abstract methods - the "contract" Derived class must implement
        //----------------------------------------------------------------------
        protected abstract NodeBase derivedCreateNode();

        //----------------------------------------------------------------------
        // Private methods - helpers
        //----------------------------------------------------------------------
        private void privFillReservedPool(int count)
        {
            // doesn't make sense if its not at least 1
            Debug.Assert(count >= 0);

            this.mTotalNumNodes += count;
            this.mNumReserved += count;

            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                NodeBase pNode = this.derivedCreateNode();
                Debug.Assert(pNode != null);

                poReserve.AddToFront(pNode);
            }
        }

        private NodeBase privPopReserveNode()
        {
            if (baseIsReserveEmpty())
            {
                privFillReservedPool(mDeltaGrow);
            }

            NodeBase pNodeBase = poReserve.RemoveFromFront();
            Debug.Assert(pNodeBase != null);

            pNodeBase.Wash();
            mNumReserved--;

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data:
        //----------------------------------------------------------------------
        private ListBase poActive;

        private ListBase poReserve;
        private int mDeltaGrow;
        private int mTotalNumNodes;
        private int mNumReserved;
        private int mNumActive;

        private static NodeBaseVirtualCompare psNodeBaseComparator;
    }
}

// --- End of File ---