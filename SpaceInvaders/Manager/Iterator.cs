//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;

namespace SpaceInvaders
{
    abstract public class Iterator
    {
        // -------------------------------------------------------------------
        // Next() - Advances the iterator to the next item after current item
        //          If valid returns the next item
        //          If the item is not valid, return null
        //          This function advances the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase Next();

        // -------------------------------------------------------------------
        // IsDone() - (sometimes called hasNext or hasMore)
        //            Return status
        //            Is there additional elements after the current item?
        //            Does not advance the iterator
        // -------------------------------------------------------------------
        abstract public bool IsDone();

        // -------------------------------------------------------------------
        // First() - Returns the first element
        //           Resets the iterator state
        //           Does not advance the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase First();

        // -------------------------------------------------------------------
        // Current() - Returns the current item the iterator is pointing to
        //             Does not advance the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase Current();

        virtual public void Erase(ManBase _pMan)
        {

        }
    }

}

// --- End of File ---
