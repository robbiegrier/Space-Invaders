//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System.Diagnostics;

namespace SpaceInvaders
{
    public class Image : SLink
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
            PeaShooter,
            Skeleton,
            Runner,
            FlyingBird,
            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            OctopusA,
            OctopusB,
            CrabA,
            CrabB,
            SquidA,
            SquidB,
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

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            NullObject,

            HotPink,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Image()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;

            // LTN - store the rect for this image
            this.poRect = new Azul.Rect();
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name name, Texture pSrcTexture, float x, float y, float w, float h)
        {
            Debug.Assert(pSrcTexture != null);
            this.pTexture = pSrcTexture;

            // Remember the allocation was already made in constructor
            // so don't remove... replace the data
            this.poRect.Set(x, y, w, h);

            this.name = name;
        }

        private void privClear()
        {
            Debug.Assert(this.poRect != null);
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect.Clear();
        }

        //------------------------------------
        // Override
        //------------------------------------
        public override System.Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            this.Clear();
            this.privClear();
        }

        public override void Dump()
        {
            // we are using HASH code as its unique identifier
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            if (this.pTexture == null)
            {
                Debug.WriteLine("      Texture: null");
            }
            else
            {
                Debug.WriteLine("      Texture: {0}", this.pTexture.name);
            }
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            // Let the base print its contribution
            this.baseDump();
        }

        public Azul.Texture GetAzulTexture()
        {
            return pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            return poRect;
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;

        public Azul.Rect poRect;
        public Texture pTexture;
    }
}

// --- End of File ---