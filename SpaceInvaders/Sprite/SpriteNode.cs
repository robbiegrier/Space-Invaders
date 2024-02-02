using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteNode : DLink
    {
        public SpriteNode()
            : base()
        {
            pSprite = null;
        }

        public void Set(SpriteBase pInSprite, SpriteNodeMan pSpriteNodeMan)
        {
            pSprite = pInSprite;
            Debug.Assert(pSprite != null);
            pSprite.SetSpriteNode(this);

            Debug.Assert(pSpriteNodeMan != null);
            pBackSpriteNodeMan = pSpriteNodeMan;
        }

        public SpriteBase GetSpriteBase()
        {
            return pSprite;
        }

        public SpriteNodeMan GetSBNodeMan()
        {
            Debug.Assert(pBackSpriteNodeMan != null);
            return pBackSpriteNodeMan;
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(pBackSpriteNodeMan != null);
            return pBackSpriteNodeMan.GetSpriteBatch();
        }

        public override bool Compare(NodeBase pSpriteNodeBaseB)
        {
            Debug.Assert(pSpriteNodeBaseB != null);
            SpriteNode pDataB = (SpriteNode)pSpriteNodeBaseB;
            return pSprite.GetName().GetHashCode() == pDataB.GetName().GetHashCode();
        }

        private void privClear()
        {
            pSprite = null;
        }

        public override void Wash()
        {
            baseClear();
            privClear();
        }

        public override System.Enum GetName()
        {
            return null;
        }

        public override void Dump()
        {
            Debug.WriteLine("   ({0}) node", this.GetHashCode());
            Debug.WriteLine("   pSprite: {0} ({1})", this.pSprite.GetName(), this.pSprite.GetHashCode());
            baseDump();
        }

        public SpriteBase GetSprite()
        {
            return pSprite;
        }

        private SpriteBase pSprite;
        private SpriteNodeMan pBackSpriteNodeMan = null;
    }
}