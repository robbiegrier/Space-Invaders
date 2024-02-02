using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Bomb : BombCategory
    {
        public Bomb(GameObject.Name name, SpriteGame.Name spriteName, FallStrategy pInStrategy, float posX, float posY, AlienBase pInCreator)
            : base(name, spriteName, posX, posY, BombCategory.Type.Bomb)
        {
            pCreator = pInCreator;
            speed = 1.5f;
            pFallStrategy = pInStrategy;
            pFallStrategy.Reset(y);
        }

        public override void Update()
        {
            base.Update();
            y -= speed;
            pFallStrategy.Fall(this);
        }

        public override void EndPlay()
        {
            base.EndPlay();

            SplatterRoot.MakeSplatter(SpriteGame.Name.AlienShotExplosion, x, y);
        }

        public void Reset()
        {
            y = 700.0f;
            pFallStrategy.Reset(this.y);
        }

        public override void Remove()
        {
            pCreator.SignalBombReady();

            base.Remove();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBomb(this);
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

        public float GetBoundingBoxHeight()
        {
            return GetCollisionObject().poColRect.height;
        }

        public void MultiplyScale(float sx, float sy)
        {
            pSpriteProxy.sx *= sx;
            pSpriteProxy.sy *= sy;
        }

        public void SetStrategy(FallStrategy pInStrategy)
        {
            pFallStrategy = pInStrategy;
            pFallStrategy.Reset(y);
        }

        public void SetCreator(AlienBase pInCreator)
        {
            pCreator = pInCreator;
        }

        public float speed;
        private FallStrategy pFallStrategy;
        private AlienBase pCreator;
    }
}