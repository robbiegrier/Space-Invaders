using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BombObserver : CollisionObserver
    {
        public override void Notify()
        {
            Bomb pBomb = (Bomb)pSubject.pObjA;
            pBomb.Destroy();
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.BombObserver;
        }
    }
}