using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class GameObject : Component
    {
        public enum Name
        {
            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,

            SquidAlien,
            CrabAlien,
            OctopusAlien,

            Ufo,
            UfoRoot,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            BirdColumn,
            BirdColumn_0,
            BirdColumn_1,
            BirdColumn_2,
            BirdGrid,

            ShieldRoot,
            ShieldColumn,
            ShieldBrick,
            ShieldGrid,

            AlienColumn,
            AlienGrid,

            Missile,
            MissileGroup,

            Ship,
            ShipRoot,

            Bomb,
            BombRoot,

            Splatter,
            SplatterRoot,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            NullObject,
            Uninitialized
        }

        protected GameObject(Component.Container type, GameObject.Name gameObjectName, SpriteGame.Name proxyName)
            : base(type)
        {
            name = gameObjectName;
            x = 0.0f;
            y = 0.0f;

            spriteName = proxyName;
            SpriteGameProxy pInProxy = SpriteGameProxyMan.Add(proxyName);
            Debug.Assert(pInProxy != null);

            pSpriteProxy = pInProxy;

            // LTN - game object holds onto the collision object
            pCollisionObject = new CollisionObject(pSpriteProxy);
            Debug.Assert(pCollisionObject != null);
        }

        protected GameObject(Component.Container type, GameObject.Name gameObjectName, SpriteGame.Name inSpriteName, float inX, float inY)
            : base(type)
        {
            name = gameObjectName;
            x = inX;
            y = inY;

            spriteName = inSpriteName;
            pSpriteProxy = SpriteGameProxyMan.Add(spriteName);

            // LTN - game object holds onto the collision object
            pCollisionObject = new CollisionObject(pSpriteProxy);
            Debug.Assert(pCollisionObject != null);
        }

        // Called when the object enters the scene, either after being constructed or recycled
        public virtual void BeginPlay()
        {
            PrintAction(spawnVerb);
        }

        ~GameObject()
        {
        }

        public void SetSprite(SpriteGame.Name inName)
        {
            spriteName = inName;
            pSpriteProxy.Set(spriteName);
        }

        public CollisionObject GetCollisionObject()
        {
            return pCollisionObject;
        }

        public void AddLocationOffset(float inXOffset, float inYOffset)
        {
            x += inXOffset;
            y += inYOffset;
            Update();
        }

        public void SetLocation(float inX, float inY)
        {
            x = inX;
            y = inY;
            Update();
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            GetCollisionObject().pColSprite.SetColor(red, green, blue);
        }

        public override void Resurrect()
        {
            diedThisFrame = false;

            //pSpriteProxy = SpriteGameProxyMan.Add(spriteName);

            //pCollisionObject = new CollisionObject(pSpriteProxy);
            //Debug.Assert(pCollisionObject != null);
            pCollisionObject.Resurrect(pSpriteProxy);
            spawnVerb = "Resurrected";

            base.Resurrect();
        }

        public virtual void Remove()
        {
            EndPlay();

            if (pSpriteProxy.GetSpriteNode().GetSprite() != null)
            {
                SpriteBatchMan.Remove(pSpriteProxy.GetSpriteNode());
                //SpriteGameProxyMan.Remove(pSpriteProxy);
            }

            SpriteBatchMan.Remove(GetCollisionObject().pColSprite.GetSpriteNode());
            //SpriteBoxMan.Remove(GetCollisionObject().pColSprite);

            GameObjectNodeMan.Remove(this);

            PrintAction("Ghosted");

            if (type != Component.Container.COMPOSITE)
            {
                GhostMan.Attach(this);
            }
        }

        public void Destroy()
        {
            if (!diedThisFrame)
            {
                diedThisFrame = true;

                GetCollisionObject().poColRect.Minimize();
                Update();

                DeathMan.Attach(new RemoveRightGameObjectObserver(this));
            }
        }

        public virtual void EndPlay()
        {
        }

        public bool DiedThisFrame()
        {
            return diedThisFrame;
        }

        public virtual void Update()
        {
            Debug.Assert(pSpriteProxy != null);
            Debug.Assert(pCollisionObject != null);
            Debug.Assert(pCollisionObject.pColSprite != null);

            pSpriteProxy.x = x;
            pSpriteProxy.y = y;

            pCollisionObject.UpdatePos(x, y);
            pCollisionObject.pColSprite.Update();
        }

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(pCollisionObject != null);
            pSpriteBatch.Attach(pCollisionObject.pColSprite);
        }

        public void ActivateSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(pSpriteProxy);
        }

        public override void Dump()
        {
            Debug.WriteLine("\t\t\t       name: {0} ({1})", name, GetHashCode());

            if (pSpriteProxy != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", pSpriteProxy.name);
                if (pSpriteProxy.pSprite == null)
                {
                    Debug.WriteLine("\t\t    pRealSprite: null");
                }
                else
                {
                    Debug.WriteLine("\t\t    pRealSprite: {0}", this.pSpriteProxy.pSprite.GetName());
                }
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }

            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", x, y);

            base.baseDump();
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public SpriteBase GetSprite()
        {
            return pSpriteProxy;
        }

        public int GetGameObjectTreeDepth()
        {
            int output = 0;

            Component pCurr = pParent;

            while (pCurr != null)
            {
                pCurr = pCurr.pParent;
                output++;
            }

            return output;
        }

        public void Print(string content)
        {
            if (SpaceInvaders.DEBUG_OBJECTS)
            {
                Debug.WriteLine(content);
            }
        }

        public void PrintWithDepth(string content)
        {
            string indent = "";

            for (int i = 0; i < GetGameObjectTreeDepth(); i++)
            {
                indent += '\t';
            }

            Print($"{indent}{content}");
        }

        public void PrintAction(string action)
        {
            string n = GetName().ToString().ToLower();
            string article = n.StartsWith("a") || n.StartsWith("e") || n.StartsWith("i") || n.StartsWith("o") || n.StartsWith("u") ? "an" : "a";
            PrintWithDepth($"{action} {article} {GetName()}");
        }

        public void SetName(GameObject.Name inName)
        {
            name = inName;
        }

        public GameObject.Name GetGameObjectName()
        {
            return name;
        }

        // Object name
        protected GameObject.Name name;

        // Transform x
        public float x;

        // Transform y
        public float y;

        // Sprite proxy instance
        public SpriteGameProxy pSpriteProxy;

        // Object collision
        public CollisionObject pCollisionObject;

        // Is marked for death
        private bool diedThisFrame = false;

        public SpriteGame.Name spriteName;

        private string spawnVerb = "Created";
    }
}