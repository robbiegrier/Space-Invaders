using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipDiedObserver : CollisionObserver
    {
        public ShipDiedObserver()
        {
        }

        public override void Notify()
        {
            Ship pShip = (Ship)pSubject.pObjB;

            if (pShip.GetStateName() != ShipState.Name.End)
            {
                pShip.OnDeath();
            }
        }

        public override void Execute()
        {
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.RemoveLeftObserver;
        }
    }
}