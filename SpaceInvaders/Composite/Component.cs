using System;

namespace SpaceInvaders
{
    // Base type for composite pattern
    internal abstract class Component : CollisionVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }

        public Component(Component.Container inType)
        {
            type = inType;
            pParent = null;
        }

        public virtual int GetNumChildren()
        {
            return 0;
        }

        public virtual void Resurrect()
        {
            pParent = null;
            pReverse = null;
        }

        public abstract void Print();

        public abstract void Add(Component c);

        public abstract void Remove(Component c);

        public abstract void DumpNode();

        public Container type;
        public Component pParent;
        public Component pReverse;
    }
}