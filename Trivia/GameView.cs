using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class GameView
    {
        public GameView(Game game)
        {
            game.PlayerAdded += Game_PlayerAdded;
        }

        private void Game_PlayerAdded(object sender, Game.PlayerAddedEventArgs e)
        {
            Console.WriteLine(e.PlayerName + " was added");
            Console.WriteLine("They are player number " + e.PlayerNumber);
        }
    }
}
