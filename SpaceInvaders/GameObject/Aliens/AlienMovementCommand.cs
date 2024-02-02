using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienMovementCommand : Command
    {
        public AlienMovementCommand()
            : base()
        {
        }

        public override void Execute(float deltaTime)
        {
            AlienGrid pGrid = (AlienGrid)GameObjectNodeMan.Find(GameObject.Name.AlienGrid);
            pGrid.MoveAliens();

            TimerEventMan.Add(TimerEvent.Name.AlienMovement, this, pGrid.GetMovementDelay());

            if (soundClock == 0)
            {
                SoundSystem.Play(SoundSystem.aliensSpeed1);
            }
            else if (soundClock == 1)
            {
                SoundSystem.Play(SoundSystem.aliensSpeed2);
            }
            else if (soundClock == 2)
            {
                SoundSystem.Play(SoundSystem.aliensSpeed3);
            }
            else
            {
                SoundSystem.Play(SoundSystem.aliensSpeed4);
            }

            soundClock = (soundClock + 1) % 4;
        }

        private int soundClock = 0;
    }
}