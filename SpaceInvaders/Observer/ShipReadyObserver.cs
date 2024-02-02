using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipReadyObserver : CollisionObserver
    {
        public override void Notify()
        {
            ShipMan.GetShip().Handle();
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.ShipReadyObserver;
        }
    }
}