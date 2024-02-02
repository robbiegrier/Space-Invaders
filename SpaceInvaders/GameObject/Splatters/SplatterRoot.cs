using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SplatterRoot : Composite
    {
        public SplatterRoot(SpriteGame.Name spriteName, float posX, float posY)
            : base(Name.SplatterRoot, spriteName)
        {
            x = posX;
            y = posY;
        }

        public override void Accept(CollisionVisitor other)
        {
            // no collision
        }

        public override void Update()
        {
            base.Update();
        }

        public static Splatter MakeSplatter(SpriteGame.Name splatterImage, float inX, float inY)
        {
            Splatter pOutput = null;

            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Splatter);

            if (pGameObjNode == null)
            {
                pOutput = new Splatter(SpriteGame.Name.AlienExplosion, inX, inY);
            }
            else
            {
                pOutput = (Splatter)pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);
                pOutput.Resurrect();
                pOutput.x = inX;
                pOutput.y = inY;
            }

            pOutput.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pOutput.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Splatters));

            pOutput.SetSprite(splatterImage);
            GameObject pParent = GameObjectNodeMan.Find(GameObject.Name.SplatterRoot);
            Debug.Assert(pParent != null);
            pParent.Add(pOutput);

            pOutput.Update();

            pOutput.BeginPlay();

            return pOutput;
        }
    }
}