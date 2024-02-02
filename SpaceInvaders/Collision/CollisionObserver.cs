using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class CollisionObserver : SLink
    {
        public enum Name
        {
            SoundObserver,
            GridObserver,
            ShipReadyObserver,
            ShipRemoveMissileObserver,
            RemoveLeftObserver,
            RemoveRightObserver,
            BombObserver,
            AlienKilledObserver,
            ShipDiedObserver,
            Uninitialized
        }

        public abstract void Notify();

        public virtual void Execute()
        {
        }

        public override void Wash()
        {
            Debug.Assert(false);
        }

        public CollisionSubject pSubject;
    }
}