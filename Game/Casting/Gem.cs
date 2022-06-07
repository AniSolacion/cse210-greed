using System;  

namespace Unit04.Game.Casting
{
    public class Gem : Actor
    {
        private int score = 10;

        public Gem()
        {
        }

        public int getScore()
        {
            return score;
        }
    }
}