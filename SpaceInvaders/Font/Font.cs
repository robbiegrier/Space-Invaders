using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Font : DLink
    {
        public enum Name
        {
            TestMessage,
            TestOneOff,
            TimedCharacter,
            ScorePlayer1,
            ScorePlayer2,
            HighScore,

            NullObject,
            Uninitialized
        }

        public Font()
        {
            name = Name.Uninitialized;

            // LTN - owns the sprite font object
            poSpriteFont = new SpriteFont();
        }

        public void Set(Font.Name inName, string inMessage, Glyph.Name glyphName, float xStart, float yStart, int inSession = 0)
        {
            Debug.Assert(inMessage != null);
            name = inName;

            session = inSession;

            // delegate most properties to the spriteFont
            poSpriteFont.Set(name, inMessage, glyphName, xStart, yStart);
        }

        public void UpdateMessage(string pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(poSpriteFont != null);

            // delegate to the sprite font - update the text content
            poSpriteFont.UpdateMessage(pMessage);
        }

        private void privClear()
        {
            name = Name.Uninitialized;

            // clear the sprite
            poSpriteFont.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            privClear();
        }

        public void SetColor(float red, float green, float blue)
        {
            // just set the sprite font
            poSpriteFont.SetColor(red, green, blue);
        }

        public override bool Compare(NodeBase pTarget)
        {
            Debug.Assert(pTarget != null);
            Font pDataB = (Font)pTarget;
            return name == pDataB.name;
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());
            baseDump();
        }

        public void Remove()
        {
            SpriteBatchMan.Remove(poSpriteFont.GetSpriteNode());
        }

        // the font object id
        public Name name;

        // the sprite with which we will display the text
        public SpriteFont poSpriteFont;

        // the null default string
        private static string pNullString = "null";

        public int session = 0;
    }
}