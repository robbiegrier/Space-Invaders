using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class ShipMan
    {
        public enum State
        {
            Ready,
            MissileFlying,
            Dead
        }

        public ShipMan()
        {
            pStateReady = new ShipReadyState();
            pStateMissileFlying = new ShipMissileFlyingState();
            pStateDead = new ShipEndState();
            pShip = null;
            pMissile = null;
        }

        public static void Create()
        {
            pInstance.pShip = ActivateShip();
            pInstance.pShip.SetState(ShipMan.State.Ready);
        }

        public static void Destroy()
        {
        }

        public static Ship GetShip()
        {
            return privGetInstance().privGetShip();
        }

        public static ShipRoot GetShipRoot()
        {
            return privGetInstance().pShipRoot;
        }

        private Ship privGetShip()
        {
            Debug.Assert(pShip != null);
            return pShip;
        }

        public static ShipState GetState(State state)
        {
            return privGetInstance().privGetState(state);
        }

        private ShipState privGetState(State state)
        {
            switch (state)
            {
                case State.Ready:
                    return pStateReady;

                case State.MissileFlying:
                    return pStateMissileFlying;

                case State.Dead:
                    return pStateDead;

                default:
                    Debug.Assert(false);
                    break;
            }

            return null;
        }

        public static Missile GetMissile()
        {
            return privGetInstance().privGetMissile();
        }

        private Missile privGetMissile()
        {
            return pMissile;
        }

        public static Missile ActivateMissile()
        {
            return privGetInstance().privActivateMissile();
        }

        private Missile privActivateMissile()
        {
            //pMissile = new Missile(SpriteGame.Name.PlayerShot, 400, 100);

            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Missile);
            if (pGameObjNode == null)
            {
                pMissile = new Missile(SpriteGame.Name.PlayerShot, 400, 100);
            }
            else
            {
                pMissile = (Missile)pGameObjNode.pGameObject;
                GhostMan.Remove(pGameObjNode);

                pMissile.Resurrect(400, 100);
            }

            pMissile.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            pMissile.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Missiles));

            GameObject pMissileGroup = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);
            pMissileGroup.Add(pMissile);

            pMissile.BeginPlay();

            return pMissile;
        }

        private static Ship ActivateShip()
        {
            return privGetInstance().privActivateShip();
        }

        private Ship privActivateShip()
        {
            pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, SpriteGame.Name.NullObject, 200, 100);
            Debug.Assert(pShipRoot != null);
            GameObjectNodeMan.Attach(pShipRoot);

            pShip = new Ship(GameObject.Name.Ship, SpriteGame.Name.Player, 200, 100);
            pShip.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Players));

            pShipRoot.Add(pShip);

            return pShip;
        }

        private static ShipMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void SetInstance(ShipMan pInInstance)
        {
            pInstance = pInInstance;
        }

        private static ShipMan pInstance = null;
        private Ship pShip;
        private ShipRoot pShipRoot;
        private Missile pMissile;
        private ShipReadyState pStateReady;
        private ShipMissileFlyingState pStateMissileFlying;
        private readonly ShipEndState pStateDead;
    }
}