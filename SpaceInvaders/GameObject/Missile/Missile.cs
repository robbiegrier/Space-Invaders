using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Missile : MissileCategory
    {
        public Missile(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Missile, spriteName, posX, posY)
        {
            x = posX;
            y = posY;
        }

        public override void Update()
        {
            base.Update();
            y += speed;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissile(this);
        }

        public void Resurrect(float posX, float posY)
        {
            x = posX;
            y = posY;
            speed = 5.0f;

            base.Resurrect();

            GetCollisionObject().pColSprite.SetColor(1, 1, 0);
        }

        public float speed = 5f;
    }
}