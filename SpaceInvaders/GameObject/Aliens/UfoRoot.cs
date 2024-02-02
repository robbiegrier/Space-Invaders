using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class UfoRoot : Composite
    {
        public UfoRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
            pCollisionObject.pColSprite.SetColor(1f, 0.5f, 0.5f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitUfoRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }

        public static Ufo SpawnUfo()
        {
            Ufo pOutput = null;

            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Ufo);

            if (pGameObjNode == null)
            {
                pOutput = new Ufo(SpriteGame.Name.Saucer, 0, 0);
            }
            else
            {
                pOutput = (Ufo)pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);
                pOutput.Resurrect();
            }

            pOutput.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pOutput.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Aliens));

            GameObject pParent = GameObjectNodeMan.Find(GameObject.Name.UfoRoot);
            Debug.Assert(pParent != null);
            pParent.Add(pOutput);

            pOutput.Update();

            pOutput.BeginPlay();

            return pOutput;
        }
    }
}