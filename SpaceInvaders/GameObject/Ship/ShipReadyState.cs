using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipReadyState : ShipState
    {
        public ShipReadyState()
        {
            name = Name.Ready;
        }

        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.MissileFlying);
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
            Missile pMissile = ShipMan.ActivateMissile();
            pMissile.SetLocation(pShip.x, pShip.y + 20f);
            Handle(pShip);

            SoundSystem.Play(SoundSystem.shoot);
        }
    }
}