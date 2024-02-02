using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class InputMan
    {
        public InputMan()
        {
            pArrowLeftPressed = new InputSubject();
            pArrowRightPressed = new InputSubject();
            pSpacePressed = new InputSubject();
            privSpaceKeyPrev = false;
        }

        public static InputSubject OnArrowRight()
        {
            return privGetInstance().pArrowRightPressed;
        }

        public static InputSubject OnArrowLeft()
        {
            return privGetInstance().pArrowLeftPressed;
        }

        public static InputSubject OnSpace()
        {
            return privGetInstance().pSpacePressed;
        }

        public static void Update()
        {
            privGetInstance().privUpdate();
        }

        private void privUpdate()
        {
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT))
            {
                pArrowLeftPressed.Broadcast();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
            {
                pArrowRightPressed.Broadcast();
            }

            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);

            if (spaceKeyCurr && !privSpaceKeyPrev)
            {
                pSpacePressed.Broadcast();
            }

            privSpaceKeyPrev = spaceKeyCurr;
        }

        private static InputMan privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputMan();
            }

            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void SetInstance(InputMan pInInstance)
        {
            pInstance = pInInstance;
        }

        private static InputMan pInstance = null;
        private bool privSpaceKeyPrev;

        private InputSubject pArrowRightPressed;
        private InputSubject pArrowLeftPressed;
        private InputSubject pSpacePressed;
    }
}