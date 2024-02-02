using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class UfoWallObserver : CollisionObserver
    {
        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override Enum GetName()
        {
            return Name.GridObserver;
        }

        public override void Notify()
        {
            Ufo pAlien = (Ufo)pSubject.pObjA;
            WallCategory pWall = (WallCategory)pSubject.pObjB;

            if (pWall.GetWallType() == WallCategory.Type.Right && !pAlien.IsMovingLeft())
            {
                pAlien.Destroy();
            }
            else if (pWall.GetWallType() == WallCategory.Type.Left && pAlien.IsMovingLeft())
            {
                pAlien.Destroy();
            }
        }
    }
}