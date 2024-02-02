using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // When a collision occurs, remove the left-hand-side object
    internal class RemoveLeftGameObjectObserver : CollisionObserver
    {
        public RemoveLeftGameObjectObserver()
        {
            pObject = null;
        }

        public RemoveLeftGameObjectObserver(RemoveLeftGameObjectObserver m)
        {
            pObject = m.pObject;
        }

        public RemoveLeftGameObjectObserver(GameObject m)
        {
            pObject = m;
        }

        public override void Notify()
        {
            pSubject.pObjA.Destroy();
        }

        public override void Execute()
        {
            pObject.Remove();
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.RemoveLeftObserver;
        }

        private GameObject pObject;
    }
}