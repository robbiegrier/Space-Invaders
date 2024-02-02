using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class MissileCategory : Leaf
    {
        public enum Type
        {
            Missile,
            MissileGroup,
            Unitialized
        }

        protected MissileCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
        : base(gameName, spriteName, _x, _y)
        {
        }
    }
}