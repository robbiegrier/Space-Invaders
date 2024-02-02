using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class GameObjectNull : Leaf
    {
        public GameObjectNull()
            : base(GameObject.Name.NullObject, SpriteGame.Name.NullObject, 0, 0)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitNullGameObject(this);
        }
    }
}