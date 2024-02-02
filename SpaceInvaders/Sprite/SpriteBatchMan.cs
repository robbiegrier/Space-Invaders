using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteBatchMan : ManBase
    {
        public SpriteBatchMan(int reserveNum, int reserveGrow)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // LTN - Own the compare spritebatch
            psSpriteBatchCompare = new SpriteBatch();

            // LTN - Own the less than object
            psPriorityLessThan = new PriorityLessThan();
        }

        public static void Create(int reserveNum = 2, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(psInstance == null);

            if (psInstance == null)
            {
                // LTN - Singleton instance
                psInstance = new SpriteBatchMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy(bool printEnabled = false)
        {
            SpriteBatchMan pMan = privGetInstance();
            Debug.Assert(pMan != null);

            if (printEnabled)
            {
                DumpStats();
            }
        }

        // Add a sprite batch with a name, priority, and reserve memory parameters for the underlying SpriteNodeList.
        public static SpriteBatch Add(SpriteBatch.Name inName, int inPriority, int reserveNum = 3, int reserveGrow = 1)
        {
            return privGetInstance().privAdd(inName, inPriority, reserveNum, reserveGrow);
        }

        // Draw all the batches.
        public static void Draw()
        {
            privGetInstance().privDraw();
        }

        // Find a batch by name.
        public static SpriteBatch Find(SpriteBatch.Name inName)
        {
            return privGetInstance().privFind(inName);
        }

        // Remove a batch by reference.
        public static void Remove(SpriteBatch pSpriteBatch)
        {
            privGetInstance().privRemove(pSpriteBatch);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            privGetInstance().privRemove(pSpriteBatchNode);
        }

        // Set the priority of a batch by name.
        public static void SetPriority(SpriteBatch.Name inName, int inPriority)
        {
            privGetInstance().privSetPriority(inName, inPriority);
        }

        // Set the priority of a batch by reference.
        public static void SetPriority(SpriteBatch pBatch, int inPriority)
        {
            privGetInstance().privSetPriority(pBatch, inPriority);
        }

        private SpriteBatch privAdd(SpriteBatch.Name inName, int inPriority, int reserveNum, int reserveGrow)
        {
            // Construct in place and verify the batch
            SpriteBatch pSpriteBatch = (SpriteBatch)baseInsert(psPriorityLessThan.With(inPriority));
            Debug.Assert(pSpriteBatch != null);

            // Load the Properties
            pSpriteBatch.Set(inName, reserveNum, reserveGrow);

            // Reserve 'SetPriority()' for the method that actually changes the effective priority
            pSpriteBatch.WriteToPriorityWithNoEffect(inPriority);

            return pSpriteBatch;
        }

        private void privDraw()
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            // For Each Batch
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Draw all the Sprites in the Batch
                SpriteBatch pCurrent = (SpriteBatch)pIt.Current();

                if (pCurrent.IsVisible())
                {
                    pCurrent.GetBatchedSprites().Draw();
                }
            }
        }

        private SpriteBatch privFind(SpriteBatch.Name inName)
        {
            psSpriteBatchCompare.name = inName;
            return (SpriteBatch)baseFind(psSpriteBatchCompare);
        }

        private void privRemove(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            baseRemove(pSpriteBatch);
        }

        private void privRemove(SpriteNode pSpriteBatchNode)
        {
            SpriteNodeMan pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();
            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }

        private void privSetPriority(SpriteBatch.Name inName, int inPriority)
        {
            privSetPriority(Find(inName), inPriority);
        }

        private void privSetPriority(SpriteBatch pBatch, int inPriority)
        {
            baseRelocate(pBatch, psPriorityLessThan.With(inPriority));
            pBatch.WriteToPriorityWithNoEffect(inPriority);
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");
            SpriteBatchMan pMan = privGetInstance();
            Debug.Assert(pMan != null);
            pMan.baseDump();
        }

        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");
            SpriteBatchMan pMan = privGetInstance();
            Debug.Assert(pMan != null);
            pMan.baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - Man base will store this
            NodeBase pNodeBase = new SpriteBatch();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private static SpriteBatchMan privGetInstance()
        {
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        public static void SetInstance(SpriteBatchMan pInInstance)
        {
            psInstance = pInInstance;
        }

        private static SpriteBatch psSpriteBatchCompare;
        private static SpriteBatchMan psInstance = null;
        private static PriorityLessThan psPriorityLessThan;
    }

    internal class PriorityLessThan : BinaryComparator
    {
        public override bool Compare(object pLeft, object pRight)
        {
            Debug.Assert(pLeft != null);
            Debug.Assert(pRight != null);

            int priority = (int)pLeft;
            SpriteBatch pRightBatch = (SpriteBatch)pRight;

            return priority < pRightBatch.GetPriority();
        }
    }
}