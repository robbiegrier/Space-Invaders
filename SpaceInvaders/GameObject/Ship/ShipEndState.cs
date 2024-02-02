using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipEndState : ShipState
    {
        public ShipEndState()
        {
            name = Name.End;
        }

        public override void Handle(Ship pShip)
        {
            pShip.SetSprite(SpriteGame.Name.Player);
            pShip.SetState(ShipMan.State.Ready);
        }

        public override void MoveRight(Ship pShip)
        {
        }

        public override void MoveLeft(Ship pShip)
        {
        }

        public override void ShootMissile(Ship pShip)
        {
        }
    }
}