using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShieldFactory
    {
        public ShieldFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pInTree)
        {
            pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(pSpriteBatch != null);

            pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(pCollisionSpriteBatch != null);

            Debug.Assert(pInTree != null);
            pTree = pInTree;
        }

        public void SetParent(GameObject pParentNode)
        {
            Debug.Assert(pParentNode != null);
            pTree = (Composite)pParentNode;
        }

        public GameObject Create(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            GameObjectNode pGameObjNode = GhostMan.Find(gameName);
            if (pGameObjNode != null)
            {
                pShield = pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);

                //GhostMan.Dump();

                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                        pShield.SetSprite(SpriteGame.Name.Brick);
                        break;

                    case ShieldCategory.Type.LeftTop1:
                        pShield.SetSprite(SpriteGame.Name.Brick_LeftTop1);
                        break;

                    case ShieldCategory.Type.LeftTop0:
                        pShield.SetSprite(SpriteGame.Name.Brick_LeftTop0);
                        break;

                    case ShieldCategory.Type.LeftBottom:
                        pShield.SetSprite(SpriteGame.Name.Brick_LeftBottom);
                        break;

                    case ShieldCategory.Type.RightTop1:
                        pShield.SetSprite(SpriteGame.Name.Brick_RightTop1);
                        break;

                    case ShieldCategory.Type.RightTop0:
                        pShield.SetSprite(SpriteGame.Name.Brick_RightTop0);
                        break;

                    case ShieldCategory.Type.RightBottom:
                        pShield.SetSprite(SpriteGame.Name.Brick_RightBottom);
                        break;
                }

                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                    case ShieldCategory.Type.LeftTop1:
                    case ShieldCategory.Type.LeftTop0:
                    case ShieldCategory.Type.LeftBottom:
                    case ShieldCategory.Type.RightTop1:
                    case ShieldCategory.Type.RightTop0:
                    case ShieldCategory.Type.RightBottom:
                        ((ShieldBrick)pShield).Resurrect(posX, posY);
                        break;

                    case ShieldCategory.Type.Root:
                        Debug.Assert(false);
                        break;

                    case ShieldCategory.Type.Grid:
                        ((ShieldGrid)pShield).Resurrect(posX, posY);
                        break;

                    case ShieldCategory.Type.Column:
                        ((ShieldColumn)pShield).Resurrect(posX, posY);
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick, posX, posY);
                        break;

                    case ShieldCategory.Type.LeftTop1:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop1, posX, posY);
                        break;

                    case ShieldCategory.Type.LeftTop0:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop0, posX, posY);
                        break;

                    case ShieldCategory.Type.LeftBottom:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftBottom, posX, posY);
                        break;

                    case ShieldCategory.Type.RightTop1:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop1, posX, posY);
                        break;

                    case ShieldCategory.Type.RightTop0:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop0, posX, posY);
                        break;

                    case ShieldCategory.Type.RightBottom:
                        pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightBottom, posX, posY);
                        break;

                    case ShieldCategory.Type.Root:
                        pShield = new ShieldRoot(gameName, SpriteGame.Name.NullObject, posX, posY);
                        pShield.SetCollisionColor(0.0f, 1.0f, 1.0f);
                        break;

                    case ShieldCategory.Type.Column:
                        pShield = new ShieldColumn(gameName, SpriteGame.Name.NullObject, posX, posY);
                        pShield.SetCollisionColor(1.0f, 0.0f, 0.0f);
                        break;

                    case ShieldCategory.Type.Grid:
                        pShield = new ShieldGrid(gameName, SpriteGame.Name.NullObject, posX, posY);
                        pShield.SetCollisionColor(0.0f, 1.0f, 1.0f);
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            pTree.Add(pShield);
            pShield.ActivateSprite(pSpriteBatch);
            pShield.ActivateCollisionSprite(pCollisionSpriteBatch);

            return pShield;
        }

        private SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;
    }
}