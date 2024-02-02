using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AnimationCommand : Command
    {
        public AnimationCommand(SpriteGame.Name spriteName)
        {
            pSprite = SpriteGameMan.Find(spriteName);
            Debug.Assert(pSprite != null);

            // LTN - own the list of images
            poImageSequence = new SLinkMan();
            Debug.Assert(poImageSequence != null);

            pIt = poImageSequence.GetIterator();
            Debug.Assert(pIt != null);
        }

        public void Attach(Image.Name imageName)
        {
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            // LTN - store long term on the image list
            ImageNode pImageHolder = new ImageNode(pImage);
            Debug.Assert(pImageHolder != null);

            poImageSequence.AddToFront(pImageHolder);

            pIt = poImageSequence.GetIterator();
            Debug.Assert(pIt != null);
        }

        public override void Execute(float deltaTime)
        {
            if (pIt.IsDone())
            {
                pIt.First();
            }

            ImageNode pImageNode = (ImageNode)pIt.Current();
            Debug.Assert(pImageNode != null);

            pIt.Next();
            pSprite.SwapImage(pImageNode.pImage);

            TimerEventMan.Add(TimerEvent.Name.Animation, this, pSprite.animationSpeed);
        }

        private SpriteGame pSprite;
        private SLinkMan poImageSequence;
        private Iterator pIt;
    }
}