using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class TimedCharacterFactory
    {
        public TimedCharacterFactory()
        {
        }

        // install a msg with delay
        public static Font Install(string pMessage, float deltaTimeToTrigger, float delayTime, float xPos, float yPos, float red, float green, float blue)
        {
            //Debug.WriteLine("install " + pMessage);
            Font pFont = FontMan.Add(Font.Name.TimedCharacter, SpriteBatch.Name.Texts, "", Glyph.Name.SpaceInvaders, xPos, yPos);
            pFont.session = session;

            deltaTimeToTrigger += deltaThisSession;

            // each cmd is linked
            TimedCharacterCommand pPrevCmd = null;

            for (int i = 0; i < pMessage.Length; i++)
            {
                // add up characters each cmd
                string pCharacter = pMessage.Substring(0, i + 1);

                // create the new command linked to the prev one
                TimedCharacterCommand pCmd = new TimedCharacterCommand(pPrevCmd, pCharacter, red, green, blue, pFont, session);

                // Set timer to execute the character cmd
                float time = deltaTimeToTrigger + i * delayTime;
                TimerEventMan.Add(TimerEvent.Name.TimedCharacter, pCmd, time);

                // update prev link
                pPrevCmd = pCmd;
            }

            session++;
            return pFont;
        }

        private static TimedCharacterFactory privInstance()
        {
            if (pInstance == null)
            {
                pInstance = new TimedCharacterFactory();
            }

            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void SetInstance(TimedCharacterFactory pInInstance)
        {
            pInstance = pInInstance;
        }

        private static TimedCharacterFactory pInstance = null;
        private static int session = 0;
        public static float deltaThisSession = 0f;
    }
}