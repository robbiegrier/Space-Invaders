using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class ShipState
    {
        public enum Name
        {
            Ready,
            MissileFlying,
            End,
            Uninitialized
        }

        public abstract void Handle(Ship pShip);

        public abstract void MoveRight(Ship pShip);

        public abstract void MoveLeft(Ship pShip);

        public abstract void ShootMissile(Ship pShip);

        public Name name = Name.Uninitialized;
    }
}