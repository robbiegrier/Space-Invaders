using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class PlayerMan
    {
        public enum Mode
        {
            SinglePlayer,
            MultiPlayer
        }

        private PlayerMan()
        {
            pPlayer1 = new Player(Player.Name.Player1);
            pPlayer2 = new Player(Player.Name.Player2);
            pPlayerActive = pPlayer1;
        }

        public static void Create()
        {
            pInstance = new PlayerMan();
        }

        public static void Destroy()
        {
        }

        public static Player GetPlayer1()
        {
            return privGetInstance().pPlayer1;
        }

        public static Player GetPlayer2()
        {
            return privGetInstance().pPlayer2;
        }

        public static Player GetActivePlayer()
        {
            return privGetInstance().pPlayerActive;
        }

        public static void SetActivePlayer(Player.Name inName)
        {
            if (inName == Player.Name.Player1)
            {
                privGetInstance().pPlayerActive = privGetInstance().pPlayer1;
            }
            else
            {
                privGetInstance().pPlayerActive = privGetInstance().pPlayer2;
            }
        }

        private static PlayerMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static Mode GetGameMode()
        {
            return privGetInstance().gameMode;
        }

        public static void SetGameMode(Mode inMode)
        {
            privGetInstance().gameMode = inMode;
        }

        private static PlayerMan pInstance;
        private Player pPlayer1;
        private Player pPlayer2;
        private Player pPlayerActive;

        private Mode gameMode = Mode.SinglePlayer;
    }
}