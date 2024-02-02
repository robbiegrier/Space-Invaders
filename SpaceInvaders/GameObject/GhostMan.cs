using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class GhostMan : ManBase
    {
        private GhostMan(int reserveNum = 3, int reserveGrow = 1)
        : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            poNodeCompare = new GameObjectNode();
            poGameObj = new GameObjectNull();
            poNodeCompare.pGameObject = poGameObj;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GhostMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            return privGetInstance().privAttach(pGameObject);
        }

        private GameObjectNode privAttach(GameObject pGameObject)
        {
            GameObjectNode pNode = (GameObjectNode)baseAdd();
            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObjectNode Find(GameObject.Name name)
        {
            return privGetInstance().privFind(name);
        }

        private GameObjectNode privFind(GameObject.Name name)
        {
            poNodeCompare.pGameObject.SetName(name);
            GameObjectNode pData = (GameObjectNode)baseFind(poNodeCompare);
            return pData;
        }

        public static void Remove(GameObjectNode pNode)
        {
            privGetInstance().baseRemove(pNode);
        }

        public static void Dump()
        {
            privGetInstance().baseDump();
        }

        public static void DumpStats()
        {
            privGetInstance().privDumpStats();
        }

        private void privDumpStats()
        {
            Debug.WriteLine("------ Ghost Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------");
        }

        private static GhostMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        protected override NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new GameObjectNode();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private readonly GameObjectNode poNodeCompare;
        private readonly GameObjectNull poGameObj;
        private static GhostMan pInstance = null;
    }
}