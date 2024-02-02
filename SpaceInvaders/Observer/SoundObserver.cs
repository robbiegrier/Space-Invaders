using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SoundObserver : CollisionObserver
    {
        public SoundObserver(IrrKlang.ISoundSource inSource)
        {
            Debug.Assert(inSource != null);
            pSoundSource = inSource;
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