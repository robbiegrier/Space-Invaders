//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteGame : SpriteBase
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,
            BlueBird,

            BombStraight,
            BombZigZag,
            BombCross,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,
            PeaShooter,
            Skeleton,
            Runner,
            FlyingBird,

            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            Octopus,
            Crab,
            Squid,
            AlienExplosion,
            Saucer,
            SaucerExplosion,
            Player,
            PlayerExplosionA,
            PlayerExplosionB,
            AlienPullYA,
            AlienPullYB,
            AlienPullUpisdeDownYA,
            AlienPullUpsideDownYB,
            PlayerShot,
            PlayerShotExplosion,
            SquigglyShotA,
            SquigglyShotB,
            SquigglyShotC,
            SquigglyShotD,
            PlungerShotA,
            PlungerShotB,
            PlungerShotC,
            PlungerShotD,
            RollingShotA,
            RollingShotB,
            RollingShotC,
            RollingShotD,
            AlienShotExplosion,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            LessThan,
            GreaterThan,
            Space,
            Equals,
            Asterisk,
            Question,
            Hyphen,

            Compare,
            NullObject,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        public SpriteGame()
            : base()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

            this.name = Name.Uninitialized;
            this.pImage = null;

            // LTN - Own the color long term
            this.poColor = new Azul.Color();
            Debug.Assert(this.poColor != null);

            // LTN - Own the sprite long term
            this.poAzulSprite = new Azul.Sprite();
            Debug.Assert(this.poAzulSprite != null);

            // LTN - Own the rect long term
            poRect = new Azul.Rect();
            Debug.Assert(poRect != null);
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public override void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            this.poAzulSprite.Update();
        }

        public override void Render()
        {
            this.poAzulSprite.Render();
        }

        public void Set(Name name, Image pImage, float _x, float _y, float _w, float _h, Azul.Color pInColor)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);

            this.pImage = pImage;
            this.name = name;

            if (pInColor == null)
            {
                this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.poColor.Set(pInColor);
            }

            this.poRect.Set(_x, _y, _w, _h);
            this.poAzulSprite.Swap(pImage.pTexture.poAzulTexture, pImage.poRect, poRect, poColor);

            this.poAzulSprite.Update();

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

        private void privClear()
        {
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poAzulSprite != null);

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

            this.name = Name.Uninitialized;
            this.pImage = null;

            this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);

            Image pImage = ImageMan.Find(Image.Name.HotPink);
            Debug.Assert(pImage != null);

            this.poRect.Set(0.0f, 0.0f, 1.0f, 1.0f);
            this.poAzulSprite.Swap(pImage.pTexture.poAzulTexture, pImage.poRect, poRect, poColor);
            this.poAzulSprite.Update();
        }

        public void SwapColor(Azul.Color _pColor)
        {
            Debug.Assert(_pColor != null);
            Debug.Assert(poColor != null);
            Debug.Assert(poAzulSprite != null);

            poColor.Set(_pColor);
            poAzulSprite.SwapColor(_pColor);
        }

        public void SwapColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(poColor != null);
            Debug.Assert(poAzulSprite != null);

            poColor.Set(red, green, blue, alpha);
            poAzulSprite.SwapColor(poColor);
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(poAzulSprite != null);
            Debug.Assert(pNewImage != null);
            pImage = pNewImage;

            poAzulSprite.SwapTexture(pImage.GetAzulTexture());
            poAzulSprite.SwapTextureRect(pImage.GetAzulRect());
        }

        public Azul.Rect GetRect()
        {
            Debug.Assert(poRect != null);
            return poRect;
        }

        public void Animate(Image.Name pImageA, Image.Name pImageB, float speed)
        {
            // LTN - Command is passed in to the timer event and owned long term there
            AnimationCommand pAnimCmd = new AnimationCommand(name);
            pAnimCmd.Attach(pImageA);
            pAnimCmd.Attach(pImageB);
            TimerEventMan.Add(TimerEvent.Name.Animation, pAnimCmd, speed);
            animationSpeed = speed;
        }

        public void Animate(Image.Name pImageA, Image.Name pImageB, Image.Name pImageC, Image.Name pImageD, float speed)
        {
            // LTN - Command is passed in to the timer event and owned long term there
            AnimationCommand pAnimCmd = new AnimationCommand(name);
            pAnimCmd.Attach(pImageA);
            pAnimCmd.Attach(pImageB);
            pAnimCmd.Attach(pImageC);
            pAnimCmd.Attach(pImageD);
            TimerEventMan.Add(TimerEvent.Name.Animation, pAnimCmd, speed);
        }

        //------------------------------------
        // Override
        //------------------------------------
        public override System.Enum GetName()
        {
            return this.name;
        }

        public override void Wash()
        {
            this.baseClear();
            this.privClear();
        }

        public override void Dump()
        {
            // we are using HASH code as its unique identifier
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.name, this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);
            Debug.WriteLine("     Rect(x,y,w,h): {0},{1},{2},{3}", poRect.x, poRect.y, poRect.width, poRect.height);

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public float x;

        public float y;
        public float sx;
        public float sy;
        public float angle;

        public Name name;
        public Image pImage;
        public Azul.Color poColor;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poRect;

        public float animationSpeed = 0.5f;
    }
}

// --- End of File ---