using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class CollisionPairMan : ManBase
    {
        public CollisionPairMan(int reserveNum, int reserveGrow)
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            poNodeCompare = new CollisionPair();
        }

        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new CollisionPairMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
        }

        public static CollisionPair Add(CollisionPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            return privGetInstance().privAdd(colpairName, treeRootA, treeRootB);
        }

        public static void Process()
        {
            privGetInstance().privProcess();
        }

        public static CollisionPair Find(CollisionPair.Name name)
        {
            return privGetInstance().privFind(name);
        }

        public static void Remove(CollisionPair pNode)
        {
            Debug.Assert(pNode != null);
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
            Debug.WriteLine("------ ColPair Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------");
        }

        private CollisionPair privAdd(CollisionPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            CollisionPair pColPair = (CollisionPair)baseAdd();
            Debug.Assert(pColPair != null);
            pColPair.Set(colpairName, treeRootA, treeRootB);
            return pColPair;
        }

        private void privProcess()
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                CollisionPair pNode = (CollisionPair)pIt.Current();
                Debug.Assert(pNode != null);
                pActiveCollisionPair = pNode;
                pNode.Process();
            }
        }

        public static CollisionPair GetActiveCollisionPair()
        {
            return privGetInstance().pActiveCollisionPair;
        }

        private CollisionPair privFind(CollisionPair.Name name)
        {
            poNodeCompare.name = name;
            return (CollisionPair)baseFind(poNodeCompare);
        }

        private static CollisionPairMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        protected override NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new CollisionPair();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        public static void SetInstance(CollisionPairMan pInInstance)
        {
            pInstance = pInInstance;
        }

        private readonly CollisionPair poNodeCompare;
        private static CollisionPairMan pInstance = null;
        private CollisionPair pActiveCollisionPair;
    }
}