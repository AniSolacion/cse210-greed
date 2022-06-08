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

        public Score score;

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
            while (score.getScore() >= 0)
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
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            List<Actor> gems = cast.GetActors("gems");
            List<Actor> rocks = cast.GetActors("rocks");

            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);

            foreach (Actor actor in gems)
            {
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Gem gem = (Gem) actor;
                    score.updateScore(gem.getScore());
                    cast.RemoveActor("gems", gem);
                }

                if (actor.GetPosition().GetY <= 0)
                {
                    Gem gem = (Gem) actor;
                    cast.RemoveActor("gems", gem);
                }
            }

            foreach (Actor actor in rocks)
            {
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Rock rock = (Rock) actor;
                    score.updateScore(rock.getScore());
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