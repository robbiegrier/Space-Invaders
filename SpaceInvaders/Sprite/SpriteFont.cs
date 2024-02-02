using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteFont : SpriteBase
    {
        // constructor
        public SpriteFont()
            : base()
        {
            // LTN - underlying azul sprite
            poAzulSprite = new Azul.Sprite();

            // LTN - screen rect
            poScreenRect = new Azul.Rect();

            // LTN - color of text on white base
            poColor = new Azul.Color(1.0f, 1.0f, 1.0f);

            // String representing the content
            pMessage = null;

            // glyph name
            glyphName = Glyph.Name.Uninitialized;

            // location
            x = 0.0f;
            y = 0.0f;
        }

        // set the properties of the sprite font
        public void Set(Font.Name inName, String inMessage, Glyph.Name inGlyphName, float xStart, float yStart)
        {
            // assign the message
            Debug.Assert(inMessage != null);
            pMessage = inMessage;

            // location
            x = xStart;
            y = yStart;

            // font name
            name = inName;

            // glyph name
            glyphName = inGlyphName;

            // new color
            Debug.Assert(poColor != null);
            poColor.Set(1.0f, 1.0f, 1.0f);
        }

        // change the color
        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(poColor != null);
            poColor.Set(red, green, blue, alpha);
        }

        // change the message content
        public void UpdateMessage(String inpMessage)
        {
            Debug.Assert(inpMessage != null);
            pMessage = inpMessage;
        }

        // no update
        public override void Update()
        {
            Debug.Assert(poAzulSprite != null);
        }

        // render each character
        public override void Render()
        {
            Debug.Assert(poAzulSprite != null);
            Debug.Assert(poColor != null);
            Debug.Assert(poScreenRect != null);
            Debug.Assert(pMessage != null);
            //Debug.Assert(pMessage.Length > 0);

            float yTmp = y;
            float xEnd = x;

            for (int i = 0; i < pMessage.Length; i++)
            {
                // ascii key
                int key = Convert.ToByte(pMessage[i]);

                // get the flyweight glyph for the key
                Glyph pGlyph = GlyphMan.Find(glyphName, key);
                Debug.Assert(pGlyph != null);

                // Calculate dimensions from the fetched glyph
                float wTmp = pGlyph.GetAzulRect().width * scaleX;
                float hTmp = pGlyph.GetAzulRect().height * scaleY;
                float xTmp = xEnd + wTmp / 2;

                // Set rect dimensions
                poScreenRect.Set(xTmp, yTmp, wTmp, hTmp);

                // Load the sprite with the current character/color at the current rect location and scale
                // Read directly from the glyph for the texture source and the rect dimensions. Screen rect is modified
                // by the spriteFont's scale and dimensions independent of the glyph size.
                poAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulRect(), poScreenRect, poColor);

                // Update and render the current character sprite
                poAzulSprite.Update();
                poAzulSprite.Render();

                // scribble
                xEnd = wTmp / 2 + xTmp + sep;
            }
        }

        // clear the sprite data
        private void privClear()
        {
            Debug.Assert(poAzulSprite != null);
            Debug.Assert(poColor != null);
            Debug.Assert(poScreenRect != null);

            poScreenRect.Set(0, 0, 0, 0);
            poColor.Set(1.0f, 1.0f, 1.0f);
            pMessage = null;
            glyphName = Glyph.Name.Uninitialized;
            x = 0.0f;
            y = 0.0f;
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
            SpriteFont pDataB = (SpriteFont)pTarget;
            return name == pDataB.name;
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());
            baseDump();
        }

        // associated font
        public Font.Name name;

        // underlying sprite re-used for each character
        private Azul.Sprite poAzulSprite;

        // Rect moved and used for each character
        private Azul.Rect poScreenRect;

        // text color
        private Azul.Color poColor;

        // string content to render
        private string pMessage;

        // read only glyph type to draw flyweight images from
        public Glyph.Name glyphName;

        public float x;
        public float y;

        public float scaleX = 2.5f;
        public float scaleY = 3.0f;
        public float sep = 6.5f;
    }
}