using System;

namespace SpaceInvaders
{
    internal abstract class BirdBase : Leaf
    {
        public enum Type
        {
            Red,
            Yellow,
            Green,
            White
        }

        protected BirdBase(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName, spriteName, _x, _y)
        {
            pCollisionObject.pColSprite.SetColor(1.0f, 1.0f, 0.0f);
        }
    }
}