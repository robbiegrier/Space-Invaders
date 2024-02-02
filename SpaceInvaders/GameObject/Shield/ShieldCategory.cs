using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class ShieldCategory : Leaf
    {
        public enum Type
        {
            Root,
            Column,
            Brick,
            Grid,

            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,

            Unitialized
        }

        protected ShieldCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, Type inShieldType)
            : base(name, spriteName, posX, posY)
        {
            shieldType = inShieldType;
        }

        public Type GetCategoryType()
        {
            return shieldType;
        }

        protected Type shieldType;
    }
}