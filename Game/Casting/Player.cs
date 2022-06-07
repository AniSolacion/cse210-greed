using System;  

namespace Unit04.Game.Casting
{
    public class Player : Actor
    {
        private int score = 0;

        public Player()
        {
        }

        public int getScore()
        {
            return score;
        }

        public void addScore(int score)
        {
            this.score += score;
        }
    }
}