using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class AlienKilledObserver : CollisionObserver
    {
        public override void Notify()
        {
            AlienBase pAlien = (AlienBase)pSubject.pObjB;

            SpriteGame.Name splatterSprite = SpriteGame.Name.AlienExplosion;

            if (pAlien.GetGameObjectName() == GameObject.Name.OctopusAlien)
            {
                ShipMan.GetShip().AddScore(10);
            }
            else if (pAlien.GetGameObjectName() == GameObject.Name.CrabAlien)
            {
                ShipMan.GetShip().AddScore(20);
            }
            else if (pAlien.GetGameObjectName() == GameObject.Name.SquidAlien)
            {
                ShipMan.GetShip().AddScore(30);
            }
            else if (pAlien.GetGameObjectName() == GameObject.Name.Ufo)
            {
                int score;

                Random rand = new Random();
                int choice = rand.Next(5);

                switch (choice)
                {
                    case 0: score = 50; break;
                    case 1: score = 100; break;
                    case 2: score = 150; break;
                    case 3: score = 200; break;
                    default: score = 300; break;
                }

                ShipMan.GetShip().AddScore(score);

                Font pScore = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, score.ToString(), Glyph.Name.SpaceInvaders, pAlien.x, pAlien.y);
                pScore.SetColor(0.9f, 0.3f, 0.3f);
                FontRemoveCommand.Launch(pScore, 1.0f);

                splatterSprite = SpriteGame.Name.SaucerExplosion;
            }

            AlienGrid alienGrid = (AlienGrid)GameObjectNodeMan.Find(GameObject.Name.AlienGrid);
            alienGrid.IncreaseDifficulty();

            SplatterRoot.MakeSplatter(splatterSprite, pAlien.x, pAlien.y);
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.AlienKilledObserver;
        }
    }

    internal class FontRemoveCommand : Command
    {
        public FontRemoveCommand(Font pInSubject)
        {
            pSubject = pInSubject;
        }

        public override void Execute(float deltaTime)
        {
            pSubject.Remove();
            FontMan.Remove(pSubject);
        }

        public static void Launch(Font pInSubject, float time)
        {
            TimerEventMan.Add(TimerEvent.Name.SplatterRemove, new FontRemoveCommand(pInSubject), time);
        }

        private Font pSubject;
    }
}