using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class MissileGroup : Composite
    {
        public MissileGroup()
            : base()
        {
            name = Name.MissileGroup;
            pCollisionObject.pColSprite.SetColor(0, 0, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissileGroup(this);
        }

        public static MissileGroup Generate()
        {
            SpriteBatch pMissileBatch = SpriteBatchMan.Find(SpriteBatch.Name.Missiles);

            MissileGroup pMissileGroup = new MissileGroup();
            pMissileGroup.ActivateSprite(pMissileBatch);
            pMissileGroup.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));

            GameObjectNodeMan.Attach(pMissileGroup);

            return pMissileGroup;
        }
    }
}