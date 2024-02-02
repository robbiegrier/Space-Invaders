//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteGameMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SpriteGameMan(ListBase _poActive, ListBase _poReserve, int reserveNum = 3, int reserveGrow = 1)
            : base(_poActive, _poReserve, reserveNum, reserveGrow)
        {
            // LTN - store the compare object
            this.poSpriteCompare = new SpriteGame();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------

        public static SpriteGame Add(SpriteGame.Name name, Image.Name _ImageName, float x, float y, float width, float height)
        {
            return privGetInstance().privAdd(name, _ImageName, x, y, width, height);
        }

        public static SpriteGame Find(SpriteGame.Name name)
        {
            return privGetInstance().privFind(name);
        }

        public static void Remove(SpriteGame pSprite)
        {
            privGetInstance().privRemove(pSprite);
        }

        public static void Dump()
        {
            privGetInstance().privDump();
        }

        private SpriteGame privAdd(SpriteGame.Name name, Image.Name _ImageName, float x, float y, float width, float height, Azul.Color pInColor = null)
        {
            Image pImage = ImageMan.Find(_ImageName);
            Debug.Assert(pImage != null);

            SpriteGame pSprite = (SpriteGame)this.baseAdd();
            Debug.Assert(pSprite != null);

            pSprite.Set(name, pImage, x, y, width, height, pInColor);
            return pSprite;
        }

        private SpriteGame privFind(SpriteGame.Name name)
        {
            // Compare functions only compares two Sprites

            // So:  Use the Compare Sprite - as a reference
            //      use in the Compare() function
            this.poSpriteCompare.name = name;

            SpriteGame pData = (SpriteGame)this.baseFind(this.poSpriteCompare);
            return pData;
        }

        private void privRemove(SpriteGame pSprite)
        {
            Debug.Assert(pSprite != null);
            this.baseRemove(pSprite);
        }

        private void privDump()
        {
            this.baseDump();
        }

        public static void DumpStats()
        {
            privGetInstance().privDumpStats();
        }

        private void privDumpStats()
        {
            Debug.WriteLine("------ SpriteGame Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override NodeBase derivedCreateNode()
        {
            // LTN - man base will store this
            NodeBase pNodeBase = new SpriteGame();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        public static void Create(int reserveNum = 2, int reserveGrow = 1)
        {
            // make sure values are ressonable
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // LTN - Singleton Instance
            // LTN - Lists owned by man base
            psInstance = new SpriteGameMan(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow);

            SpriteGameMan.Add(SpriteGame.Name.NullObject, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
        }

        public static void Destroy()
        {
            SpriteGameMan pMan = privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        private static SpriteGameMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);
            return psInstance;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager
        //----------------------------------------------------------------------
        private readonly SpriteGame poSpriteCompare;

        private static SpriteGameMan psInstance = null;
    }
}

// --- End of File ---