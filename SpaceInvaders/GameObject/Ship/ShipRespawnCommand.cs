using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipRespawnCommand : Command
    {
        public ShipRespawnCommand(Ship pInShip)
        {
            pShip = pInShip;
        }

        public override void Execute(float deltaTime)
        {
            if (PlayerMan.GetGameMode() == PlayerMan.Mode.SinglePlayer)
            {
                pShip.Respawn();
            }
            else
            {
                if (PlayerMan.GetActivePlayer().name == Player.Name.Player1)
                {
                    //SpaceInvaders.ChangeScene(SceneContext.Scene.Play2);
                    SpaceInvaders.pSceneContext.GetState().nextSceneCache = SceneContext.Scene.Play2;
                }
                else
                {
                    if (!pShip.IsOutOfLives())
                    {
                        //SpaceInvaders.ChangeScene(SceneContext.Scene.Play);
                        SpaceInvaders.pSceneContext.GetState().nextSceneCache = SceneContext.Scene.Play;
                    }
                    else
                    {
                        //SpaceInvaders.ChangeScene(SceneContext.Scene.Over);
                        SpaceInvaders.pSceneContext.GetState().nextSceneCache = SceneContext.Scene.Over;
                    }
                }
            }
        }

        private Ship pShip;
    }
}