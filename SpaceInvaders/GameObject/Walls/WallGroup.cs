using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class WallGroup : Composite
    {
        public WallGroup(GameObject.Name inName, SpriteGame.Name spriteName, float posX, float posY)
            : base(inName, spriteName)
        {
            x = posX;
            y = posY;
            pCollisionObject.pColSprite.SetColor(1, 1, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallGroup(this);
        }

        public override void VisitBirdGrid(BirdGrid a)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(a, pGameObj);
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(a, pGameObj);
        }

        public override void VisitUfoRoot(UfoRoot a)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(a);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitUfo(Ufo m)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb b)
        {
            GameObject pGameObj = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public static WallGroup Generate(float screenWidth, float screenHeight, float wallDistance)
        {
            SpriteBatch pBoxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.AngryBirds));
            pWallGroup.ActivateCollisionSprite(pBoxes);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, (screenWidth / 2f) + wallDistance, screenHeight / 2f, 50, 800);
            pWallRight.ActivateCollisionSprite(pBoxes);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, (screenWidth / 2f) - wallDistance, screenHeight / 2f, 50, 800);
            pWallLeft.ActivateCollisionSprite(pBoxes);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, screenWidth / 2f, screenHeight - 10f, 850, 30);
            pWallTop.ActivateCollisionSprite(pBoxes);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.NullObject, screenWidth / 2f, 30f, 850, 60);
            pWallBottom.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Texts));

            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);

            GameObjectNodeMan.Attach(pWallGroup);

            return pWallGroup;
        }
    }
}