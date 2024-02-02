using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipMissileFlyingState : ShipState
    {
        public ShipMissileFlyingState()
        {
            name = Name.MissileFlying;
        }

        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.Ready);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }

        public override void ShootMissile(Ship pShip)
        {
            // do nothing, it is flying already
        }
    }
}