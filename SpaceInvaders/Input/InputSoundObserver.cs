using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class InputSoundObserver : InputObserver
    {
        public InputSoundObserver(IrrKlang.ISoundSource pInSource)
        {
            pSoundSource = pInSource;
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override Enum GetName()
        {
            return Name.SoundObserver;
        }

        public override void Notify()
        {
            SoundSystem.Play(pSoundSource);
        }

        private IrrKlang.ISoundSource pSoundSource;
    }
}