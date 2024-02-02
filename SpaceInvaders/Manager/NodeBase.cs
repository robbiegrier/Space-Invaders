//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class NodeBase
    {
        // these are needed to make the ManagerBase work
        abstract public void Wash();
        abstract public void Dump();
        abstract public System.Enum GetName();

        virtual public bool Compare(NodeBase pNodeBaseB)
        {
            Debug.Assert(pNodeBaseB != null);
            return GetName().GetHashCode() == pNodeBaseB.GetName().GetHashCode();
        }
    }

    public class NodeBaseVirtualCompare : BinaryComparator
    {
        public override bool Compare(object pLeft, object pRight)
        {
            NodeBase pNodeLeft = (NodeBase)pLeft;
            NodeBase pNodeRight = (NodeBase)pRight;

            // Delegate the comparison to the virtual NodeBase Compare().
            return pNodeLeft.Compare(pNodeRight);
        }
    }

}

// --- End of File ---

