using System;

namespace SpaceInvaders
{
    public abstract class Predicate
    {
        public abstract bool Check(object pSubject);
    }

    public abstract class BinaryComparator : Predicate
    {
        private object pLeftHandSide;

        public BinaryComparator With(object pLeft)
        {
            pLeftHandSide = pLeft;
            return this;
        }

        public override bool Check(object pSubject)
        {
            return Compare(pLeftHandSide, pSubject);
        }

        public abstract bool Compare(object pLeft, object pRight);
    }
}
