using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SpriteGameProxy : SpriteBase
    {
        public enum Name
        {
            Proxy,
            Uninitialized,
            NullObject,
            Compare
        }

        public SpriteGameProxy()
            : base()
        {
            privClear();
        }

        protected SpriteGameProxy(SpriteGameProxy.Name _name)
            : base()
        {
            name = _name;
            privClear();
        }

        public void Set(SpriteGame.Name spriteName)
        {
            name = SpriteGameProxy.Name.Proxy;
            x = 0.0f;
            y = 0.0f;
            pSprite = SpriteGameMan.Find(spriteName);

            Debug.Assert(pSprite != null);
        }

        private void privPushToReal()
        {
            Debug.Assert(pSprite != null);

            pSprite.x = x;
            pSprite.y = y;
            pSprite.sx = sx;
            pSprite.sy = sy;
        }

        private void privClear()
        {
            name = SpriteGameProxy.Name.Uninitialized;
            x = 0.0f;
            y = 0.0f;
            sx = 1.0f;
            sy = 1.0f;
            pSprite = null;
        }

        public override void Render()
        {
            privPushToReal();
            pSprite.Update();
            pSprite.Render();
        }

        public override void Update()
        {
            privPushToReal();
            pSprite.Update();
        }

        public override bool Compare(NodeBase pNodeBaseB)
        {
            Debug.Assert(pNodeBaseB != null);
            SpriteGameProxy pNodeB = (SpriteGameProxy)pNodeBaseB;

            Debug.Assert(this.pSprite != null);
            Debug.Assert(pNodeB.pSprite != null);

            return this.pSprite.GetName().GetHashCode() == pNodeB.pSprite.GetName().GetHashCode();
        }

        public override void Wash()
        {
            baseClear();
            privClear();
        }

        public override void Dump()
        {
            Debug.WriteLine("   {0} ({1})", name, GetHashCode());

            if (pSprite != null)
            {
                Debug.WriteLine("       Sprite:{0} ({1})", pSprite.GetName(), pSprite.GetHashCode());
            }
            else
            {
                Debug.WriteLine("       Sprite: null");
            }

            Debug.WriteLine("        (x,y): {0},{1}", x, y);

            baseDump();
        }

        public override Enum GetName()
        {
            return name;
        }

        public Name name;
        public float x;
        public float y;
        public SpriteGame pSprite;
        public float sx;
        public float sy;
    }
}