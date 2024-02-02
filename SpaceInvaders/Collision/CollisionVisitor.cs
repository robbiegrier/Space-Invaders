using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal abstract class CollisionVisitor : DLink
    {
        public virtual void VisitAlienGrid(AlienGrid b)
        {
            Debug.WriteLine("Visit by AlienGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienColumn(AlienColumn b)
        {
            Debug.WriteLine("Visit by AlienColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSquid(Squid b)
        {
            Debug.WriteLine("Visit by Squid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCrab(Crab b)
        {
            Debug.WriteLine("Visit by Crab not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitOctopus(Octopus b)
        {
            Debug.WriteLine("Visit by Octopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship s)
        {
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShipRoot(ShipRoot s)
        {
            Debug.WriteLine("Visit by ShipRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBirdGrid(BirdGrid b)
        {
            Debug.WriteLine("Visit by BirdGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBirdColumn(BirdColumn b)
        {
            Debug.WriteLine("Visit by BirdColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitRedBird(BirdRed b)
        {
            Debug.WriteLine("Visit by RedBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitYellowBird(BirdYellow b)
        {
            Debug.WriteLine("Visit by YellowBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitGreenBird(BirdGreen b)
        {
            Debug.WriteLine("Visit by Octopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWhiteBird(BirdWhite b)
        {
            Debug.WriteLine("Visit by Octopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup m)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallLeft(WallLeft m)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight m)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop m)
        {
            Debug.WriteLine("Visit by WallTop not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(GameObjectNull n)
        {
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb b)
        {
            Debug.WriteLine("Visit by Bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBombRoot(BombRoot b)
        {
            Debug.WriteLine("Visit by BombRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallBottom(WallBottom w)
        {
            Debug.WriteLine("Visit by WallBottom not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldRoot(ShieldRoot s)
        {
            Debug.WriteLine("Visit by ShieldRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldColumn(ShieldColumn s)
        {
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldBrick(ShieldBrick s)
        {
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldGrid(ShieldGrid s)
        {
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUfoRoot(UfoRoot u)
        {
            Debug.WriteLine("Visit by UfoRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUfo(Ufo u)
        {
            Debug.WriteLine("Visit by Ufo not implemented");
            Debug.Assert(false);
        }

        public abstract void Accept(CollisionVisitor other);
    }
}