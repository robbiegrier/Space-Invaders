using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipRemoveMissileObserver : CollisionObserver
    {
        public ShipRemoveMissileObserver()
        {
            pMissile = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            pMissile = m.pMissile;
        }

        public override void Notify()
        {
            pMissile = (Missile)pSubject.pObjA;
            pMissile.SetLocation(ShipMan.GetShip().x, ShipMan.GetShip().y + 20);
            pMissile.Destroy();
        }

        public override void Execute()
        {
            pMissile.Remove();
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.ShipRemoveMissileObserver;
        }

        // data
        private GameObject pMissile;
    }
}