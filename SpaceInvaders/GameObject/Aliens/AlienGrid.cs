using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienGrid : Composite
    {
        public AlienGrid()
            : base()
        {
            SetName(Name.AlienGrid);
            pCollisionObject.pColSprite.SetColor(0.0f, 1.0f, 0.0f);

            // LTN - store iterator once, and reuse
            pIt = new IteratorComposite();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void BeginPlay()
        {
            base.BeginPlay();

            // LTN - Passing in the command to the timer event which will own it long term
            AlienMovementCommand pAlienMovementCommand = new AlienMovementCommand();
            TimerEventMan.Add(TimerEvent.Name.AlienMovement, pAlienMovementCommand, GetMovementDelay());
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienGrid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObject = (GameObject)IteratorComposite.GetChild(this);
            CollisionPair.Collide(m, pGameObject);
        }

        public void StartMovingLeft()
        {
            sign = -1f;
        }

        public void StartMovingRight()
        {
            sign = 1f;
        }

        public bool IsMovingLeft()
        {
            return sign < 0f;
        }

        public void MoveAliens()
        {
            if (ShipMan.GetShip().GetStateName() == ShipState.Name.End)
            {
                return;
            }

            if (!movementStateDown)
            {
                pIt.Reset(this);

                for (pIt.First(); !pIt.IsDone(); pIt.Next())
                {
                    GameObject pObject = (GameObject)pIt.Curr();
                    pObject.AddLocationOffset(pixelsToMove * sign, 0f);
                }
            }
            else
            {
                MoveAliensDown();
                movementStateDown = false;
            }
        }

        private void MoveAliensDown()
        {
            pIt.Reset(this);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pObject = (GameObject)pIt.Curr();
                pObject.AddLocationOffset(0f, -pixelsToMoveDown);
            }
        }

        public void MoveDownNextFrame()
        {
            movementStateDown = true;
        }

        public bool AllAliensDead()
        {
            return IsEmpty();
        }

        public float GetMovementDelay()
        {
            return movementDelay;
        }

        public void IncreaseDifficulty()
        {
            SetMovementDelay(Math.Max(0.01f, movementDelay - movementDelayDecrease));
        }

        public void ResetDifficutly()
        {
            SetMovementDelay(0.5f);
        }

        public void SetMovementDelay(float inDelay)
        {
            movementDelay = inDelay;
            SpriteGameMan.Find(SpriteGame.Name.Squid).animationSpeed = inDelay;
            SpriteGameMan.Find(SpriteGame.Name.Octopus).animationSpeed = inDelay;
            SpriteGameMan.Find(SpriteGame.Name.Crab).animationSpeed = inDelay;
        }

        public static AlienGrid Generate(float startBottomLeftX, float startBottomLeftY, float horizontalStep, float verticalStep, int columns)
        {
            // STN - Just using the factory in LoadContent(), so this is only called once at the start
            return new AlienFactory().Generate(startBottomLeftX, startBottomLeftY, horizontalStep, verticalStep, columns);
        }

        public void Regenerate(float startBottomLeftX, float startBottomLeftY, float horizontalStep, float verticalStep, int columns)
        {
            movementStateDown = false;
            SetMovementDelay(0.5f);
            sign = 1f;

            // STN - Just using the factory in LoadContent(), so this is only called once at the start
            new AlienFactory().Regenerate(this, startBottomLeftX, startBottomLeftY, horizontalStep, verticalStep, columns);
        }

        private float sign = 1f;
        private static readonly float pixelsToMove = 4.0f;
        private static float pixelsToMoveDown = 20f;
        private float movementDelay = 0.5f;
        private float movementDelayDecrease = 0.0075f;
        private bool movementStateDown = false;

        private IteratorComposite pIt;
    }
}