using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteBatch : DLink
    {
        public enum Name
        {
            PacMan,
            AngryBirds,
            DemoSprites,
            Boxes,
            Aliens,
            Missiles,
            Texts,
            Players,
            Bombs,
            Shields,
            Splatters,

            DebugBatchA,
            DebugBatchB,
            DebugBatchC,
            DebugBatchD,

            Uninitialized
        }

        public SpriteBatch()
            : base()
        {
            name = SpriteBatch.Name.Uninitialized;

            // LTN - own the batched sprites list long term
            poBatchedSprites = new SpriteNodeMan();
            Debug.Assert(poBatchedSprites != null);
        }

        public void Set(SpriteBatch.Name inName, int reserveNum = 3, int reserveGrow = 1)
        {
            name = inName;
            poBatchedSprites.Set(inName, reserveNum, reserveGrow);
        }

        public void SetName(SpriteBatch.Name inName)
        {
            name = inName;
        }

        //
        public SpriteNodeMan GetBatchedSprites()
        {
            return poBatchedSprites;
        }

        public SpriteNode Attach(SpriteBase pInSprite)
        {
            Debug.Assert(pInSprite != null);
            SpriteNode pNode = poBatchedSprites.Attach(pInSprite);

            pNode.Set(pInSprite, poBatchedSprites);
            poBatchedSprites.SetSpriteBatch(this);

            return pNode;
        }

        public SpriteNode Attach(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            SpriteNode pNode = poBatchedSprites.Attach(pGameObject.pSpriteProxy);

            pNode.Set(pGameObject.pSpriteProxy, poBatchedSprites);
            poBatchedSprites.SetSpriteBatch(this);

            return pNode;
        }

        public override void Wash()
        {
            baseClear();
            privClear();
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());
            Debug.WriteLine("   Name: {0} ({1})", name, GetHashCode());
            Debug.WriteLine("   Priority: {0} ({1})", name, GetPriority());
        }

        private void privClear()
        {
            // Do nothing
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public int GetPriority()
        {
            return priority;
        }

        public void SetPriority(int inPriority)
        {
            SpriteBatchMan.SetPriority(this, inPriority);
        }

        public void WriteToPriorityWithNoEffect(int inPriority)
        {
            priority = inPriority;
        }

        public void SetVisibility(bool visibility)
        {
            visible = visibility;
        }

        public bool IsVisible()
        {
            return visible;
        }

        public SpriteBatch.Name name;

        // The list of sprite nodes within this batch
        private readonly SpriteNodeMan poBatchedSprites;

        private int priority = 0;

        private bool visible = true;
    }
}