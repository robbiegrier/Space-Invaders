using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // An slink holding an image
    class ImageNode : SLink
    {
        public ImageNode(Image _pImage)
            : base()
        {
            Debug.Assert(_pImage != null);
            this.pImage = _pImage;
        }

        private void privClean()
        {
            pImage = null;
        }

        public override void Wash()
        {
            privClean();
        }

        public override void Dump()
        {
            Debug.WriteLine("   ({0}) node", this.GetHashCode());
            Debug.WriteLine("   pImage: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
            baseDump();
        }

        public override System.Enum GetName()
        {
            return pImage.GetName();
        }

        public Image pImage;
    }
}
