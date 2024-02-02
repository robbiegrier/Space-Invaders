using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SplatterRemoveCommand : Command
    {
        public SplatterRemoveCommand(GameObject pInSubject)
        {
            pSubject = pInSubject;
        }

        public override void Execute(float deltaTime)
        {
            pSubject.Destroy();
        }

        public static void Launch(Splatter pInSubject)
        {
            TimerEventMan.Add(TimerEvent.Name.SplatterRemove, new SplatterRemoveCommand(pInSubject), pInSubject.screenTime);
        }

        public static void Launch(GameObject pInSubject, float time)
        {
            TimerEventMan.Add(TimerEvent.Name.SplatterRemove, new SplatterRemoveCommand(pInSubject), time);
        }

        private GameObject pSubject;
    }
}