using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class FallZigZag : FallStrategy
    {
        public FallZigZag()
        {
            oldHeight = 0.0f;
        }

        public override void Reset(float height)
        {
            oldHeight = height;
        }

        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            float targetY = oldHeight - 1.0f * pBomb.GetBoundingBoxHeight();

            if (pBomb.y < targetY)
            {
                pBomb.MultiplyScale(-1.0f, 1.0f);
                oldHeight = targetY;
            }
        }

        // Data
        private float oldHeight;
    }
}