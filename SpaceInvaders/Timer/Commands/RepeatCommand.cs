using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RepeatCommand : Command
    {
        public RepeatCommand(String inTxt, float deltaRepeatTime)
        {
            pString = inTxt;
            repeatDelta = deltaRepeatTime;
        }

        public override void Execute(float deltaTime)
        {
            Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());

            // Add itself back to timer
            TimerEventMan.Add(TimerEvent.Name.RepeatSample, this, repeatDelta);
        }

        private String pString;
        private float repeatDelta;
    }
}
