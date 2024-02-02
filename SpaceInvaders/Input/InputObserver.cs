using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class InputObserver : SLink
    {
        public enum Name
        {
            MoveLeftObserver,
            MoveRightObserver,
            ShootObserver,
            SoundObserver,
            Uninitialized
        }

        public abstract void Notify();

        public override void Wash()
        {
            Debug.Assert(false);
        }

        public InputSubject pSubject;
    }
}