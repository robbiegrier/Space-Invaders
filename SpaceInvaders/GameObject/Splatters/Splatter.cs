using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Splatter : Leaf
    {
        public Splatter(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Splatter, spriteName, posX, posY)
        {
        }

        public override void BeginPlay()
        {
            base.BeginPlay();

            SplatterRemoveCommand.Launch(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            // no collision
        }

        public readonly float screenTime = 0.2f;
    }
}