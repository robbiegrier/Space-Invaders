using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShieldRoot : Composite
    {
        public ShieldRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            x = posX;
            y = posY;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldRoot(this);
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

        public void Resurrect(float posX, float posY)
        {
            x = posX;
            y = posY;
            base.Resurrect();
        }

        public static void LoadBrickSprites()
        {
            SpriteGameMan.Add(SpriteGame.Name.Brick, Image.Name.Brick, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, brickWidth, brickHeight);
        }

        public static ShieldRoot Generate()
        {
            ShieldRoot pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pShieldRoot.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pShieldRoot.SetCollisionColor(0, 0, 1);
            GameObjectNodeMan.Attach(pShieldRoot);

            pShieldRoot.Regenerate();

            return pShieldRoot;
        }

        public void Regenerate()
        {
            float screenWidth = SpaceInvaders.WIDTH;
            IteratorReverseComposite pRev = new IteratorReverseComposite(this);
            for (pRev.First(); !pRev.IsDone(); pRev.Next())
            {
                GameObject pTmp = (GameObject)pRev.Curr();

                if (pTmp.GetGameObjectName() == GameObject.Name.ShieldBrick)
                {
                    pTmp.Remove();
                }
            }

            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, this);
            float shieldSep = 140f;
            float shieldStart = ((screenWidth / 2) - (shieldSep / 2) - 35) - shieldSep;
            float shieldHeight = 175f;

            GenerateGrid(shieldStart, shieldHeight, SF, this);
            GenerateGrid(shieldStart + (shieldSep * 1), shieldHeight, SF, this);
            GenerateGrid(shieldStart + (shieldSep * 2), shieldHeight, SF, this);
            GenerateGrid(shieldStart + (shieldSep * 3), shieldHeight, SF, this);

            pRev = new IteratorReverseComposite(this);
            for (pRev.First(); !pRev.IsDone(); pRev.Next())
            {
                GameObject pTmp = (GameObject)pRev.Curr();

                if (pTmp != this)
                {
                    pTmp.BeginPlay();
                }
            }

            //GhostMan.DumpStats();
            //SpriteGameProxyMan.DumpStats();

            //TextureMan.DumpStats();
            //ImageMan.DumpStats();
            //SpriteGameMan.DumpStats();
            //SpriteBatchMan.DumpStats();
            //SpriteBoxMan.DumpStats();
            //TimerEventMan.DumpStats();
            //SpriteGameProxyMan.DumpStats();
            //GameObjectNodeMan.DumpStats();
            //CollisionPairMan.DumpStats();
        }

        public static ShieldGrid GenerateGrid(float startX, float startY, ShieldFactory SF, ShieldRoot pRoot)
        {
            SF.SetParent(pRoot);

            ShieldGrid pShieldGrid = (ShieldGrid)SF.Create(ShieldCategory.Type.Grid, Name.ShieldGrid);// new ShieldGrid(GameObject.Name.ShieldGrid, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            //pShieldGrid.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pShieldGrid.SetCollisionColor(0, 1, 1);
            SF.SetParent(pShieldGrid);

            GameObject pColumn;

            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            float start_x = startX;
            float start_y = startY;
            float off_x = 0;

            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            SF.SetParent(pShieldGrid);
            pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn);

            SF.SetParent(pColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);

            return pShieldGrid;
        }

        public static readonly float brickWidth = 10f;
        public static readonly float brickHeight = 10f;
    }
}