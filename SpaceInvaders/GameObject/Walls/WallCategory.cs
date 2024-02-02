using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class WallCategory : Leaf
    {
        public enum Type
        {
            WallGroup,
            Right,
            Left,
            Bottom,
            Top,

            Unitialized
        }

        protected WallCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y, WallCategory.Type inType)
        : base(gameName, spriteName, _x, _y)
        {
            wallType = inType;
        }

        public Type GetWallType()
        {
            return wallType;
        }

        protected WallCategory.Type wallType;
    }
}