using System;
using System.Collections.Generic;
using System.Text;

using Trivia.Events;

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
            game.PlayerLocationChanged += Game_PlayerLocationChanged;
            game.QuestionAsked += Game_QuestionAsked;
        }

        private void Game_QuestionAsked(object sender, QuestionAskedEventArgs e)
        {
            Console.WriteLine("The category is " + e.Category);
            Console.WriteLine(e.Question);
        }

        private void Game_PlayerLocationChanged(object sender, PlayerLocationChangedEventArgs e)
        {
            Console.WriteLine($"{e.PlayerName}'s new location is {e.newLocation}");
        }

        private void Game_PlayerAttemptedToEscapePenaltyBox(object sender, PlayerTryEscapeEventArgs e)
        {
            if (e.Succeeded)
            {
                Console.WriteLine(e.PlayerName + " is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine(e.PlayerName + " is not getting out of the penalty box");
            }
        }

        private void Game_PlayerRolled(object sender, PlayerRolledEventArgs e)
        {
            Console.WriteLine("They have rolled a " + e.RollValue);
        }

        private void Game_CurrentPlayerChanged(object sender, PlayerEventArgs e)
        {
            Console.WriteLine(e.PlayerName + " is the current player");
        }

        private void Game_PlayerAdded(object sender, PlayerAddedEventArgs e)
        {
            Console.WriteLine(e.PlayerName + " was added");
            Console.WriteLine("They are player number " + e.PlayerNumber);
        }
    }
}
