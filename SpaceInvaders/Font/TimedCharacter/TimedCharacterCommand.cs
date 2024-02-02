using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class TimedCharacterCommand : Command
    {
        public TimedCharacterCommand(TimedCharacterCommand _pCmd_old, string _pLetter, float _red, float _green, float _blue, Font pInFontHandle, int inSession)
        {
            pLetter = _pLetter;
            red = _red;
            green = _green;
            blue = _blue;
            pPrevCmd = _pCmd_old;
            pFontHandle = pInFontHandle;
            session = inSession;
        }

        public override void Execute(float deltaTime)
        {
            if (pFontHandle.session != session)
            {
                //Debug.WriteLine("discard stale " + pLetter);
                return;
            }

            pFontHandle.UpdateMessage(pLetter);
            pFontHandle.SetColor(red, green, blue);

            //Debug.WriteLine("execute " + pLetter);
        }

        private string pLetter;
        private float red;
        private float green;
        private float blue;
        private TimedCharacterCommand pPrevCmd;
        private Font pFontHandle;
        public int session;
    }
}