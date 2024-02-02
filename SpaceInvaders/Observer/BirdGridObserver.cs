using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class BirdGridObserver : CollisionObserver
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
            Debug.WriteLine("Grid_Observer: {0} {1}", pSubject.pObjA, pSubject.pObjB);

            BirdGrid pGrid = (BirdGrid)pSubject.pObjA;
            WallCategory pWall = (WallCategory)pSubject.pObjB;

            if (pWall.GetWallType() == WallCategory.Type.Right)
            {
                pGrid.SetDelta(-0.5f);
            }
            else if (pWall.GetWallType() == WallCategory.Type.Left)
            {
                pGrid.SetDelta(0.5f);
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}