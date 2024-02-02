using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Simulation
    {
        public enum State
        {
            Realtime,
            FixedStep,
            SingleStep,
            Pause
        };

        // singleton access
        private static Simulation privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void Create()
        {
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new Simulation();
            }
        }

        public static void Update(float systemTime)
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            pSim.privProcessInput();

            pSim.stopWatch_toc = systemTime - pSim.stopWatch_tic;
            pSim.stopWatch_tic = systemTime;

            if (pSim.privGetState() == State.FixedStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
            }
            else if (pSim.privGetState() == State.Realtime)
            {
                pSim.timeStep = pSim.stopWatch_toc;
            }
            else if (pSim.privGetState() == State.SingleStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
                pSim.privSetState(State.Pause);
            }
            else
            {
                pSim.timeStep = 0.0f;
            }

            pSim.totalWatch += pSim.timeStep;
        }

        private void privProcessInput()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_G))
            {
                privSetState(State.FixedStep);
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_H))
            {
                privSetState(State.Realtime);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S) && !oldKey)
            {
                privSetState(State.SingleStep);
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_D))
            {
                privSetState(State.SingleStep);
            }

            oldKey = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S);
        }

        private void privSetState(State simState)
        {
            state = simState;
        }

        private State privGetState()
        {
            return state;
        }

        public static void SetState(State simState)
        {
            privGetInstance().privSetState(simState);
        }

        public static State GetState()
        {
            return privGetInstance().privGetState();
        }

        public static float GetTimeStep()
        {
            return privGetInstance().timeStep;
        }

        public static float GetTotalTime()
        {
            return privGetInstance().totalWatch;
        }

        private Simulation()
        {
            state = State.Realtime;
            timeStep = 0.0f;
            totalWatch = 0.0f;
            stopWatch_tic = 0.0f;
            stopWatch_toc = 0.0f;
        }

        private static Simulation pInstance;

        private State state;

        private float stopWatch_tic;
        private float stopWatch_toc;
        private float totalWatch;
        private float timeStep;

        private const int SIM_NUM_WAKE_CYCLES = 0;
        private const float SIM_SINGLE_TIME_STEP = 0.016666f;

        private static bool oldKey = false;
    }
}