using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class Leaf : GameObject
    {
        public Leaf(GameObject.Name gameObjectName, SpriteGame.Name spriteName, float x, float y)
            : base(Component.Container.LEAF, gameObjectName, spriteName, x, y)
        {
        }

        public override void Print()
        {
            Dump();
        }

        public override void Wash()
        {
            Debug.Assert(false);
        }

        // Makes no sense to add to leaf
        public override void Add(Component c)
        {
            Debug.Assert(false);
        }

        public override void DumpNode()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1}) parent:{2}", this.GetName(), this.GetHashCode(), IteratorComposite.GetParent(this).GetHashCode());
        }

        public override void Resurrect()
        {
            base.Resurrect();
        }

        public override void Remove(Component c)
        {
            Debug.Assert(false);
        }
    }
}