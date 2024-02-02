//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class NodeMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public NodeMan(ListBase _poActive, ListBase _poReserve, int reserveNum = 3, int reserveGrow = 1)
                : base(_poActive, _poReserve, reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            this.poNodeCompare = new Node();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public Node Add(Node.Name name, int val)
        {
            Node pNode = (Node)this.baseAdd();
            Debug.Assert(pNode != null);

            // Initialize the date
            pNode.Set(name, val);
            return pNode;
        }

        public Node Find(Node.Name name)
        {
            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            this.poNodeCompare.name = name;

            Node pData = (Node)this.baseFind(this.poNodeCompare);
            return pData;
        }

        public void Remove(Node pNode)
        {
            Debug.Assert(pNode != null);
            this.baseRemove(pNode);
        }

        public void Dump()
        {
            this.baseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override NodeBase derivedCreateNode()
        {
            // LTN - Man base will store this
            NodeBase pNodeBase = new Node(Node.Name.Unitialized, 0);
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager
        //----------------------------------------------------------------------
        private readonly Node poNodeCompare;
    }
}

// --- End of File ---