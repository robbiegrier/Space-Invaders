using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class TimerEvent : DLink
    {
        public enum Name
        {
            Sample1,
            Sample2,
            Sample3,
            Sample4,
            Sample5,
            Sample6,
            Sample7,
            Sample8,
            Sample9,

            TimedCharacter,

            RepeatSample,
            Animation,

            ShipRespawn,
            SceneChange,

            SquidAnimation,
            CrabAnimation,
            OctopusAnimation,

            AlienMovement,

            SplatterRemove,

            Uninitialized
        }

        public TimerEvent()
            : base()
        {
            name = TimerEvent.Name.Uninitialized;
            pCommand = null;
            triggerTime = 0.0f;
            deltaTime = 0.0f;
        }

        public void Set(TimerEvent.Name eventName, Command pInCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pInCommand != null);

            name = eventName;
            pCommand = pInCommand;
            deltaTime = deltaTimeToTrigger;

            // set the trigger time
            triggerTime = TimerEventMan.GetCurrTime() + deltaTimeToTrigger;
        }

        public void Process()
        {
            Debug.Assert(pCommand != null);
            pCommand.Execute(deltaTime);
        }

        public new void Clear()
        {
            name = TimerEvent.Name.Uninitialized;
            pCommand = null;
            triggerTime = 0.0f;
            deltaTime = 0.0f;
        }

        public override System.Enum GetName()
        {
            return name;
        }

        public override void Wash()
        {
            baseClear();
            Clear();
        }

        public override void Dump()
        {
            Debug.WriteLine("   Name: {0} ({1})", name, GetHashCode());
            Debug.WriteLine("      Command: {0}", pCommand);
            Debug.WriteLine("   Event Name: {0}", name);
            Debug.WriteLine(" Trigger Time: {0}", triggerTime);
            Debug.WriteLine("   Delta Time: {0}", deltaTime);
            baseDump();
        }

        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;
    }
}