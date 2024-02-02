using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class MoveRightObserver : InputObserver
    {
        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override Enum GetName()
        {
            return Name.MoveRightObserver;
        }

        public override void Notify()
        {
            ShipMan.GetShip().MoveRight();
        }
    }
}