using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class GlobalTimer
    {
        private GlobalTimer()
        {
            mCurrTime = 0.0f;
        }

        public static void Update(float time)
        {
            GlobalTimer pTimer = GlobalTimer.privGetInstance();
            pTimer.mCurrTime = time;
        }

        public static float GetTime()
        {
            GlobalTimer pTimer = GlobalTimer.privGetInstance();
            return pTimer.mCurrTime;
        }

        private static GlobalTimer privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new GlobalTimer();
            }

            return pInstance;
        }

        private static GlobalTimer pInstance = null;
        protected float mCurrTime;
    }
}