using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class FontMan : ManBase
    {
        public FontMan(int reserveNum = 3, int reserveGrow = 1)
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            poNodeCompare = new Font();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new FontMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
        }

        // add a font with name, batch to render, message, glyph family, and location
        public static Font Add(Font.Name name, SpriteBatch.Name spriteBatchName, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            return privGetInstance().privAdd(name, spriteBatchName, pMessage, glyphName, xStart, yStart);
        }

        public static Font Find(Font.Name name)
        {
            return privGetInstance().privFind(name);
        }

        private Font privAdd(Font.Name name, SpriteBatch.Name spriteBatchName, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            // Get the font object and load the data
            Font pNode = (Font)baseAdd();
            Debug.Assert(pNode != null);
            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // get the batch object and attach the sprite font
            SpriteBatch pSB = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.poSpriteFont != null);
            pSB.Attach(pNode.poSpriteFont);

            return pNode;
        }

        private Font privFind(Font.Name name)
        {
            poNodeCompare.name = name;
            return (Font)baseFind(poNodeCompare);
        }

        public static void AddXml(Glyph.Name glyphName, string assetName, Texture.Name textName)
        {
            GlyphMan.AddXml(assetName, glyphName, textName);
        }

        public static void Remove(Font pNode)
        {
            Debug.Assert(pNode != null);
            privGetInstance().baseRemove(pNode);
        }

        public static void Dump()
        {
            privGetInstance().baseDump();
        }

        private static FontMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        protected override NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Font();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        public static void SetInstance(FontMan pInInstance)
        {
            pInstance = pInInstance;
        }

        public static void RemoveAll()
        {
            privGetInstance().privRemoveAll();
        }

        private void privRemoveAll()
        {
            Iterator pIt = baseGetIterator();

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                Font pCurr = (Font)pIt.Current();

                pCurr.Remove();
                pIt.Erase(this);
            }
        }

        private readonly Font poNodeCompare;
        private static FontMan pInstance = null;
    }
}