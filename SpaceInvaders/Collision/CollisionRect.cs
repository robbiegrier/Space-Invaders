using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class CollisionRect : Azul.Rect
    {
        public CollisionRect(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
        }

        public CollisionRect(Azul.Rect pRect)
            : base(pRect)
        {
        }

        public CollisionRect(CollisionRect pRect)
            : base(pRect)
        {
        }

        public CollisionRect()
            : base()
        {
        }

        public new void Set(float x, float y, float width, float height)
        {
            base.Set(x, y, width, height);
        }

        public new void Set(Azul.Rect pRect)
        {
            Debug.Assert(pRect != null);
            base.Set(pRect);
        }

        public float Area()
        {
            return height * width;
        }

        public bool IsZero()
        {
            float epsilon = 0.0000001f;
            float area = Area();
            return area < epsilon && area > -epsilon;
        }

        public void Minimize()
        {
            height = 0f;
            width = 0f;
        }

        public static bool Intersect(CollisionRect a, CollisionRect b)
        {
            // Allow rects to be zero area and not collide with anything
            if (a.IsZero() || b.IsZero()) return false;

            float minXA = a.x - (a.width / 2f);
            float maxXA = a.x + (a.width / 2f);
            float minYA = a.y - (a.height / 2f);
            float maxYA = a.y + (a.height / 2f);

            float minXB = b.x - (b.width / 2f);
            float maxXB = b.x + (b.width / 2f);
            float minYB = b.y - (b.height / 2f);
            float maxYB = b.y + (b.height / 2f);

            return !((maxXB < minXA) || (minXB > maxXA) || (maxYB < minYA) || (minYB > maxYA));
        }

        public void Union(CollisionRect ColRect)
        {
            if (ColRect.IsZero()) return;

            float minX;
            float minY;
            float maxX;
            float maxY;

            if ((this.x - this.width / 2) < (ColRect.x - ColRect.width / 2))
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }

            if ((this.x + this.width / 2) > (ColRect.x + ColRect.width / 2))
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            if ((this.y + this.height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            if ((this.y - this.height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }

            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;
        }
    }
}