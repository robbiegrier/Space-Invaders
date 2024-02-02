using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class FallStrategy
    {
        public abstract void Fall(Bomb pBomb);

        public abstract void Reset(float height);
    }
}