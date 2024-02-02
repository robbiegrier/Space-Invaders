using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class GameObjectNode : DLink
    {
        public GameObjectNode()
            : base()
        {
            privClear();
        }

        public void Set(GameObject pInGameObject)
        {
            Debug.Assert(pInGameObject != null);
            pGameObject = pInGameObject;
        }

        public override bool Compare(NodeBase pTarget)
        {
            Debug.Assert(pTarget != null);
            GameObjectNode pDataB = (GameObjectNode)pTarget;

            bool status = false;

            Debug.Assert(pDataB.pGameObject != null);
            Debug.Assert(pGameObject != null);

            if (pTarget.GetName().GetHashCode() == pGameObject.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }

        public override void Dump()
        {
            Debug.WriteLine("   GameObjectNode: ({0})", GetHashCode());

            if (pGameObject != null)
            {
                Debug.WriteLine("      GameObject.name: {0} ({1})", pGameObject.GetName(), pGameObject.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      GameObject.name: null");
            }

            baseDump();
        }

        public override Enum GetName()
        {
            System.Enum name;

            if (pGameObject == null)
            {
                name = null;
            }
            else
            {
                name = pGameObject.GetName();
            }

            return name;
        }

        public override void Wash()
        {
            privClear();
        }

        private void privClear()
        {
            pGameObject = null;
        }

        public GameObject pGameObject;
    }
}