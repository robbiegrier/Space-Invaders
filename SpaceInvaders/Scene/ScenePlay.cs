using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ScenePlay : SceneState
    {
        public ScenePlay()
        {
            Initialize();
        }

        public override void Initialize()
        {
            GrabManagers();

            pToggleBatch = SpriteBatchMan.Add(SpriteBatch.Name.Boxes, 999_999_999);
            pToggleBatch.SetVisibility(false);
            SpriteBatchMan.Add(SpriteBatch.Name.AngryBirds, 25_000);
            SpriteBatchMan.Add(SpriteBatch.Name.Splatters, 20_001);
            SpriteBatchMan.Add(SpriteBatch.Name.Aliens, 20_000);
            SpriteBatchMan.Add(SpriteBatch.Name.Shields, 14_500);
            SpriteBatchMan.Add(SpriteBatch.Name.Missiles, 15_000);
            SpriteBatchMan.Add(SpriteBatch.Name.Players, 15_001);
            SpriteBatchMan.Add(SpriteBatch.Name.Bombs, 15_002);
            SpriteBatchMan.Add(SpriteBatch.Name.Texts, 999_999_999);

            SpriteGameMan.Find(SpriteGame.Name.Squid).Animate(Image.Name.SquidA, Image.Name.SquidB, 0.5f);
            SpriteGameMan.Find(SpriteGame.Name.Crab).Animate(Image.Name.CrabA, Image.Name.CrabB, 0.5f);
            SpriteGameMan.Find(SpriteGame.Name.Octopus).Animate(Image.Name.OctopusA, Image.Name.OctopusB, 0.5f);
            SpriteGameMan.Find(SpriteGame.Name.PlayerExplosionA).Animate(Image.Name.PlayerExplosionA, Image.Name.PlayerExplosionB, 0.2f);

            SetupInput();

            ShipMan.Create();

            pAlienGroup = AlienGrid.Generate(86f, alienHeightStart, 50f, 50f, columns);
            MissileGroup pMissileGroup = MissileGroup.Generate();
            WallGroup pWallGroup = WallGroup.Generate(SpaceInvaders.WIDTH, SpaceInvaders.HEIGHT, 340f);
            pShieldGroup = ShieldRoot.Generate();
            BombRoot pBombRoot = GenBombs();
            ShipRoot pShipRoot = ShipMan.GetShipRoot();
            UfoRoot pUfoGroup = GenUfos();
            GenSplatters();

            SetupCollisions(pAlienGroup, pMissileGroup, pWallGroup, pBombRoot, pShipRoot, pUfoGroup);

            Font pLivesWidget = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "3", Glyph.Name.SpaceInvaders, 15, 40);
            pLivesWidget.SetColor(0.9f, 0.9f, 0.9f);

            Font pLivesIconWidget = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "ee", Glyph.Name.SpaceInvaders, 50, 40);
            pLivesIconWidget.SetColor(0.0f, 0.9f, 0.0f);

            float topTextMargin = 20f;
            Font pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.SpaceInvaders, 20, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            Font pScoreWidget1 = FontMan.Add(Font.Name.ScorePlayer1, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, 30, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pScoreWidget1.SetColor(0.9f, 0.9f, 0.9f);
            pScoreWidget1.UpdateMessage(PlayerMan.GetPlayer1().GetScore().ToString());

            pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.SpaceInvaders, SpaceInvaders.MIDWIDTH - 80, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            pFont = FontMan.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, SpaceInvaders.MIDWIDTH - 35, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pFont.SetColor(0.90f, 0.90f, 0.90f);
            pFont.UpdateMessage(SpaceInvaders.HIGH_SCORE.ToString());

            pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.SpaceInvaders, SpaceInvaders.WIDTH - 200, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            Font pScoreWidget2 = FontMan.Add(Font.Name.ScorePlayer2, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, SpaceInvaders.WIDTH - 180, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pScoreWidget2.SetColor(0.90f, 0.90f, 0.90f);
            pScoreWidget2.UpdateMessage(PlayerMan.GetPlayer2().GetScore().ToString());

            ShipMan.GetShip().BindLivesIconWidget(pLivesIconWidget);
            ShipMan.GetShip().BindLivesWidget(pLivesWidget);

            if (PlayerMan.GetActivePlayer().name == Player.Name.Player1)
            {
                ShipMan.GetShip().BindScoreWidget(pScoreWidget1);
            }
            else
            {
                ShipMan.GetShip().BindScoreWidget(pScoreWidget2);
            }

            GameObjectNodeMan.BeginPlay();
        }

        public override void Update(float systemTime)
        {
            HandleBoxToggle();
            HandleShieldRegen();
            HandlePlayerRegen();
            HandleAliensRegen();
            HandleAliensDifficulty();
            HandleUfo();

            InputMan.Update();
            Simulation.Update(systemTime);

            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimerEventMan.Update(Simulation.GetTotalTime());
                GameObjectNodeMan.Update();
                SoundSystem.Update();
                CollisionPairMan.Process();
                DeathMan.Process();
            }

            if (pAlienGroup.AllAliensDead())
            {
                level++;
                alienHeightStart = Math.Max(250f, alienHeightStart - 30f);
                pAlienGroup.Regenerate(86f, alienHeightStart, 50f, 50f, columns);
                pAlienGroup.ResetDifficutly();

                for (int i = 0; i < level * 15; i++)
                {
                    pAlienGroup.IncreaseDifficulty();
                }

                pShieldGroup.Regenerate();
            }

            if (PlayerMan.GetGameMode() == PlayerMan.Mode.SinglePlayer)
            {
                if (pAlienGroup.y < minAlienHeight)
                {
                    UpdateHighScore();
                    SpaceInvaders.ChangeScene(SceneContext.Scene.Over);
                }
                else if (!awaitingSceneChange && ShipMan.GetShip().IsOutOfLives())
                {
                    UpdateHighScore();
                    awaitingSceneChange = true;
                    SceneChangeCommand pSceneChangeCmd = new SceneChangeCommand(SceneContext.Scene.Over);
                    TimerEventMan.Add(TimerEvent.Name.SceneChange, pSceneChangeCmd, 3f);
                }
            }
            else
            {
                if (pAlienGroup.y < minAlienHeight && ShipMan.GetShip().GetStateName() != ShipState.Name.End)
                {
                    pAlienGroup.Regenerate(86f, alienHeightStart, 50f, 50f, columns);
                    ShipMan.GetShip().OnDeath();
                }

                if (nextSceneCache != SceneContext.Scene.None)
                {
                    if (nextSceneCache == SceneContext.Scene.Over)
                    {
                        UpdateHighScore();
                    }

                    SceneContext.Scene tmp = nextSceneCache;
                    nextSceneCache = SceneContext.Scene.None;
                    SpaceInvaders.ChangeScene(tmp);
                }
            }
        }

        private void UpdateHighScore()
        {
            if (PlayerMan.GetPlayer1().GetScore() > SpaceInvaders.HIGH_SCORE)
            {
                SpaceInvaders.HIGH_SCORE = PlayerMan.GetPlayer1().GetScore();
                FontMan.Find(Font.Name.HighScore).UpdateMessage(SpaceInvaders.HIGH_SCORE.ToString());
            }

            if (PlayerMan.GetPlayer2().GetScore() > SpaceInvaders.HIGH_SCORE)
            {
                SpaceInvaders.HIGH_SCORE = PlayerMan.GetPlayer2().GetScore();
                FontMan.Find(Font.Name.HighScore).UpdateMessage(SpaceInvaders.HIGH_SCORE.ToString());
            }
        }

        public override void Draw()
        {
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            GrabManagers();

            FontMan.Find(Font.Name.HighScore).UpdateMessage(SpaceInvaders.HIGH_SCORE.ToString());

            if (name == SceneContext.Scene.Play)
            {
                PlayerMan.SetActivePlayer(Player.Name.Player1);
                Debug.WriteLine("Started Player 1, Life " + PlayerMan.GetActivePlayer().GetLives());
            }
            else
            {
                PlayerMan.SetActivePlayer(Player.Name.Player2);
                Debug.WriteLine("Started Player 2, Life " + PlayerMan.GetActivePlayer().GetLives());
            }

            FontMan.Find(Font.Name.ScorePlayer1).UpdateMessage(PlayerMan.GetPlayer1().GetScore().ToString());
            FontMan.Find(Font.Name.ScorePlayer2).UpdateMessage(PlayerMan.GetPlayer2().GetScore().ToString());

            if (previousSceneCache == SceneContext.Scene.Select || (name == SceneContext.Scene.Play2 && PlayerMan.GetActivePlayer().GetLives() == Player.maxLives))
            {
                pShieldGroup.Regenerate();
                pAlienGroup.Regenerate(86f, alienHeightStart, 50f, 50f, columns);
                ShipMan.GetShip().Reset();
            }

            ShipMan.GetShip().Respawn();

            awaitingSceneChange = false;
        }

        private void HandleUfo()
        {
            int choice = rand.Next(3_500);

            if (choice == 23)
            {
                UfoRoot.SpawnUfo();
            }
        }

        private BombRoot GenBombs()
        {
            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            GameObjectNodeMan.Attach(pBombRoot);
            return pBombRoot;
        }

        private SplatterRoot GenSplatters()
        {
            SplatterRoot pSplatterRoot = new SplatterRoot(SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pSplatterRoot.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            GameObjectNodeMan.Attach(pSplatterRoot);
            return pSplatterRoot;
        }

        private UfoRoot GenUfos()
        {
            UfoRoot pRoot = new UfoRoot(GameObject.Name.UfoRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pRoot.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            GameObjectNodeMan.Attach(pRoot);
            return pRoot;
        }

        private void HandleBoxToggle()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_3))
            {
                if (hasBeenReleasedBoxes)
                {
                    pToggleBatch.SetVisibility(!isBatchToggledBoxes);
                    isBatchToggledBoxes = !isBatchToggledBoxes;
                    hasBeenReleasedBoxes = false;
                }
            }
            else
            {
                hasBeenReleasedBoxes = true;
            }
        }

        private void HandleShieldRegen()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_8))
            {
                if (hasBeenReleasedShields)
                {
                    pShieldGroup.Regenerate();
                    hasBeenReleasedShields = false;

                    UfoRoot.SpawnUfo();
                }
            }
            else
            {
                hasBeenReleasedShields = true;
            }
        }

        private void HandlePlayerRegen()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_9))
            {
                if (hasBeenReleasedPlayer)
                {
                    ShipMan.GetShip().Reset();
                    hasBeenReleasedPlayer = false;
                }
            }
            else
            {
                hasBeenReleasedPlayer = true;
            }
        }

        private void HandleAliensRegen()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_0))
            {
                if (hasBeenReleasedAliens)
                {
                    pAlienGroup.Regenerate(86f, alienHeightStart, 50f, 50f, columns);
                    hasBeenReleasedAliens = false;
                }
            }
            else
            {
                hasBeenReleasedAliens = true;
            }
        }

        private void HandleAliensDifficulty()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_O))
            {
                if (hasBeenReleasedDifficulty)
                {
                    pAlienGroup.IncreaseDifficulty();
                    hasBeenReleasedDifficulty = false;
                }
            }
            else
            {
                hasBeenReleasedDifficulty = true;
            }
        }

        private void SetupInput()
        {
            InputMan.OnArrowLeft().Subscribe(new MoveLeftObserver());
            InputMan.OnArrowRight().Subscribe(new MoveRightObserver());
            InputMan.OnSpace().Subscribe(new ShootObserver());
        }

        private void SetupCollisions(AlienGrid pAlienGroup, MissileGroup pMissileGroup, WallGroup pWallGroup, BombRoot pBombGroup, ShipRoot pShipGroup, UfoRoot pUfoGroup)
        {
            CollisionPair missileVsAlien = CollisionPairMan.Add(CollisionPair.Name.Alien_Missile, pMissileGroup, pAlienGroup);
            missileVsAlien.Subscribe(new ShipRemoveMissileObserver());
            missileVsAlien.Subscribe(new RemoveRightGameObjectObserver());
            missileVsAlien.Subscribe(new ShipReadyObserver());
            missileVsAlien.Subscribe(new AlienKilledObserver());
            missileVsAlien.Subscribe(new SoundObserver(SoundSystem.kill));

            CollisionPair alienVsWall = CollisionPairMan.Add(CollisionPair.Name.Alien_Wall, pAlienGroup, pWallGroup);
            alienVsWall.Subscribe(new AlienGridWallObserver());

            CollisionPair missileVsWall = CollisionPairMan.Add(CollisionPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
            missileVsWall.Subscribe(new ShipRemoveMissileObserver());
            missileVsWall.Subscribe(new ShipReadyObserver());

            CollisionPair bombVsWall = CollisionPairMan.Add(CollisionPair.Name.Bomb_Wall, pBombGroup, pWallGroup);
            bombVsWall.Subscribe(new BombObserver());

            CollisionPair missileVsShield = CollisionPairMan.Add(CollisionPair.Name.Misslie_Shield, pMissileGroup, pShieldGroup);
            missileVsShield.Subscribe(new ShipRemoveMissileObserver());
            missileVsShield.Subscribe(new RemoveRightGameObjectObserver());
            missileVsShield.Subscribe(new ShipReadyObserver());
            missileVsShield.Subscribe(new SoundObserver(SoundSystem.kill));

            CollisionPair bombVsShield = CollisionPairMan.Add(CollisionPair.Name.Bomb_Shield, pBombGroup, pShieldGroup);
            bombVsShield.Subscribe(new RemoveRightGameObjectObserver());
            bombVsShield.Subscribe(new RemoveLeftGameObjectObserver());
            bombVsShield.Subscribe(new SoundObserver(SoundSystem.kill));

            CollisionPair bombVsMissile = CollisionPairMan.Add(CollisionPair.Name.Bomb_Missile, pMissileGroup, pBombGroup);
            bombVsMissile.Subscribe(new ShipRemoveMissileObserver());
            bombVsMissile.Subscribe(new RemoveRightGameObjectObserver());
            bombVsMissile.Subscribe(new ShipReadyObserver());
            bombVsMissile.Subscribe(new SoundObserver(SoundSystem.kill));

            CollisionPair bombVsPlayer = CollisionPairMan.Add(CollisionPair.Name.Bomb_Player, pBombGroup, pShipGroup);
            bombVsPlayer.Subscribe(new RemoveLeftGameObjectObserver());
            bombVsPlayer.Subscribe(new SoundObserver(SoundSystem.explosion));
            bombVsPlayer.Subscribe(new ShipDiedObserver());

            CollisionPair missileVsUfo = CollisionPairMan.Add(CollisionPair.Name.Missile_Ufo, pMissileGroup, pUfoGroup);
            missileVsUfo.Subscribe(new ShipRemoveMissileObserver());
            missileVsUfo.Subscribe(new RemoveRightGameObjectObserver());
            missileVsUfo.Subscribe(new AlienKilledObserver());
            missileVsUfo.Subscribe(new ShipReadyObserver());
            missileVsUfo.Subscribe(new SoundObserver(SoundSystem.kill));

            CollisionPair ufoVsWall = CollisionPairMan.Add(CollisionPair.Name.Ufo_Wall, pUfoGroup, pWallGroup);
            ufoVsWall.Subscribe(new UfoWallObserver());
        }

        private ShieldRoot pShieldGroup;
        private AlienGrid pAlienGroup;
        private Random rand = new Random();

        private SpriteBatch pToggleBatch;
        private bool isBatchToggledBoxes = false;
        private bool hasBeenReleasedBoxes = true;
        private bool hasBeenReleasedShields = true;
        private bool hasBeenReleasedPlayer = true;
        private bool hasBeenReleasedAliens = true;
        private bool hasBeenReleasedDifficulty = true;
        private bool awaitingSceneChange = false;

        private int level = 0;
        private int columns = 11;
        private float minAlienHeight = 225f;

        private float alienHeightStart = 400f;
    }
}