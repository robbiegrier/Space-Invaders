using System;

namespace SpaceInvaders
{
    internal class SpriteGameProxyNull : SpriteGameProxy
    {
        public SpriteGameProxyNull()
            : base(SpriteGameProxy.Name.NullObject)
        {
        }

        public override void Render()
        {
            // do nothing
        }

        public override void Update()
        {
            // do nothing
        }
    }
}