using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trivia.Events;

namespace Trivia
{

    public class Game
    {
        private readonly ObservableCollectionItemChanged<Player> players = new ObservableCollectionItemChanged<Player>();
        private readonly Questions questions = new Questions();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        Player CurrentPlayer => players[currentPlayer];

        public event EventHandler<PlayerAddedEventArgs> PlayerAdded;
        protected void OnPlayerAdded(string playerName, int playerNumber)
        {
            PlayerAdded?.Invoke(this, new PlayerAddedEventArgs
            {
                PlayerName = playerName,
                PlayerNumber = playerNumber,
            });
        }

        public event EventHandler<PlayerEventArgs> CurrentPlayerChanged;
        protected void OnCurrentPlayerChanged(string newCurrentPlayerName)
        {
            CurrentPlayerChanged?.Invoke(this, new PlayerEventArgs
            {
                PlayerName = newCurrentPlayerName
            });
        }

        public event EventHandler<PlayerRolledEventArgs> PlayerRolled;
        protected void OnPlayerRolled(string playerName, int rollValue)
        {
            PlayerRolled.Invoke(this, new PlayerRolledEventArgs()
            {
                PlayerName = playerName,
                RollValue = rollValue,
            });
        }

        public event EventHandler<PlayerTryEscapeEventArgs> PlayerAttemptedToEscapePenaltyBox;
        protected void OnPlayerAttemptedToEscapePenaltyBox(string playerName, bool success)
        {
            PlayerAttemptedToEscapePenaltyBox?.Invoke(this, new PlayerTryEscapeEventArgs
            {
                PlayerName = playerName,
                Succeeded = success,
            });
        }

        public event EventHandler<PlayerLocationChangedEventArgs> PlayerLocationChanged;
        protected void OnPlayerLocationChanged(string playerName, int newLocation)
        {
            PlayerLocationChanged?.Invoke(this, new PlayerLocationChangedEventArgs
            {
                PlayerName = playerName,
                newLocation = newLocation,
            });
        }

        public event EventHandler<QuestionAskedEventArgs> QuestionAsked;
        protected void OnQuestionAsked(string category, string question)
        {
            QuestionAsked?.Invoke(this, new QuestionAskedEventArgs
            {
                Category = category,
                Question = question,
            });
        }

        public event EventHandler<QuestionAnsweredEventArgs> QuestionAnswered;
        protected void OnQuestionAnswered(string playerName, bool correctAnswer)
        {
            QuestionAnswered?.Invoke(this, new QuestionAnsweredEventArgs
            {
                PlayerName = playerName,
                CorrectAnswer = correctAnswer,
            });
        }

        public event EventHandler<PlayerEventArgs> PlayerSentToPenaltyBox;
        protected void OnPlayerSentToPenaltyBox(string playerName)
        {
            PlayerSentToPenaltyBox?.Invoke(this, new PlayerEventArgs
            {
                PlayerName = playerName,
            });
        }

        public event EventHandler<PlayerPurseChangedEventArgs> PlayerPurseChanged;
        protected void OnPlayerPurseChanged(string playerName, int newPurseValue)
        {
            PlayerPurseChanged?.Invoke(this, new PlayerPurseChangedEventArgs
            {
                PlayerName = playerName,
                NewPurseValue = newPurseValue,
            });
        }




        public Game()
        {
            questions.MakeDumbDefaultQuestions();

            players.ItemPropertyChanged += (s, e) =>
            {
                var player = s as Player;
                if (e.PropertyName == nameof(Player.Purse))
                {
                    OnPlayerPurseChanged(player.Name, player.Purse);
                }
                else if (e.PropertyName == nameof(Player.Place))
                {
                    OnPlayerLocationChanged(player.Name, player.Place);
                }
            };
        }




        public bool Add(String playerName)
        {
            players.Add(new Player(playerName));
            OnPlayerAdded(playerName, players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            OnCurrentPlayerChanged(CurrentPlayer.Name);
            OnPlayerRolled(CurrentPlayer.Name, roll);

            if (CurrentPlayer.IsInPenaltyBox)
            {
                isGettingOutOfPenaltyBox = roll % 2 != 0;

                //This would seem to be the logical bugfix but we are preserving the original functionality of this game exactly
                //CurrentPlayer.IsInPenaltyBox = !isGettingOutOfPenaltyBox; 

                OnPlayerAttemptedToEscapePenaltyBox(CurrentPlayer.Name, isGettingOutOfPenaltyBox);
            }

            if (!CurrentPlayer.IsInPenaltyBox || isGettingOutOfPenaltyBox)
            {
                CurrentPlayer.Place += roll;
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            string question = questions.GetAndDiscardQuestion(CurrentCategory());
            OnQuestionAsked(CurrentCategory(), question);
        }


        private String CurrentCategory()
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
        public bool WasCorrectlyAnswered()
        {
            if (CurrentPlayer.IsInPenaltyBox && !isGettingOutOfPenaltyBox)
            {
                MoveToNextPlayer();
                return true;
            }
            else
            {
                OnQuestionAnswered(CurrentPlayer.Name, true);
                CurrentPlayer.Purse++;

                bool winner = PlayerWon();
                MoveToNextPlayer();

                return !winner;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if play should continue</returns>
        public bool WrongAnswer()
        {
            OnQuestionAnswered(CurrentPlayer.Name, false);
            CurrentPlayer.IsInPenaltyBox = true;
            OnPlayerSentToPenaltyBox(CurrentPlayer.Name);
            MoveToNextPlayer();
            return true;
        }


        private bool PlayerWon()
        {
            return CurrentPlayer.Purse >= 6;
        }
    }

}
