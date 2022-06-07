using System;  

namespace Unit04.Game.Casting
{
    public class Rock : Actor
    {
        private int score = -10;

        public Rock()
        {
        }

        public int getScore()
        {
            return score;
        }
    }
}