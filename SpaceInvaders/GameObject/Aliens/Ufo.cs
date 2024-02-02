using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Ufo : AlienBase
    {
        public Ufo(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Ufo, spriteName, posX, posY)
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitUfo(this);
        }

        public override void BeginPlay()
        {
            SoundSystem.Play(SoundSystem.ufoLow);

            Random rand = new Random();

            int choice = rand.Next(2);

            if (choice == 0)
            {
                sign = 1f;
                x = -startXOffset;
            }
            else
            {
                sign = -1f;
                x = SpaceInvaders.WIDTH + startXOffset;
            }

            y = SpaceInvaders.HEIGHT - startYOffset;
            base.Update();

            base.BeginPlay();
        }

        public override void Update()
        {
            x += 0.8f * sign;

            base.Update();
        }

        public bool IsMovingLeft()
        {
            return sign < 0f;
        }

        private float sign = 1f;
        private float startXOffset = 10;
        private float startYOffset = 100;
    }
}