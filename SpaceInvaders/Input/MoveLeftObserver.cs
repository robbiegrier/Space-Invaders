using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class MoveLeftObserver : InputObserver
    {
        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override Enum GetName()
        {
            return Name.MoveLeftObserver;
        }

        public override void Notify()
        {
            ShipMan.GetShip().MoveLeft();
        }
    }
}