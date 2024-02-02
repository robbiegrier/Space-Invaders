//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Aliens,
            Stitch,
            Birds,
            PeaShooter,
            HotPink,
            Skeleton,
            Runner,
            FlyingBird,
            PacMan,
            SpaceInvaders,
            Consolas20pt,
            Consolas36pt,
            Shield,
            NullObject,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Texture()
        : base()
        {
            // LTN - own the texture
            this.poAzulTexture = new Azul.Texture();
            Debug.Assert(this.poAzulTexture != null);

            this.name = Texture.Name.Uninitialized;
        }

        public Texture(Name name, string pTextureName)
        : base()
        {
            Debug.Assert(pTextureName != null);

            // LTN - own the texture
            this.poAzulTexture = new Azul.Texture(pTextureName);
            Debug.Assert(this.poAzulTexture != null);

            this.name = name;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name name, string pTextureName)
        {
            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);

            this.poAzulTexture.Set(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            this.name = name;
        }

        private void privClear()
        {
            Debug.Assert(this.poAzulTexture != null);
            this.poAzulTexture.Set("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            this.name = Name.Uninitialized;
        }

        public override System.Enum GetName()
        {
            return this.name;
        }

        public override void Wash()
        {
            Clear();
            privClear();
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());
            Debug.WriteLine("   Name: {0} ({1})", name, GetHashCode());

            if (poAzulTexture == null)
            {
                Debug.WriteLine("      poAzulTexture: {0} ", poAzulTexture.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      poAzulTexture: {0} ", poAzulTexture.GetHashCode());
            }

            baseDump();
        }

        public Azul.Texture GetAzulTexture()
        {
            return poAzulTexture;
        }

        public Name name;
        public Azul.Texture poAzulTexture;
    }
}

// --- End of File ---