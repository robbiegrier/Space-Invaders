using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class SpriteBase : DLink
    {
        public SpriteBase()
            : base()
        {
            pBackSpriteNode = null;
        }

        public abstract void Update();

        public abstract void Render();

        public SpriteNode GetSpriteNode()
        {
            Debug.Assert(pBackSpriteNode != null);
            return pBackSpriteNode;
        }

        public void SetSpriteNode(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            pBackSpriteNode = pSpriteBatchNode;
        }

        private SpriteNode pBackSpriteNode;
    }
}