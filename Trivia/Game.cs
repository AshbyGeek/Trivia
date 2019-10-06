using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{

    public class Game
    {
        private readonly List<Player> players = new List<Player>();
        private readonly Questions questions = new Questions();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        Player CurrentPlayer => players[currentPlayer];

        public class PlayerAddedEventArgs : EventArgs
        {
            public string PlayerName;

            /// <summary>
            /// The player's placement in turn order
            /// </summary>
            public int PlayerNumber;
        }
        public event EventHandler<PlayerAddedEventArgs> PlayerAdded;
        protected void OnPlayerAdded(string playerName, int playerNumber)
        {
            PlayerAdded?.Invoke(this, new PlayerAddedEventArgs
            {
                PlayerName = playerName,
                PlayerNumber = playerNumber,
            });
        }

        public event EventHandler<Player> CurrentPlayerChanged;
        protected void OnCurrentPlayerChanged(Player newPlayer)
        {
            CurrentPlayerChanged?.Invoke(this, newPlayer);
        }

        public class PlayerRolledEventArgs : EventArgs
        {
            public int rollValue;
        }
        public event EventHandler<PlayerRolledEventArgs> PlayerRolled;
        protected void OnPlayerRolled(int rollValue)
        {
            PlayerRolled.Invoke(this, new PlayerRolledEventArgs()
            {
                rollValue = rollValue,
            });
        }

        public class PlayerTryEscapeEventArgs : EventArgs
        {
            public string playerName;
            public bool succeeded;
        }
        public event EventHandler<PlayerTryEscapeEventArgs> PlayerAttemptedToEscapePenaltyBox;
        protected void OnPlayerAttemptedToEscapePenaltyBox(bool success)
        {
            PlayerAttemptedToEscapePenaltyBox?.Invoke(this, new PlayerTryEscapeEventArgs
            {
                playerName = CurrentPlayer.Name,
                succeeded = success,
            });
        }



        public Game()
        {
            questions.MakeDumbDefaultQuestions();
        }

        public bool add(String playerName)
        {
            players.Add(new Player(playerName));
            OnPlayerAdded(playerName, players.Count);
            return true;
        }

        public void roll(int roll)
        {
            OnCurrentPlayerChanged(CurrentPlayer);
            OnPlayerRolled(roll);


            if (CurrentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;
                    Console.WriteLine(CurrentPlayer.Name + " is getting out of the penalty box");
                }
                else
                {
                    Console.WriteLine(CurrentPlayer.Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                    return;
                }
            }

            CurrentPlayer.Place += roll;
            if (CurrentPlayer.Place > 11) CurrentPlayer.Place = CurrentPlayer.Place - 12;

            Console.WriteLine($"{CurrentPlayer.Name}'s new location is {CurrentPlayer.Place}");
            Console.WriteLine("The category is " + currentCategory());
            askQuestion();
        }

        private void askQuestion()
        {
            string question = questions.GetAndDiscardQuestion(currentCategory());
            Console.WriteLine(question);
        }


        private String currentCategory()
        {
            return CurrentPlayer.Place switch
            {
                0 => "Pop",
                4 => "Pop",
                8 => "Pop",
                1 => "Science",
                5 => "Science",
                9 => "Science",
                2 => "Sports",
                6 => "Sports",
                10 => "Sports",
                _ => "Rock",
            };
        }

        private void MoveToNextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count)
            {
                currentPlayer = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if play should continue</returns>
        public bool wasCorrectlyAnswered()
        {
            if (CurrentPlayer.IsInPenaltyBox && !isGettingOutOfPenaltyBox)
            {
                MoveToNextPlayer();
                return true;
            }
            else
            {
                Console.WriteLine("Answer was correct!!!!");
                CurrentPlayer.Purse++;
                Console.WriteLine($"{CurrentPlayer.Name} now has {CurrentPlayer.Purse} Gold Coins.");

                bool winner = PlayerWon();
                MoveToNextPlayer();

                return !winner;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if play should continue</returns>
        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
            CurrentPlayer.IsInPenaltyBox = true;
            MoveToNextPlayer();
            return true;
        }


        private bool PlayerWon()
        {
            return CurrentPlayer.Purse >= 6;
        }
    }

}
