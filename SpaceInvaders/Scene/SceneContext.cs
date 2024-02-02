using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SceneContext
    {
        public enum Scene
        {
            Select,
            Play,
            Play2,
            Over,
            None
        }

        public SceneContext()
        {
            poSceneSelect = new SceneSelect();
            poSceneSelect.name = Scene.Select;
            PlayerMan.SetActivePlayer(Player.Name.Player1);
            poScenePlay = new ScenePlay();
            poScenePlay.name = Scene.Play;
            PlayerMan.SetActivePlayer(Player.Name.Player2);
            poScenePlay2 = new ScenePlay();
            poScenePlay2.name = Scene.Play2;
            PlayerMan.SetActivePlayer(Player.Name.Player1);
            poSceneOver = new SceneOver();
            poSceneOver.name = Scene.Over;

            pSceneState = poSceneSelect;
            pSceneState.Entering();
        }

        public SceneState GetState()
        {
            return pSceneState;
        }

        public void SetState(Scene eScene)
        {
            SceneContext.Scene tmp = pSceneState.name;
            pSceneState.Leaving();

            switch (eScene)
            {
                case Scene.Select:
                    //poSceneSelect = new SceneSelect();
                    pSceneState = poSceneSelect;
                    break;

                case Scene.Play:
                    pSceneState = poScenePlay;
                    break;

                case Scene.Play2:
                    pSceneState = poScenePlay2;
                    break;

                case Scene.Over:
                    pSceneState = poSceneOver;
                    break;
            }

            pSceneState.previousSceneCache = tmp;
            pSceneState.Entering();
        }

        private SceneState pSceneState;
        private SceneSelect poSceneSelect;
        private SceneOver poSceneOver;
        private ScenePlay poScenePlay;
        private ScenePlay poScenePlay2;
    }
}