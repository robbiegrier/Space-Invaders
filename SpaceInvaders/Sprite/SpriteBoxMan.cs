using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteBoxMan : ManBase
    {
        private SpriteBoxMan(int reserveNum, int reserveGrow)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // LTN - own the compare object
            SpriteBoxMan.psSpriteBoxCompare = new SpriteBox();
        }

        public static void Create(int reserveNum = 8, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(psInstance == null);

            if (psInstance == null)
            {
                // LTN - Singleton instance
                psInstance = new SpriteBoxMan(reserveNum, reserveGrow);
            }

            // Pre-load null assets
            Add(SpriteBox.Name.NullObject, 0, 0, 0, 0);
        }

        public static void Destroy(bool bPrintEnable = false)
        {
            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            if (bPrintEnable)
            {
                SpriteBoxMan.DumpStats();
            }
        }

        public static SpriteBox Add(SpriteBox.Name inName, float inX, float inY, float inWidth, float inHeight, Azul.Color pInColor = null)
        {
            return privGetInstance().privAdd(inName, inX, inY, inWidth, inHeight, pInColor);
        }

        public static SpriteBox Find(SpriteBox.Name inName)
        {
            return privGetInstance().privFind(inName);
        }

        public static void Remove(SpriteBox pSpriteBox)
        {
            privGetInstance().privRemove(pSpriteBox);
        }

        private SpriteBox privAdd(SpriteBox.Name inName, float inX, float inY, float inWidth, float inHeight, Azul.Color pInColor)
        {
            SpriteBox pNode = (SpriteBox)baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(inName, inX, inY, inWidth, inHeight, pInColor);
            return pNode;
        }

        private SpriteBox privFind(SpriteBox.Name inName)
        {
            psSpriteBoxCompare.name = inName;
            return (SpriteBox)baseFind(psSpriteBoxCompare);
        }

        private void privRemove(SpriteBox pSpriteBox)
        {
            Debug.Assert(pSpriteBox != null);
            baseRemove(pSpriteBox);
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBox Man: ------");
            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);
            pMan.baseDump();
        }

        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBox Man: ------");
            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);
            pMan.baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - man base will store this
            NodeBase pNodeBase = new SpriteBox();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private static SpriteBoxMan privGetInstance()
        {
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        private static SpriteBox psSpriteBoxCompare;
        private static SpriteBoxMan psInstance = null;
    }
}