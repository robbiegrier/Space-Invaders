using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class SceneState
    {
        public SceneState()
        {
            timeAtPause = TimerEventMan.GetCurrTime();
            pSpriteBatchMan = new SpriteBatchMan(3, 1);
            pFontMan = new FontMan(3, 1);
            pTimerEventMan = new TimerEventMan(3, 1);
            pGameObjectNodeMan = new GameObjectNodeMan(3, 1);
            pCollisionPairMan = new CollisionPairMan(3, 1);
            pInputMan = new InputMan();
            pTimedCharacterFactory = new TimedCharacterFactory();
            pShipMan = new ShipMan();
        }

        protected void GrabManagers()
        {
            SpriteBatchMan.SetInstance(pSpriteBatchMan);
            FontMan.SetInstance(pFontMan);
            TimerEventMan.SetInstance(pTimerEventMan);
            GameObjectNodeMan.SetInstance(pGameObjectNodeMan);
            CollisionPairMan.SetInstance(pCollisionPairMan);
            InputMan.SetInstance(pInputMan);
            TimedCharacterFactory.SetInstance(pTimedCharacterFactory);
            ShipMan.SetInstance(pShipMan);

            float t0 = GlobalTimer.GetTime();
            float t1 = timeAtPause;
            float delta = t0 - t1;
            TimerEventMan.PauseUpdate(delta);
            TimedCharacterFactory.deltaThisSession = delta;
        }

        public abstract void Initialize();

        public abstract void Update(float systemTime);

        public abstract void Draw();

        public abstract void Entering();

        public virtual void Leaving()
        {
            timeAtPause = TimerEventMan.GetCurrTime();
        }

        public float timeAtPause = GlobalTimer.GetTime();
        public SpriteBatchMan pSpriteBatchMan;
        public FontMan pFontMan;
        public TimerEventMan pTimerEventMan;
        public GameObjectNodeMan pGameObjectNodeMan;
        public CollisionPairMan pCollisionPairMan;
        public InputMan pInputMan;
        public TimedCharacterFactory pTimedCharacterFactory;
        public ShipMan pShipMan;

        public SceneContext.Scene nextSceneCache = SceneContext.Scene.None;
        public SceneContext.Scene previousSceneCache = SceneContext.Scene.None;
        public SceneContext.Scene name = SceneContext.Scene.None;
    }
}