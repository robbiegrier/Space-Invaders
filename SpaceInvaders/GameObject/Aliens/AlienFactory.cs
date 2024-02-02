using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienFactory
    {
        public AlienFactory(SpriteBatch.Name spriteBatchName = SpriteBatch.Name.Aliens, SpriteBatch.Name spriteBatchOverlayName = SpriteBatch.Name.Boxes)
        {
            pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(pSpriteBatch != null);

            pSpriteBatchOverlay = SpriteBatchMan.Find(spriteBatchOverlayName);
            Debug.Assert(pSpriteBatchOverlay != null);
        }

        public GameObject Create(GameObject.Name type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObject = null;

            GameObjectNode pGameObjNode = GhostMan.Find(type);

            if (pGameObjNode != null)
            {
                pGameObject = (GameObject)pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);
                pGameObject.Resurrect();
                pGameObject.x = posX;
                pGameObject.y = posY;
                pGameObject.Update();
            }
            else
            {
                switch (type)
                {
                    case GameObject.Name.SquidAlien:
                        // LTN - factory created but the caller will store long term
                        pGameObject = new Squid(SpriteGame.Name.Squid, posX, posY);
                        break;

                    case GameObject.Name.CrabAlien:
                        // LTN - factory created but the caller will store long term
                        pGameObject = new Crab(SpriteGame.Name.Crab, posX, posY);
                        break;

                    case GameObject.Name.OctopusAlien:
                        // LTN - factory created but the caller will store long term
                        pGameObject = new Octopus(SpriteGame.Name.Octopus, posX, posY);
                        break;

                    case GameObject.Name.AlienGrid:
                        // LTN - factory created but the caller will store long term
                        pGameObject = new AlienGrid();
                        break;

                    case GameObject.Name.AlienColumn:
                        // LTN - factory created but the caller will store long term
                        pGameObject = new AlienColumn();
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            Debug.Assert(pGameObject != null);

            pSpriteBatch.Attach(pGameObject);
            pSpriteBatchOverlay.Attach(pGameObject.GetCollisionObject().pColSprite);
            return pGameObject;
        }

        public AlienGrid Generate(float startBottomLeftX, float startBottomLeftY, float horizontalStep, float verticalStep, int columns)
        {
            AlienGrid pGrid = (AlienGrid)Create(GameObject.Name.AlienGrid);
            GameObjectNodeMan.Attach(pGrid);

            Regenerate(pGrid, startBottomLeftX, startBottomLeftY, horizontalStep, verticalStep, columns);

            //for (int i = 0; i < columns; i++)
            //{
            //    int row = 0;
            //    AlienColumn pColumn = (AlienColumn)Create(GameObject.Name.AlienColumn);
            //    pColumn.Add(Create(GameObject.Name.OctopusAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
            //    pColumn.Add(Create(GameObject.Name.OctopusAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
            //    pColumn.Add(Create(GameObject.Name.CrabAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
            //    pColumn.Add(Create(GameObject.Name.CrabAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
            //    pColumn.Add(Create(GameObject.Name.SquidAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
            //    pGrid.Add(pColumn);
            //}

            return pGrid;
        }

        public void Regenerate(AlienGrid pGrid, float startBottomLeftX, float startBottomLeftY, float horizontalStep, float verticalStep, int columns)
        {
            IteratorReverseComposite pRev = new IteratorReverseComposite(pGrid);
            for (pRev.First(); !pRev.IsDone(); pRev.Next())
            {
                GameObject pTmp = (GameObject)pRev.Curr();

                if (pTmp.GetGameObjectName() == GameObject.Name.CrabAlien ||
                    pTmp.GetGameObjectName() == GameObject.Name.OctopusAlien ||
                    pTmp.GetGameObjectName() == GameObject.Name.SquidAlien)
                {
                    pTmp.Remove();
                }
            }

            for (int i = 0; i < columns; i++)
            {
                int row = 0;
                AlienColumn pColumn = (AlienColumn)Create(GameObject.Name.AlienColumn);
                pColumn.Add(Create(GameObject.Name.OctopusAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(Create(GameObject.Name.OctopusAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(Create(GameObject.Name.CrabAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(Create(GameObject.Name.CrabAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(Create(GameObject.Name.SquidAlien, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pGrid.Add(pColumn);
            }
        }

        private SpriteBatch pSpriteBatch;
        private SpriteBatch pSpriteBatchOverlay;
    }
}