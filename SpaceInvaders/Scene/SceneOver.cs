using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SceneOver : SceneState
    {
        public SceneOver()
        {
            Initialize();
        }

        public override void Initialize()
        {
            GrabManagers();
            SpriteBatchMan.Add(SpriteBatch.Name.Texts, 999_999_999);
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

            if (!awaitingSceneChange)
            {
                awaitingSceneChange = true;
                SceneChangeCommand pSceneChangeCmd = new SceneChangeCommand(SceneContext.Scene.Select);
                TimerEventMan.Add(TimerEvent.Name.SceneChange, pSceneChangeCmd, 3.5f);
            }
        }

        public override void Draw()
        {
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            GrabManagers();

            FontMan.RemoveAll();

            TimedCharacterFactory.Install("GAME OVER", 0.1f, 0.2f, SpaceInvaders.MIDWIDTH - 100, SpaceInvaders.HEIGHT - 200, 0.9f, 0.0f, 0.0f);

            awaitingSceneChange = false;
        }

        private bool awaitingSceneChange = false;
    }
}