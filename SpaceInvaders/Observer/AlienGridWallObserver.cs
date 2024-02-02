using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienGridWallObserver : CollisionObserver
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
            AlienGrid pGrid = (AlienGrid)pSubject.pObjA;
            WallCategory pWall = (WallCategory)pSubject.pObjB;

            if (pWall.GetWallType() == WallCategory.Type.Right && !pGrid.IsMovingLeft())
            {
                pGrid.StartMovingLeft();
                pGrid.MoveDownNextFrame();
            }
            else if (pWall.GetWallType() == WallCategory.Type.Left && pGrid.IsMovingLeft())
            {
                pGrid.StartMovingRight();
                pGrid.MoveDownNextFrame();
            }
        }
    }
}