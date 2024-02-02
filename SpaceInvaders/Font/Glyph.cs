using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // See SpriteFont.Render() for example usage
    internal class Glyph : DLink
    {
        public enum Name
        {
            Consolas20pt,
            Consolas36pt,
            SpaceInvaders,
            NullObject,
            Uninitialized
        }

        public Glyph()
            : base()
        {
            name = Name.Uninitialized;
            pTexture = null;
            poSubRect = new Azul.Rect();
            key = 0;
        }

        // set the data
        public void Set(Name inName, int inKey, Texture.Name textureName, float x, float y, float width, float height)
        {
            // glyph family name
            Debug.Assert(poSubRect != null);
            name = inName;

            // texture source from tex man
            pTexture = TextureMan.Find(textureName);
            Debug.Assert(pTexture != null);

            // update rect properties
            poSubRect.Set(x, y, width, height);

            // the ascii key
            key = inKey;
        }

        private void privClear()
        {
            name = Name.Uninitialized;
            pTexture = null;
            poSubRect.Set(0, 0, 1, 1);
            key = 0;
        }

        // get the rect
        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(poSubRect != null);
            return poSubRect;
        }

        // get the texture source
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(pTexture != null);
            return pTexture.GetAzulTexture();
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            privClear();
        }

        public override bool Compare(NodeBase pTarget)
        {
            Debug.Assert(pTarget != null);
            Glyph pDataB = (Glyph)pTarget;
            return name == pDataB.name && key == pDataB.key;
        }

        public override void Dump()
        {
            Debug.WriteLine("\t\tname: {0} ({1})", name, GetHashCode());
            Debug.WriteLine("\t\t\tkey: {0}", key);

            if (pTexture != null)
            {
                Debug.WriteLine("\t\t   pTexture: {0}", pTexture.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pTexture: null");
            }

            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", poSubRect.x, poSubRect.y, poSubRect.width, poSubRect.height);

            baseDump();
        }

        // get the ascii key
        public int GetKey()
        {
            return key;
        }

        // set the ascii key
        public void SetKey(int inKey)
        {
            key = inKey;
        }

        // type of glyph (font family)
        public Name name;

        // key for this specific glyph
        private int key;

        // rect for how to frame the image
        private Azul.Rect poSubRect;

        // texture to sample the glyph from with the rect
        private Texture pTexture;
    }
}