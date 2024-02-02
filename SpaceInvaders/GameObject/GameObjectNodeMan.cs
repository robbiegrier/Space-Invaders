using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class GameObjectNodeMan : ManBase
    {
        public GameObjectNodeMan(int reserveNum, int reserveGrow)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // LTN - Store the compare object once and reuse
            psGameObjectNodeCompare = new GameObjectNode();

            // LTN - Store the compare null object once and reuse
            psGameObjectNodeCompare.pGameObject = new GameObjectNull();
        }

        public static void Create(int reserveNum = 8, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(psInstance == null);

            if (psInstance == null)
            {
                // LTN - the singleton instance
                psInstance = new GameObjectNodeMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy(bool bPrintEnable = false)
        {
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            if (bPrintEnable)
            {
                DumpStats();
            }
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            return privGetInstance().privAttach(pGameObject);
        }

        public static GameObject Find(GameObject.Name name)
        {
            return privGetInstance().privFind(name);
        }

        public static void Update()
        {
            privGetInstance().privUpdate();
        }

        public static void Remove(GameObjectNode pNode)
        {
            privGetInstance().privRemove(pNode);
        }

        public static void Dump()
        {
            privGetInstance().privDump();
        }

        public static void DumpStats()
        {
            privGetInstance().privDumpStats();
        }

        private GameObjectNode privAttach(GameObject pGameObject)
        {
            GameObjectNode pNode = (GameObjectNode)baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        private GameObject privFind(GameObject.Name name)
        {
            Debug.Assert(psGameObjectNodeCompare != null);
            Debug.Assert(psGameObjectNodeCompare.pGameObject != null);
            psGameObjectNodeCompare.pGameObject.SetName(name);

            GameObjectNode pData = (GameObjectNode)baseFind(psGameObjectNodeCompare);
            Debug.Assert(pData != null);

            GameObject pGameObject = null;

            if (pData != null)
            {
                pGameObject = pData.pGameObject;
            }

            return pGameObject;
        }

        private void privUpdate()
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObjectNode pGameObjectNode = (GameObjectNode)pIt.Current();
                GameObject pRoot = pGameObjectNode.pGameObject;
                Debug.Assert(pRoot != null);

                IteratorReverseComposite pRev = new IteratorReverseComposite(pRoot);
                for (pRev.First(); !pRev.IsDone(); pRev.Next())
                {
                    GameObject pTmp = (GameObject)pRev.Curr();
                    pTmp.Update();
                }
            }
        }

        private void privRemove(GameObjectNode pNode)
        {
            Debug.Assert(pNode != null);
            baseRemove(pNode);
        }

        private void privDump()
        {
            Debug.WriteLine("\n   ------ GameObjectNode Man: ------");
            baseDump();
        }

        private void privDumpStats()
        {
            Debug.WriteLine("\n   ------ GameObjectNode Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        public static void BeginPlay()
        {
            privGetInstance().privBeginPlay();
        }

        private void privBeginPlay()
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObjectNode pGameObjectNode = (GameObjectNode)pIt.Current();
                GameObject pRoot = pGameObjectNode.pGameObject;
                Debug.Assert(pRoot != null);

                IteratorReverseComposite pRev = new IteratorReverseComposite(pRoot);
                for (pRev.First(); !pRev.IsDone(); pRev.Next())
                {
                    GameObject pTmp = (GameObject)pRev.Curr();
                    pTmp.BeginPlay();
                }
            }
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - Manager Base will hold the new node, this just creates it
            NodeBase pNodeBase = new GameObjectNode();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private static GameObjectNodeMan privGetInstance()
        {
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        public static void SetInstance(GameObjectNodeMan pInInstance)
        {
            psInstance = pInInstance;
        }

        public static void Remove(GameObject pNode)
        {
            Composite pParent = (Composite)IteratorComposite.GetParent(pNode);
            pParent.Remove(pNode);

            if (pParent.ShouldBeRemoved())
            {
                pParent.Remove();
            }
        }

        private static GameObjectNode psGameObjectNodeCompare;
        private static GameObjectNodeMan psInstance = null;
    }
}