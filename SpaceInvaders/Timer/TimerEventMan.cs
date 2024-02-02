using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class TimerEventMan : ManBase
    {
        public TimerEventMan(int reserveNum, int reserveGrow)
            // LTN - Manager Base will hold the new active and reserve lists
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // LTN - store the less than cmd
            psTimeLessThan = new TimeLessThan();

            // LTN - store the compare object
            poNodeCompare = new TimerEvent();

            mCurrTime = 0.0f;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                // LTN - Singleton Instance
                pInstance = new TimerEventMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy(bool bPrintEnable = false)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            if (bPrintEnable)
            {
                TimerEventMan.DumpStats();
            }
        }

        public static TimerEvent Add(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            return privGetInstance().privAdd(timeName, pCommand, deltaTimeToTrigger);
        }

        public static TimerEvent Find(TimerEvent.Name name)
        {
            return privGetInstance().privFind(name);
        }

        public static void Remove(TimerEvent pEvent)
        {
            privGetInstance().privRemove(pEvent);
        }

        public static void Dump()
        {
            privGetInstance().privDump();
        }

        public static void DumpStats()
        {
            privGetInstance().privDumpStats();
        }

        public static void Update(float totalTime)
        {
            privGetInstance().privUpdate(totalTime);
        }

        public static float GetCurrTime()
        {
            return privGetInstance().privGetCurrTime();
        }

        private TimerEvent privAdd(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            float trueTime = GetCurrTime() + deltaTimeToTrigger;
            TimerEvent pTimerEvent = (TimerEvent)baseInsert(psTimeLessThan.With(trueTime));
            Debug.Assert(pTimerEvent != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pTimerEvent.Set(timeName, pCommand, deltaTimeToTrigger);

            return pTimerEvent;
        }

        private TimerEvent privFind(TimerEvent.Name name)
        {
            poNodeCompare.name = name;
            TimerEvent pData = (TimerEvent)baseFind(poNodeCompare);
            return pData;
        }

        private void privRemove(TimerEvent pEvent)
        {
            Debug.Assert(pEvent != null);
            baseRemove(pEvent);
        }

        private void privDump()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");
            baseDump();
        }

        private void privDumpStats()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");
            baseDumpStats();
            Debug.WriteLine("   ------------\n");
        }

        public static void PauseUpdate(float delta)
        {
            privGetInstance().privPauseUpdate(delta);
        }

        private void privPauseUpdate(float delta)
        {
            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            TimerEvent pEvent = (TimerEvent)pIt.First();

            while (!pIt.IsDone())
            {
                pEvent.triggerTime += delta;
                pEvent = (TimerEvent)pIt.Next();
            }
        }

        private void privUpdate(float totalTime)
        {
            mCurrTime = totalTime;

            Iterator pIt = baseGetIterator();
            Debug.Assert(pIt != null);

            TimerEvent pNode = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent
            // ToDo Fix: List needs to be sorted then its an early out

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pNode = (TimerEvent)pIt.Current();

                if (mCurrTime >= pNode.triggerTime)
                {
                    // call it and remove from list
                    pNode.Process();
                    pIt.Erase(this);
                }
                else
                {
                    // Early out, no more of the timers are ready yet
                    return;
                }
            }
        }

        private float privGetCurrTime()
        {
            return mCurrTime;
        }

        private static TimerEventMan privGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override NodeBase derivedCreateNode()
        {
            // LTN - Man base will own this
            NodeBase pNodeBase = new TimerEvent();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        public static void SetInstance(TimerEventMan pInInstance)
        {
            pInstance = pInInstance;
        }

        private readonly TimerEvent poNodeCompare;
        private static TimerEventMan pInstance = null;
        protected float mCurrTime;
        private static BinaryComparator psTimeLessThan;
    }

    internal class TimeLessThan : BinaryComparator
    {
        public override bool Compare(object pLeft, object pRight)
        {
            float triggerTimeLeft = (float)pLeft;
            TimerEvent pTimerEventRight = (TimerEvent)pRight;

            return triggerTimeLeft < pTimerEventRight.triggerTime;
        }
    }
}