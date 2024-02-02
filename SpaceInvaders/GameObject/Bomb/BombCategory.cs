using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class BombCategory : Leaf
    {
        public enum Type
        {
            Bomb,
            BombRoot,
            Unitialized
        }

        protected BombCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, BombCategory.Type inBombType)
            : base(name, spriteName, posX, posY)
        {
            bombType = inBombType;
        }

        protected BombCategory.Type bombType;
    }
}