using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class GameView
    {
        public string CurrentPlayer;

        public GameView(Game game)
        {
            game.PlayerAdded += Game_PlayerAdded;
            game.CurrentPlayerChanged += Game_CurrentPlayerChanged;
            game.PlayerRolled += Game_PlayerRolled;
            game.PlayerAttemptedToEscapePenaltyBox += Game_PlayerAttemptedToEscapePenaltyBox;
        }

        private void Game_PlayerAttemptedToEscapePenaltyBox(object sender, Game.PlayerTryEscapeEventArgs e)
        {
            if (e.succeeded)
            {
                Console.WriteLine(e.playerName + " is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine(e.playerName + " is not getting out of the penalty box");
            }
        }

        private void Game_PlayerRolled(object sender, Game.PlayerRolledEventArgs e)
        {
            Console.WriteLine("They have rolled a " + e.rollValue);
        }

        private void Game_CurrentPlayerChanged(object sender, Player e)
        {
            Console.WriteLine(e.Name + " is the current player");
        }

        private void Game_PlayerAdded(object sender, Game.PlayerAddedEventArgs e)
        {
            Console.WriteLine(e.PlayerName + " was added");
            Console.WriteLine("They are player number " + e.PlayerNumber);
        }
    }
}
