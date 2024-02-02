using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteGameProxyMan : ManBase
    {
        private SpriteGameProxyMan(int reserveNum, int reserveGrow)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // LTN - store compare object
            SpriteGameProxyMan.psSpriteGameProxyCompare = new SpriteGameProxy();

            // LTN - store null sprite in compare object
            SpriteGameProxyMan.psSpriteGameProxyCompare.pSprite = new SpriteGameNull();
            SpriteGameProxyMan.psSpriteGameProxyCompare.pSprite.name = SpriteGame.Name.Compare;
            SpriteGameProxyMan.psSpriteGameProxyCompare.name = SpriteGameProxy.Name.Compare;
        }

        public static void Create(int reserveNum = 0, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                // LTN - Singleton Instance
                psInstance = new SpriteGameProxyMan(reserveNum, reserveGrow);
            }

            // Pre-load null assets
            Add(SpriteGame.Name.NullObject);
        }

        public static void Destroy(bool bPrintEnable = false)
        {
            if (bPrintEnable)
            {
                SpriteGameProxyMan.DumpStats();
            }
        }

        public static SpriteGameProxy Find(SpriteGame.Name name)
        {
            SpriteGameProxyMan pMan = SpriteGameProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            SpriteGameProxyMan.psSpriteGameProxyCompare.pSprite.name = name;

            SpriteGameProxy pData = (SpriteGameProxy)pMan.baseFind(SpriteGameProxyMan.psSpriteGameProxyCompare);
            return pData;
        }

        public static SpriteGameProxy Add(SpriteGame.Name name)
        {
            return privGetInstance().privAdd(name);
        }

        public static void Remove(SpriteGameProxy pSprite)
        {
            privGetInstance().privRemove(pSprite);
        }

        public static void Dump()
        {
            privGetInstance().privDump();
        }

        public static void DumpStats()
        {
            privGetInstance().privDumpStats();
        }

        private SpriteGameProxy privAdd(SpriteGame.Name name)
        {
            SpriteGameProxy pNode = (SpriteGameProxy)baseAdd();
            Debug.Assert(pNode != null);
            pNode.Set(name);
            return pNode;
        }

        private void privRemove(SpriteGameProxy pSprite)
        {
            Debug.Assert(pSprite != null);
            baseRemove(pSprite);
        }

        private void privDump()
        {
            Debug.WriteLine("\n   ------ SpriteGameProxy Man: ------");
            baseDump();
        }

        private void privDumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteGameProxy Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - Man base owns this
            NodeBase pNodeBase = new SpriteGameProxy();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private static SpriteGameProxyMan privGetInstance()
        {
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        private static SpriteGameProxy psSpriteGameProxyCompare;
        private static SpriteGameProxyMan psInstance = null;
    }
}