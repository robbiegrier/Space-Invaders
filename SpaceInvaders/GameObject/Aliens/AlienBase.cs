using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class AlienBase : Leaf
    {
        public enum Type
        {
            Squid,
            Crab,
            Octopus
        }

        protected AlienBase(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName, spriteName, _x, _y)
        {
            pCollisionObject.pColSprite.SetColor(1.0f, 1.0f, 0.0f);
        }

        public override void BeginPlay()
        {
            base.BeginPlay();
        }

        public override void Update()
        {
            base.Update();

            bool shouldDropBombs = IteratorComposite.GetSibling(this) == null || GetGameObjectName() == GameObject.Name.Ufo;

            bool playerAlive = ShipMan.GetShip().GetStateName() != ShipState.Name.End;

            if (shouldDropBombs && playerAlive)
            {
                if (bombReady)
                {
                    Random rand = new Random((int)(GlobalTimer.GetTime() * 10000f) + (int)(x * y) + GetHashCode());
                    int roll = rand.Next(5000);
                    if (roll < 2)
                    {
                        bombReady = false;
                        MakeRandomBomb();
                    }
                }
            }
        }

        private void MakeRandomBomb()
        {
            BombRoot pBombRoot = (BombRoot)GameObjectNodeMan.Find(GameObject.Name.BombRoot);

            Random rand = new Random();
            int choice = rand.Next(3);

            SpriteGame.Name bombSpriteName;
            FallStrategy pBombFallStrategy;

            switch (choice)
            {
                case 0:
                    bombSpriteName = SpriteGame.Name.SquigglyShotA;
                    pBombFallStrategy = new FallZigZag();
                    break;

                case 1:
                    bombSpriteName = SpriteGame.Name.PlungerShotA;
                    pBombFallStrategy = new FallDagger();
                    break;

                default:
                    bombSpriteName = SpriteGame.Name.RollingShotA;
                    pBombFallStrategy = new FallStraight();
                    break;
            }

            Bomb pBomb;

            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Bomb);

            if (pGameObjNode == null)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, bombSpriteName, pBombFallStrategy, x, y, this);
            }
            else
            {
                pBomb = (Bomb)pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);
                pBomb.Resurrect();
                pBomb.x = x;
                pBomb.y = y;
                pBomb.SetSprite(bombSpriteName);
                pBomb.SetStrategy(pBombFallStrategy);
                pBomb.SetCreator(this);

                //switch (choice)
                //{
                //    case 0:
                //        pBombFallStrategy = new FallZigZag();
                //        break;

                //    case 1:
                //        bombSpriteName = SpriteGame.Name.PlungerShotA;
                //        pBombFallStrategy = new FallDagger();
                //        break;

                //    default:
                //        bombSpriteName = SpriteGame.Name.RollingShotA;
                //        pBombFallStrategy = new FallStraight();
                //        break;
                //}
            }

            pBomb.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pBomb.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Bombs));

            pBombRoot.Add(pBomb);
            pBomb.Update();
            pBomb.BeginPlay();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(m);
            CollisionPair.Collide(pGameObject, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public void SignalBombReady()
        {
            bombReady = true;
        }

        private bool bombReady = true;
    }
}