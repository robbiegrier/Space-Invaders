using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class Composite : GameObject
    {
        public Composite()
            : base(Component.Container.COMPOSITE, GameObject.Name.NullObject, SpriteGame.Name.NullObject)
        {
            // LTN - Owned by the composite long term, it always needs the list of other components.
            poDLinkMan = new DLinkMan();

            // LTN - Store the iterator once and reset it every update
            pItUpdate = new IteratorComposite(this);
        }

        public Composite(GameObject.Name name, SpriteGame.Name spriteName)
            : base(Component.Container.COMPOSITE, name, spriteName)
        {
            // LTN - Owned by the composite long term, it always needs the list of other components.
            poDLinkMan = new DLinkMan();

            // LTN - Store the iterator once and reset it every update
            pItUpdate = new IteratorComposite(this);
        }

        public override int GetNumChildren()
        {
            int count = 0;

            Iterator pIt = poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                count++;
            }

            return count;
        }

        public override void Resurrect()
        {
            Debug.Assert(IsEmpty());
            base.Resurrect();
        }

        public override void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            Debug.Assert(poDLinkMan != null);

            poDLinkMan.AddToFront(pComponent);
            pComponent.pParent = this;
        }

        public override void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            Debug.Assert(poDLinkMan != null);
            poDLinkMan.Remove(pComponent);
        }

        public Component GetHead()
        {
            Debug.Assert(poDLinkMan != null);
            Component pHead = (GameObject)poDLinkMan.poHead;
            return pHead;
        }

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Composite:");

            Iterator pIt = poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();
                Debug.Assert(pNode != null);
                pNode.Print();
            }
        }

        public override void BeginPlay()
        {
            base.BeginPlay();
        }

        public bool ShouldBeRemoved()
        {
            // Only remove a composite if the contents are empty and it has no parent (do not remove root composites)
            return IsEmpty() && !IsRoot();
        }

        public bool IsEmpty()
        {
            return GetHead() == null;
        }

        public bool IsRoot()
        {
            return pParent == null;
        }

        public override void DumpNode()
        {
            if (IteratorComposite.GetParent(this) != null)
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:{1} <---- Composite", this.GetHashCode(), IteratorComposite.GetParent(this).GetHashCode());
            }
            else
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:null <---- Composite", this.GetHashCode());
            }
        }

        public override void Update()
        {
            // Init with the first object to get off on the right foot
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);

            if (pGameObject != null)
            {
                pCollisionObject.poColRect.Set(pGameObject.pCollisionObject.poColRect);

                // Go through all the sub-objects and calculate the union
                for (pItUpdate.First(); !pItUpdate.IsDone(); pItUpdate.Next())
                {
                    GameObject pChild = (GameObject)pItUpdate.Curr();
                    pCollisionObject.poColRect.Union(pChild.pCollisionObject.poColRect);
                }

                // Update the game object's position to always match the collision object
                x = pCollisionObject.poColRect.x;
                y = pCollisionObject.poColRect.y;
            }
            else
            {
                pCollisionObject.poColRect.Minimize();
            }

            base.Update();
        }

        public override void Wash()
        {
            Debug.Assert(false);
        }

        private IteratorComposite pItUpdate;
        protected DLinkMan poDLinkMan;
    }
}