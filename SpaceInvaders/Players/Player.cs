using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Player
    {
        public enum Name
        {
            Player1,
            Player2
        }

        public Player(Name inName)
        {
            name = inName;
            score = 0;
            lives = maxLives;
        }

        public void Reset()
        {
            score = 0;
            lives = maxLives;
        }

        public void SetScore(int inScore)
        {
            score = inScore;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetLives(int inLives)
        {
            lives = inLives;
        }

        public int GetLives()
        {
            return lives;
        }

        public Name name;
        private int score;
        private int lives;
        public static readonly int maxLives = 3;
    }
}