using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Ship : ShipCategory
    {
        public Ship(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShipCategory.Type.Ship)
        {
            x = posX;
            y = posY;
            shipSpeed = 1.5f;
            pState = null;
        }

        public override void Update()
        {
            x = Math.Max(40f, x);
            x = Math.Min(x, SpaceInvaders.WIDTH - 40f);
            base.Update();
        }

        public void MoveRight()
        {
            pState.MoveRight(this);
        }

        public void MoveLeft()
        {
            pState.MoveLeft(this);
        }

        public void Shoot()
        {
            pState.ShootMissile(this);
        }

        public void Handle()
        {
            pState.Handle(this);
        }

        public void SetState(ShipMan.State inState)
        {
            pState = ShipMan.GetState(inState);
        }

        public ShipState.Name GetStateName()
        {
            return pState.name;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShip(this);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair pColPair = CollisionPairMan.GetActiveCollisionPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public int GetLives()
        {
            return FetchLives();
        }

        public void OnDeath()
        {
            if (ShipMan.GetMissile() != null)
            {
                ShipMan.GetMissile().Destroy();
            }

            SetSprite(SpriteGame.Name.PlayerExplosionA);
            SetState(ShipMan.State.Dead);

            UpdateLives(FetchLives() - 1);

            if (IsOutOfLives() && PlayerMan.GetGameMode() == PlayerMan.Mode.SinglePlayer && PlayerMan.GetActivePlayer().name == Player.Name.Player1)
            {
            }
            else
            {
                ShipRespawnCommand pRespawnCommand = new ShipRespawnCommand(this);
                TimerEventMan.Add(TimerEvent.Name.ShipRespawn, pRespawnCommand, 2f);
            }
        }

        public void Respawn()
        {
            SetSprite(SpriteGame.Name.Player);
            SetState(ShipMan.State.Ready);
            x = 200;
            y = 100;
        }

        public void Reset()
        {
            Respawn();
            UpdateLives(Player.maxLives);
            UpdateScore(0);
        }

        public void BindLivesWidget(Font pInWidget)
        {
            pLivesWidget = pInWidget;
        }

        public void BindLivesIconWidget(Font pInWidget)
        {
            pLivesIconWidget = pInWidget;
        }

        public void BindScoreWidget(Font pInWidget)
        {
            pScoreWidget = pInWidget;
        }

        private void UpdateLivesWidget()
        {
            pLivesWidget.UpdateMessage(FetchLives().ToString());

            string livesIcons = "";

            for (int i = 0; i < FetchLives() - 1; i++)
            {
                livesIcons += 'e';
            }

            pLivesIconWidget.UpdateMessage(livesIcons);
        }

        public bool IsOutOfLives()
        {
            return FetchLives() < 1;
        }

        public void AddScore(int addedScore)
        {
            UpdateScore(FetchScore() + addedScore);
        }

        public void UpdateScore(int val)
        {
            PlayerMan.GetActivePlayer().SetScore(val);
            pScoreWidget.UpdateMessage(FetchScore().ToString());
        }

        public void UpdateLives(int val)
        {
            PlayerMan.GetActivePlayer().SetLives(Math.Max(0, val));
            UpdateLivesWidget();
        }

        public int GetScore()
        {
            return FetchScore();
        }

        private int FetchLives()
        {
            return PlayerMan.GetActivePlayer().GetLives();
        }

        private int FetchScore()
        {
            return PlayerMan.GetActivePlayer().GetScore();
        }

        public float shipSpeed;
        private ShipState pState;

        private Font pLivesWidget = null;
        private Font pLivesIconWidget = null;
        private Font pScoreWidget = null;
    }
}