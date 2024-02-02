using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class ShipCategory : Leaf
    {
        public enum Type
        {
            Ship,
            ShipRoot,
            Unitialized
        }

        protected ShipCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, ShipCategory.Type inShipType)
            : base(name, spriteName, posX, posY)
        {
            shipType = inShipType;
        }

        protected Type shipType;
    }
}