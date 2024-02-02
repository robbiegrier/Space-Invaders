using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SceneSelect : SceneState
    {
        public SceneSelect()
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            GrabManagers();

            SpriteBatchMan.Add(SpriteBatch.Name.Texts, 999_999_999);
        }

        private void LoadOnEntry()
        {
            FontMan.RemoveAll();

            float topTextMargin = 20f;
            Font pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.SpaceInvaders, 20, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            pFont = FontMan.Add(Font.Name.ScorePlayer1, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, 30, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.SpaceInvaders, SpaceInvaders.MIDWIDTH - 80, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            pFont = FontMan.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, SpaceInvaders.MIDWIDTH - 35, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pFont.SetColor(0.90f, 0.90f, 0.90f);
            pFont.UpdateMessage(SpaceInvaders.HIGH_SCORE.ToString());

            pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.SpaceInvaders, SpaceInvaders.WIDTH - 200, SpaceInvaders.HEIGHT - topTextMargin);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            pFont = FontMan.Add(Font.Name.ScorePlayer2, SpriteBatch.Name.Texts, "0000", Glyph.Name.SpaceInvaders, SpaceInvaders.WIDTH - 180, SpaceInvaders.HEIGHT - topTextMargin * 3);
            pFont.SetColor(0.90f, 0.90f, 0.90f);

            TimedCharacterFactory.Install("PLAY", 2.0f, 0.15f, SpaceInvaders.MIDWIDTH - 50, SpaceInvaders.HEIGHT - 200, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("SPACE INVADERS", 4.0f, 0.075f, SpaceInvaders.MIDWIDTH - 150, 500, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("1 OR 2 PLAYERS BUTTON", 4.5f, 0.05f, SpaceInvaders.MIDWIDTH - 220, 450, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("*SCORE ADVANCE TABLE*", 5.0f, 0.0f, SpaceInvaders.MIDWIDTH - 200, 350, 0.9f, 0.9f, 0.9f);

            float legendOffset = SpaceInvaders.MIDWIDTH - 160;
            TimedCharacterFactory.Install("d", 5.0f, 0f, legendOffset, 300, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("c", 5.1f, 0f, legendOffset + 10, 250, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("b", 5.2f, 0f, legendOffset + 5, 200, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("a", 5.3f, 0f, legendOffset + 5, 150, 0.2f, 0.8f, 0.2f);

            legendOffset += 60;
            TimedCharacterFactory.Install("= ? MYSTERY", 5.0f, 0.10f, legendOffset, 300, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 30 POINTS", 7.0f, 0.10f, legendOffset, 250, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 20 POINTS", 9.0f, 0.10f, legendOffset, 200, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 10 POINTS", 11.0f, 0.10f, legendOffset, 150, 0.2f, 0.8f, 0.2f);
        }

        public override void Update(float systemTime)
        {
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

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true && is1Pressed == false)
            {
                is1Pressed = true;
                PlayerMan.SetGameMode(PlayerMan.Mode.SinglePlayer);
                SpaceInvaders.ChangeScene(SceneContext.Scene.Play);
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true && is2Pressed == false)
            {
                is2Pressed = true;
                PlayerMan.SetGameMode(PlayerMan.Mode.MultiPlayer);
                SpaceInvaders.ChangeScene(SceneContext.Scene.Play);
            }
        }

        public override void Draw()
        {
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            GrabManagers();

            PlayerMan.GetPlayer1().SetScore(0);
            PlayerMan.GetPlayer2().SetScore(0);

            PlayerMan.GetPlayer1().SetLives(Player.maxLives);
            PlayerMan.GetPlayer2().SetLives(Player.maxLives);

            LoadOnEntry();

            is1Pressed = false;
            is2Pressed = false;
        }

        private bool is1Pressed = false;
        private bool is2Pressed = false;
    }
}