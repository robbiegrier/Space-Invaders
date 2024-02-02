using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdFactory
    {
        public BirdFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name spriteBatchOverlayName = SpriteBatch.Name.Boxes)
        {
            pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(pSpriteBatch != null);

            pSpriteBatchOverlay = SpriteBatchMan.Find(spriteBatchOverlayName);
            Debug.Assert(pSpriteBatchOverlay != null);
        }

        public GameObject Create(GameObject.Name type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObject = null;

            switch (type)
            {
                case GameObject.Name.GreenBird:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdGreen(SpriteGame.Name.GreenBird, posX, posY);
                    break;

                case GameObject.Name.RedBird:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdRed(SpriteGame.Name.RedBird, posX, posY);
                    break;

                case GameObject.Name.WhiteBird:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdWhite(SpriteGame.Name.WhiteBird, posX, posY);
                    break;

                case GameObject.Name.YellowBird:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdYellow(SpriteGame.Name.YellowBird, posX, posY);
                    break;

                case GameObject.Name.BirdGrid:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdGrid();
                    break;

                case GameObject.Name.BirdColumn:
                    // LTN - factory created but the caller will store long term
                    pGameObject = new BirdColumn();
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Debug.Assert(pGameObject != null);

            pGameObject.ActivateSprite(pSpriteBatch);
            pGameObject.ActivateCollisionSprite(pSpriteBatchOverlay);

            return pGameObject;
        }

        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBatchOverlay;
    }
}