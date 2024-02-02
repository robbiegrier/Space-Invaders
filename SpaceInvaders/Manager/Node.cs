//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Node : DLink
    {
        public enum Name
        {
            Cat,
            Dog,
            Bird,
            Fish,
            Rabbit,
            Worm,
            Unitialized
        }


        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public Node()
        : base()   // <--- Delegate (kick the can)
        {
            // Class should only initialize variables that it owns
            // Delegate the initialization to other classes
            this.privClear();
        }

        public Node(Name name, int val)
            : base()   // <--- base class do your thing
        {
            this.Set(name, val);
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, int val)
        {
            // Set - Node data  (only Node level)
            this.name = name;
            this.x = val;
        }

        override public void Wash()
        {
            this.privClear();
        }

        private void privClear()
        {
            // Clear - Node data  (only Node level)
            this.name = Name.Unitialized;
            this.x = 0;
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      x: {0} ", this.x);

            // Let the base print its contribution
            this.baseDump();
        }

        public override System.Enum GetName()
        {
            return this.name;
        }

        // Data: --------------------------------
        public Name name;
        public int x;

    }
}

// --- End of File ---
