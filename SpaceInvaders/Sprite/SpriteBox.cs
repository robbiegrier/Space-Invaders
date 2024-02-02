using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteBox : SpriteBase
    {
        public enum Name
        {
            Box,
            Box1,
            Box2,
            Box3,
            Box4,

            RedOutline,
            BlueOutline,
            OrangeOutline,
            PinkOutline,
            NullObject,

            Uninitialized
        }

        public SpriteBox()
            : base()
        {
            name = SpriteBox.Name.Uninitialized;

            Debug.Assert(psTmpRect != null);
            psTmpRect.Set(0f, 0f, 1f, 1f);

            // LTN - own the color long term
            poLineColor = new Azul.Color(1f, 1f, 1f);
            Debug.Assert(poLineColor != null);

            // LTN - own the underlying sprite box long term
            poAzulSpriteBox = new Azul.SpriteBox(psTmpRect, poLineColor);
            Debug.Assert(poAzulSpriteBox != null);

            x = poAzulSpriteBox.x;
            y = poAzulSpriteBox.y;
            sx = poAzulSpriteBox.sx;
            sy = poAzulSpriteBox.sy;
            angle = poAzulSpriteBox.angle;
        }

        public override void Update()
        {
            poAzulSpriteBox.x = x;
            poAzulSpriteBox.y = y;
            poAzulSpriteBox.sx = sx;
            poAzulSpriteBox.sy = sy;
            poAzulSpriteBox.angle = angle;

            poAzulSpriteBox.Update();
        }

        public override void Render()
        {
            poAzulSpriteBox.Render();
        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(poLineColor != null);
            poLineColor.Set(red, green, blue, alpha);
            poAzulSpriteBox.SwapColor(poLineColor);
        }

        public void SetRect(float x, float y, float width, float height)
        {
            Set(name, x, y, width, height);
        }

        public void Set(SpriteBox.Name inName, float inX, float inY, float inWidth, float inHeight, Azul.Color pInLineColor)
        {
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);
            Debug.Assert(psTmpRect != null);

            psTmpRect.Set(inX, inY, inWidth, inHeight);

            name = inName;

            if (pInLineColor == null)
            {
                poLineColor.Set(1f, 1f, 1f);
            }
            else
            {
                poLineColor.Set(pInLineColor);
            }

            poAzulSpriteBox.Swap(psTmpRect, poLineColor);
            poAzulSpriteBox.Update();

            x = poAzulSpriteBox.x;
            y = poAzulSpriteBox.y;
            sx = poAzulSpriteBox.sx;
            sy = poAzulSpriteBox.sy;
            angle = poAzulSpriteBox.angle;
        }

        public void Set(SpriteBox.Name name, float x, float y, float width, float height)
        {
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(psTmpRect != null);
            SpriteBox.psTmpRect.Set(x, y, width, height);

            this.name = name;

            this.poAzulSpriteBox.Swap(psTmpRect, this.poLineColor);

            this.x = poAzulSpriteBox.x;
            this.y = poAzulSpriteBox.y;
            this.sx = poAzulSpriteBox.sx;
            this.sy = poAzulSpriteBox.sy;
            this.angle = poAzulSpriteBox.angle;
        }

        private void privClear()
        {
            name = SpriteBox.Name.Uninitialized;
            poLineColor.Set(1f, 1f, 1f);
            x = 0f;
            y = 0f;
            sx = 1f;
            sy = 1f;
            angle = 0f;
        }

        public void SwapColor(Azul.Color inColor)
        {
            Debug.Assert(inColor != null);
            poAzulSpriteBox.SwapColor(inColor);
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            baseClear();
            privClear();
        }

        public override void Dump()
        {
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Color(r,b,g): {0},{1},{2} ({3})", this.poLineColor.red, this.poLineColor.green, this.poLineColor.blue, this.poLineColor.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSpriteBox.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);

            this.baseDump();
        }

        public Name name;
        public Azul.Color poLineColor;
        public Azul.SpriteBox poAzulSpriteBox;

        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        // LTN - own the underlying static azul rect long term
        private static Azul.Rect psTmpRect = new Azul.Rect();
    }
}