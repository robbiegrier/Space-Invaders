//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;

namespace SpaceInvaders
{
    public abstract class ListBase
    {
        public abstract void AddToFront(NodeBase pNode);

        public abstract void Relocate(NodeBase pNode, NodeBase pBeforeMe);

        public abstract void Insert(NodeBase pNode, NodeBase pBeforeMe);

        public abstract void Remove(NodeBase pNode);

        public abstract NodeBase RemoveFromFront();

        public abstract Iterator GetIterator();

        public abstract Iterator GetSearchIterator();
    }
}

// --- End of File ---