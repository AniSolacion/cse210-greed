using System;  

namespace Unit04.Game.Casting
{
    public class Score
    {
        private int score = 100;

        public Score()
        {
        }

        public int getScore()
        {
            return score;
        }

        public void updateScore(int score)
        {
            this.score += score;
        }
    }
}