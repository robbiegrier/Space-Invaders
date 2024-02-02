using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class CollisionObject
    {
        public CollisionObject(SpriteGameProxy pSpriteGameProxy)
        {
            Debug.Assert(pSpriteGameProxy != null);

            SpriteGame pSprite = pSpriteGameProxy.pSprite;
            Debug.Assert(pSprite != null);

            // LTN - This is owned by the collision object long term
            poColRect = new CollisionRect(pSprite.GetRect());
            Debug.Assert(poColRect != null);

            pColSprite = SpriteBoxMan.Add(SpriteBox.Name.Box, poColRect.x, poColRect.y, poColRect.width, poColRect.height);
            Debug.Assert(pColSprite != null);
            pColSprite.SetColor(1.0f, 0.0f, 0.0f);
        }

        public void UpdatePos(float x, float y)
        {
            poColRect.x = x;
            poColRect.y = y;

            pColSprite.x = poColRect.x;
            pColSprite.y = poColRect.y;

            pColSprite.SetRect(poColRect.x, poColRect.y, poColRect.width, poColRect.height);
            pColSprite.Update();
        }

        public void Resurrect(SpriteGameProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            SpriteGame pSprite = pSpriteProxy.pSprite;
            Debug.Assert(pSprite != null);

            Debug.Assert(poColRect != null);
            poColRect.Set(pSprite.GetRect());

            Debug.Assert(pColSprite != null);
            pColSprite.Set(SpriteBox.Name.Box, poColRect.x, poColRect.y, poColRect.width, poColRect.height);
            pColSprite.SetColor(1.0f, 1.0f, 1.0f);
        }

        public SpriteBox pColSprite;
        public CollisionRect poColRect;
    }
}