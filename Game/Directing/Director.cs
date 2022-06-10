using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        public Score score = new Score();

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.DrawActor(cast.GetFirstActor("banner"));
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the player.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);   
        }

        /// <summary>
        /// Updates the player's position and resolves any collisions with gems or rocks.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Random random = new Random();
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            List<Actor> gems = cast.GetActors("gems");
            List<Actor> rocks = cast.GetActors("rocks");

            string curr_score = score.getScore().ToString();
            banner.SetText(curr_score);
            
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);

            foreach (Actor actor in gems)
            {
                // Move
                Gem gem = (Gem) actor;
                gem.MoveNext(maxX, maxY);

                // Check if touched player.
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    score.updateScore(gem.getScore());

                    //Creates uniformed position
                    int ran = random.Next(1,maxX);
                    int rem = ran % 15;
                    int x = ran - rem;

                    Point new_pos = new Point(x, 0);
                    gem.SetPosition(new_pos);
                }

                // Check if reached bottom.
                if (actor.GetPosition().GetY() >= maxY)
                {
                    //Creates uniformed position
                    int ran = random.Next(1,maxX);
                    int rem = ran % 15;
                    int x = ran - rem;

                    Point new_pos = new Point(x, 0);
                    gem.SetPosition(new_pos);
                }
            }

            foreach (Actor actor in rocks)
            {
                // Move
                 Rock rock = (Rock) actor;
                 rock.MoveNext(maxX, maxY);

                // Check if touched player.
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    score.updateScore(rock.getScore());

                    //Creates uniformed position
                    int ran = random.Next(1,maxX);
                    int rem = ran % 15;
                    int x = ran - rem;

                    Point new_pos = new Point(x, 0);
                    rock.SetPosition(new_pos);
                }

                // Check if reached bottom.
                if (actor.GetPosition().GetY() >= maxY)
                {
                    //Creates uniformed position
                    int ran = random.Next(1,maxX);
                    int rem = ran % 15;
                    int x = ran - rem;

                    Point new_pos = new Point(x, 0);
                    rock.SetPosition(new_pos);
                }
            }
            if (score.getScore() <= 0)
            {
                banner.SetText("Game Over");
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}