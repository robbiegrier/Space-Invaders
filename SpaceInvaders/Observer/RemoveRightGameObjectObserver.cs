using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class RemoveRightGameObjectObserver : CollisionObserver
    {
        public RemoveRightGameObjectObserver()
        {
            pObject = null;
        }

        public RemoveRightGameObjectObserver(RemoveRightGameObjectObserver m)
        {
            pObject = m.pObject;
        }

        public RemoveRightGameObjectObserver(GameObject m)
        {
            pObject = m;
        }

        public override void Notify()
        {
            pSubject.pObjB.Destroy();
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
            return Name.RemoveRightObserver;
        }

        private GameObject pObject;
    }
}