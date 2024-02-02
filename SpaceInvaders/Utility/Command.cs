using System;

namespace SpaceInvaders
{
    public abstract class Command
    {
        public abstract void Execute(float deltaTime);
    }
}
