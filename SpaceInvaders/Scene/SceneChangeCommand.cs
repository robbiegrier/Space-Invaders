using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SceneChangeCommand : Command
    {
        public SceneChangeCommand(SceneContext.Scene scene)
        {
            sceneName = scene;
        }

        public override void Execute(float deltaTime)
        {
            SpaceInvaders.ChangeScene(sceneName);
        }

        private SceneContext.Scene sceneName;
    }
}