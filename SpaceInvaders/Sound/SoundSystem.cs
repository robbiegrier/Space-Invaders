using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class SoundSystem
    {
        public static void Create()
        {
            soundEngine = new IrrKlang.ISoundEngine();
            aliensSpeed1 = soundEngine.AddSoundSourceFromFile("fastinvader1.wav");
            aliensSpeed2 = soundEngine.AddSoundSourceFromFile("fastinvader2.wav");
            aliensSpeed3 = soundEngine.AddSoundSourceFromFile("fastinvader3.wav");
            aliensSpeed4 = soundEngine.AddSoundSourceFromFile("fastinvader4.wav");
            shoot = soundEngine.AddSoundSourceFromFile("shoot.wav");
            kill = soundEngine.AddSoundSourceFromFile("invaderkilled.wav");
            explosion = soundEngine.AddSoundSourceFromFile("explosion.wav");
            ufoHigh = soundEngine.AddSoundSourceFromFile("ufo_highpitch.wav");
            ufoLow = soundEngine.AddSoundSourceFromFile("ufo_lowpitch.wav");

            soundEngine.SoundVolume = 0.2f;
        }

        public static void Destroy()
        {
        }

        public static void Update()
        {
            soundEngine.Update();
        }

        public static void Play(IrrKlang.ISoundSource source)
        {
            soundEngine.Play2D(source, false, false, false);
        }

        public static IrrKlang.ISoundEngine soundEngine = null;
        public static IrrKlang.ISoundSource aliensSpeed1 = null;
        public static IrrKlang.ISoundSource aliensSpeed2 = null;
        public static IrrKlang.ISoundSource aliensSpeed3 = null;
        public static IrrKlang.ISoundSource aliensSpeed4 = null;
        public static IrrKlang.ISoundSource shoot = null;
        public static IrrKlang.ISoundSource kill = null;
        public static IrrKlang.ISoundSource explosion = null;
        public static IrrKlang.ISoundSource ufoHigh = null;
        public static IrrKlang.ISoundSource ufoLow = null;
    }
}