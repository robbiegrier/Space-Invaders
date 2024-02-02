using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteNodeMan : ManBase
    {
        public SpriteNodeMan(int ReserveNum = 3, int ReserveGrow = 1)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), ReserveNum, ReserveGrow)
        {
            // LTN - store the compare object
            psSpriteNodeCompare = new SpriteNode();
        }

        public void Set(SpriteBatch.Name inName, int reserveNum, int reserveGrow)
        {
            name = inName;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            baseSetReserve(reserveNum, reserveGrow);
        }

        public SpriteNode Attach(SpriteBase pSprite)
        {
            SpriteNode pSpriteNode = (SpriteNode)baseAdd();
            Debug.Assert(pSpriteNode != null);

            pSpriteNode.Set(pSprite, this);
            return pSpriteNode;
        }

        public void Draw()
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                SpriteNode pNode = (SpriteNode)pIt.Current();
                pNode.GetSprite().Render();
            }
        }

        public SpriteBatch GetSpriteBatch()
        {
            return pBackSpriteBatch;
        }

        public void SetSpriteBatch(SpriteBatch pSpriteBatch)
        {
            pBackSpriteBatch = pSpriteBatch;
        }

        public void Remove(SpriteNode pSpriteNode)
        {
            Debug.Assert(pSpriteNode != null);
            baseRemove(pSpriteNode);
        }

        public void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");
            baseDump();
        }

        public void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - man base will hold these
            NodeBase pNodeBase = new SpriteNode();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private static SpriteNode psSpriteNodeCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch = null;
    }
}