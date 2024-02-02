//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpaceInvaders : Azul.Game
    {
        public static int WIDTH = 672;
        public static int HEIGHT = 768;
        public static int MIDWIDTH = WIDTH / 2;
        public static int MIDHEIGHT = HEIGHT / 2;

        public static int HIGH_SCORE = 0;

        public static SceneContext pSceneContext;

        public static void ChangeScene(SceneContext.Scene scene)
        {
            currentScreen = scene;
            pSceneContext.SetState(scene);
        }

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before
        //      starting to run.  This is where it can query for any required services
        //      and load any non-graphic related content.
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            SetWindowName("Space Invaders");
            SetWidthHeight(WIDTH, HEIGHT);
            SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            // Load Managers
            TextureMan.Create();
            ImageMan.Create();
            SpriteGameMan.Create();
            SpriteBatchMan.Create();
            SpriteBoxMan.Create();
            TimerEventMan.Create();
            SpriteGameProxyMan.Create();
            GameObjectNodeMan.Create();
            CollisionPairMan.Create();
            GlyphMan.Create();
            FontMan.Create();
            SoundSystem.Create();
            Simulation.Create();
            GhostMan.Create();
            PlayerMan.Create();

            // Load Assets
            LoadTextures();
            LoadImages();
            LoadSprites();
            LoadFonts();

            //ShipMan.Create();

            pSceneContext = new SceneContext();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public static SceneContext.Scene currentScreen = SceneContext.Scene.Select;

        public override void Update()
        {
            //if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_4) && currentScreen != SceneContext.Scene.Select)
            //{
            //    ChangeScene(SceneContext.Scene.Select);
            //}
            //if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_5) && currentScreen != SceneContext.Scene.Play)
            //{
            //    ChangeScene(SceneContext.Scene.Play);
            //}
            //if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_6) && currentScreen != SceneContext.Scene.Over)
            //{
            //    ChangeScene(SceneContext.Scene.Over);
            //}

            GlobalTimer.Update(GetTime());
            pSceneContext.GetState().Update(GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            pSceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            ImageMan.Destroy();
            TextureMan.Destroy();
            SpriteGameMan.Destroy();
            SpriteBatchMan.Destroy();
            SpriteBoxMan.Destroy();
            SpriteGameProxyMan.Destroy();
            TimerEventMan.Destroy();
            GameObjectNodeMan.Destroy();
            CollisionPairMan.Destroy();
            GlyphMan.Destroy();
            FontMan.Destroy();
            ShipMan.Destroy();
            SoundSystem.Destroy();
            GhostMan.Destroy();
            PlayerMan.Destroy();
        }

        private BirdGrid SetupBirds()
        {
            float startBottomLeftX = 156f;
            float startBottomLeftY = 300f;

            float horizontalStep = 85f;
            float verticalStep = 85f;

            BirdFactory pFactory = new BirdFactory(SpriteBatch.Name.AngryBirds);

            BirdGrid pGrid = (BirdGrid)pFactory.Create(GameObject.Name.BirdGrid);
            GameObjectNodeMan.Attach(pGrid);

            for (int i = 0; i < 4; i++)
            {
                int row = 0;
                BirdColumn pColumn = (BirdColumn)pFactory.Create(GameObject.Name.BirdColumn);
                pColumn.Add(pFactory.Create(GameObject.Name.YellowBird, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(pFactory.Create(GameObject.Name.WhiteBird, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(pFactory.Create(GameObject.Name.GreenBird, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pColumn.Add(pFactory.Create(GameObject.Name.RedBird, startBottomLeftX + (i * horizontalStep), startBottomLeftY + (row++ * verticalStep)));
                pGrid.Add(pColumn);
            }

            return pGrid;
        }

        private void LoadFonts()
        {
            GlyphMan.AddXml("Consolas20pt.xml", Glyph.Name.Consolas20pt, Texture.Name.Consolas20pt);
            GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.Consolas36pt, Texture.Name.Consolas36pt);
            GlyphMan.AddSpaceInvaders();
        }

        private void LoadSprites()
        {
            SpriteGameMan.Add(SpriteGame.Name.Player, Image.Name.Player, 0f, 0f, 40f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.PlayerShot, Image.Name.PlayerShot, 0f, 0f, 5f, 20f);

            float birdSize = 30f;
            float birdSizeInc = 10f;
            SpriteGameMan.Add(SpriteGame.Name.RedBird, Image.Name.RedBird, 0f, 0f, birdSize, birdSize);
            SpriteGameMan.Add(SpriteGame.Name.GreenBird, Image.Name.GreenBird, 0f, 0f, birdSize + birdSizeInc, birdSize + birdSizeInc);
            SpriteGameMan.Add(SpriteGame.Name.WhiteBird, Image.Name.WhiteBird, 0f, 0f, birdSize + (birdSizeInc * 2f), birdSize + (birdSizeInc * 2f));
            SpriteGameMan.Add(SpriteGame.Name.YellowBird, Image.Name.YellowBird, 0f, 0f, birdSize + (birdSizeInc * 3f), birdSize + (birdSizeInc * 3f));
            SpriteGameMan.Add(SpriteGame.Name.BlueBird, Image.Name.BlueBird, 50, 50, 50, 50);

            SpriteGameMan.Add(SpriteGame.Name.Squid, Image.Name.SquidA, 0f, 0f, 24f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.Crab, Image.Name.CrabA, 0f, 0f, 28f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.Octopus, Image.Name.OctopusA, 0f, 0f, 36f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.Saucer, Image.Name.Saucer, 0f, 0f, 42f, 28f);
            SpriteGameMan.Add(SpriteGame.Name.PlayerExplosionA, Image.Name.PlayerExplosionA, 0f, 0f, 40f, 25f);

            SpriteGameMan.Add(SpriteGame.Name.AlienExplosion, Image.Name.AlienExplosion, 0f, 0f, 35f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.SaucerExplosion, Image.Name.SaucerExplosion, 0f, 0f, 35f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.AlienShotExplosion, Image.Name.AlienShotExplosion, 0f, 0f, 25f, 25f);
            SpriteGameMan.Add(SpriteGame.Name.AlienPullYA, Image.Name.AlienShotExplosion, 0f, 0f, 15f, 15f);

            float shotSizeX = 10f;
            float shotSizeY = 25f;
            SpriteGameMan.Add(SpriteGame.Name.SquigglyShotA, Image.Name.SquigglyShotA, 0f, 0f, shotSizeX, shotSizeY);
            SpriteGameMan.Add(SpriteGame.Name.PlungerShotA, Image.Name.PlungerShotA, 0f, 0f, shotSizeX, shotSizeY);
            SpriteGameMan.Add(SpriteGame.Name.RollingShotA, Image.Name.RollingShotA, 0f, 0f, shotSizeX, shotSizeY);

            ShieldRoot.LoadBrickSprites();

            pToggleBatch = SpriteBatchMan.Add(SpriteBatch.Name.Boxes, 999_999_999);
            pToggleBatch.SetVisibility(false);
        }

        private void LoadTextures()
        {
            TextureMan.Add(Texture.Name.PeaShooter, "PeaShooter2.tga");
            TextureMan.Add(Texture.Name.Skeleton, "Skeleton2.tga");
            TextureMan.Add(Texture.Name.Runner, "Running2.tga");
            TextureMan.Add(Texture.Name.FlyingBird, "Flying2.tga");
            TextureMan.Add(Texture.Name.Birds, "Birds.tga");
            TextureMan.Add(Texture.Name.Shield, "Birds_N_Shield.tga");
            TextureMan.Add(Texture.Name.PacMan, "PacMan.tga");
            TextureMan.Add(Texture.Name.SpaceInvaders, "SpaceInvaders_ROM.tga");
            TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");

            TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");
            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
        }

        private void LoadImages()
        {
            ImageMan.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageMan.Add(Image.Name.OctopusB, Texture.Name.SpaceInvaders, 18, 3, 12, 8);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.CrabB, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageMan.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.SquidB, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageMan.Add(Image.Name.AlienExplosion, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageMan.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);
            ImageMan.Add(Image.Name.SaucerExplosion, Texture.Name.SpaceInvaders, 118, 3, 21, 8);

            ImageMan.Add(Image.Name.Player, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageMan.Add(Image.Name.PlayerExplosionA, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageMan.Add(Image.Name.PlayerExplosionB, Texture.Name.SpaceInvaders, 38, 14, 16, 8);
            ImageMan.Add(Image.Name.AlienPullYA, Texture.Name.SpaceInvaders, 57, 14, 15, 8);
            ImageMan.Add(Image.Name.AlienPullYB, Texture.Name.SpaceInvaders, 75, 14, 15, 8);
            ImageMan.Add(Image.Name.AlienPullUpisdeDownYA, Texture.Name.SpaceInvaders, 93, 14, 14, 8);
            ImageMan.Add(Image.Name.AlienPullUpsideDownYB, Texture.Name.SpaceInvaders, 110, 14, 14, 8);

            ImageMan.Add(Image.Name.PlayerShot, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageMan.Add(Image.Name.PlayerShotExplosion, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageMan.Add(Image.Name.SquigglyShotA, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotB, Texture.Name.SpaceInvaders, 24, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotC, Texture.Name.SpaceInvaders, 30, 26, 3, 7);
            ImageMan.Add(Image.Name.SquigglyShotD, Texture.Name.SpaceInvaders, 36, 26, 3, 7);
            ImageMan.Add(Image.Name.PlungerShotA, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotB, Texture.Name.SpaceInvaders, 48, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotC, Texture.Name.SpaceInvaders, 54, 27, 3, 6);
            ImageMan.Add(Image.Name.PlungerShotD, Texture.Name.SpaceInvaders, 60, 27, 3, 6);
            ImageMan.Add(Image.Name.RollingShotA, Texture.Name.SpaceInvaders, 65, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotB, Texture.Name.SpaceInvaders, 70, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotC, Texture.Name.SpaceInvaders, 75, 26, 3, 7);
            ImageMan.Add(Image.Name.RollingShotD, Texture.Name.SpaceInvaders, 80, 26, 3, 7);
            ImageMan.Add(Image.Name.AlienShotExplosion, Texture.Name.SpaceInvaders, 86, 25, 6, 8);

            ImageMan.Add(Image.Name.A, Texture.Name.SpaceInvaders, 3, 36, 5, 7);
            ImageMan.Add(Image.Name.B, Texture.Name.SpaceInvaders, 11, 36, 5, 7);
            ImageMan.Add(Image.Name.C, Texture.Name.SpaceInvaders, 19, 36, 5, 7);
            ImageMan.Add(Image.Name.D, Texture.Name.SpaceInvaders, 27, 36, 5, 7);
            ImageMan.Add(Image.Name.E, Texture.Name.SpaceInvaders, 35, 36, 5, 7);
            ImageMan.Add(Image.Name.F, Texture.Name.SpaceInvaders, 43, 36, 5, 7);
            ImageMan.Add(Image.Name.G, Texture.Name.SpaceInvaders, 51, 36, 5, 7);
            ImageMan.Add(Image.Name.H, Texture.Name.SpaceInvaders, 59, 36, 5, 7);
            ImageMan.Add(Image.Name.I, Texture.Name.SpaceInvaders, 67, 36, 5, 7);
            ImageMan.Add(Image.Name.J, Texture.Name.SpaceInvaders, 75, 36, 5, 7);
            ImageMan.Add(Image.Name.K, Texture.Name.SpaceInvaders, 83, 36, 5, 7);
            ImageMan.Add(Image.Name.L, Texture.Name.SpaceInvaders, 91, 36, 5, 7);
            ImageMan.Add(Image.Name.M, Texture.Name.SpaceInvaders, 99, 36, 5, 7);

            ImageMan.Add(Image.Name.N, Texture.Name.SpaceInvaders, 3, 46, 5, 7);
            ImageMan.Add(Image.Name.O, Texture.Name.SpaceInvaders, 11, 46, 5, 7);
            ImageMan.Add(Image.Name.P, Texture.Name.SpaceInvaders, 19, 46, 5, 7);
            ImageMan.Add(Image.Name.Q, Texture.Name.SpaceInvaders, 27, 46, 5, 7);
            ImageMan.Add(Image.Name.R, Texture.Name.SpaceInvaders, 35, 46, 5, 7);
            ImageMan.Add(Image.Name.S, Texture.Name.SpaceInvaders, 43, 46, 5, 7);
            ImageMan.Add(Image.Name.T, Texture.Name.SpaceInvaders, 51, 46, 5, 7);
            ImageMan.Add(Image.Name.U, Texture.Name.SpaceInvaders, 59, 46, 5, 7);
            ImageMan.Add(Image.Name.V, Texture.Name.SpaceInvaders, 67, 46, 5, 7);
            ImageMan.Add(Image.Name.W, Texture.Name.SpaceInvaders, 75, 46, 5, 7);
            ImageMan.Add(Image.Name.X, Texture.Name.SpaceInvaders, 83, 46, 5, 7);
            ImageMan.Add(Image.Name.Y, Texture.Name.SpaceInvaders, 91, 46, 5, 7);
            ImageMan.Add(Image.Name.Z, Texture.Name.SpaceInvaders, 99, 46, 5, 7);

            ImageMan.Add(Image.Name.Zero, Texture.Name.SpaceInvaders, 3, 56, 5, 7);
            ImageMan.Add(Image.Name.One, Texture.Name.SpaceInvaders, 11, 56, 5, 7);
            ImageMan.Add(Image.Name.Two, Texture.Name.SpaceInvaders, 19, 56, 5, 7);
            ImageMan.Add(Image.Name.Three, Texture.Name.SpaceInvaders, 27, 56, 5, 7);
            ImageMan.Add(Image.Name.Four, Texture.Name.SpaceInvaders, 35, 56, 5, 7);
            ImageMan.Add(Image.Name.Five, Texture.Name.SpaceInvaders, 43, 56, 5, 7);
            ImageMan.Add(Image.Name.Six, Texture.Name.SpaceInvaders, 51, 56, 5, 7);
            ImageMan.Add(Image.Name.Seven, Texture.Name.SpaceInvaders, 59, 56, 5, 7);
            ImageMan.Add(Image.Name.Eight, Texture.Name.SpaceInvaders, 67, 56, 5, 7);
            ImageMan.Add(Image.Name.Nine, Texture.Name.SpaceInvaders, 75, 56, 5, 7);
            ImageMan.Add(Image.Name.LessThan, Texture.Name.SpaceInvaders, 83, 56, 5, 7);
            ImageMan.Add(Image.Name.GreaterThan, Texture.Name.SpaceInvaders, 91, 56, 5, 7);
            ImageMan.Add(Image.Name.Space, Texture.Name.SpaceInvaders, 99, 56, 5, 7);
            ImageMan.Add(Image.Name.Equals, Texture.Name.SpaceInvaders, 107, 56, 5, 7);
            ImageMan.Add(Image.Name.Asterisk, Texture.Name.SpaceInvaders, 115, 56, 5, 7);
            ImageMan.Add(Image.Name.Question, Texture.Name.SpaceInvaders, 123, 56, 5, 7);
            ImageMan.Add(Image.Name.Hyphen, Texture.Name.SpaceInvaders, 131, 56, 5, 7);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Shield, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shield, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shield, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shield, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Shield, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Shield, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shield, 55, 215, 10, 5);

            ImageMan.Add(Image.Name.RedBird, Texture.Name.Birds, 47, 41, 48, 46);
            ImageMan.Add(Image.Name.YellowBird, Texture.Name.Birds, 124, 34, 60, 56);
            ImageMan.Add(Image.Name.GreenBird, Texture.Name.Birds, 246, 135, 99, 72);
            ImageMan.Add(Image.Name.WhiteBird, Texture.Name.Birds, 139, 131, 84, 97);
            ImageMan.Add(Image.Name.BlueBird, Texture.Name.Birds, 301, 49, 33, 33);
            ImageMan.Add(Image.Name.BlueGhost, Texture.Name.PacMan, 710, 148, 33, 33);
        }

        private SpriteBatch pToggleBatch;

        public static bool DEBUG_OBJECTS = false;
    }

    internal class BombSpawnEvent : Command
    {
        public BombSpawnEvent(Random pRandom)
        {
            this.pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.pSB_Birds = SpriteBatchMan.Find(SpriteBatch.Name.Bombs);
            Debug.Assert(this.pSB_Birds != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;
        }

        public override void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            // Create Bomb
            float value = pRandom.Next(20, 700);
            Bomb pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.SquigglyShotA, new FallStraight(), value, 700.0f, null);
            //     Debug.WriteLine("----x:{0}", value);

            pBomb.ActivateCollisionSprite(this.pSB_Boxes);
            pBomb.ActivateSprite(this.pSB_Birds);

            // Attach the missile to the Bomb root
            GameObject pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pBomb);
        }

        private GameObject pBombRoot;
        private SpriteBatch pSB_Birds;
        private SpriteBatch pSB_Boxes;
        private Random pRandom;
    }
}

// --- End of File ---