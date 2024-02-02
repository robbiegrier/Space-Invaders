using System;

namespace SpaceInvaders
{
    internal abstract class IteratorCompositeBase
    {
        public abstract Component Next();

        public abstract bool IsDone();

        public abstract Component First();

        public abstract Component Curr();
    }
}