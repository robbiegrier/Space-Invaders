using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SampleCommand : Command
    {
        public SampleCommand(String txt)
        {
            pString = txt;
        }

        public override void Execute(float deltaTime)
        {
            Debug.WriteLine(" {0} time:{1} ", pString, TimerEventMan.GetCurrTime());
        }

        private String pString;
    }
}
