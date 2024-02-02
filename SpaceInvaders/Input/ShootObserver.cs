using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShootObserver : InputObserver
    {
        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override Enum GetName()
        {
            return Name.ShootObserver;
        }

        public override void Notify()
        {
            ShipMan.GetShip().Shoot();
        }
    }
}