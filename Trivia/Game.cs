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
        string CurrentPlayerName => CurrentPlayer.Name;

        public Game()
        {
            questions.MakeDumbDefaultQuestions();
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(new Player(playerName));

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (CurrentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(CurrentPlayerName + " is getting out of the penalty box");
                    CurrentPlayer.Place = CurrentPlayer.Place + roll;
                    if (CurrentPlayer.Place > 11) CurrentPlayer.Place = CurrentPlayer.Place - 12;

                    Console.WriteLine(CurrentPlayerName
                            + "'s new location is "
                            + CurrentPlayer.Place);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(CurrentPlayerName + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                CurrentPlayer.Place = CurrentPlayer.Place + roll;
                if (CurrentPlayer.Place > 11) CurrentPlayer.Place = CurrentPlayer.Place - 12;

                Console.WriteLine(CurrentPlayerName
                        + "'s new location is "
                        + CurrentPlayer.Place);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

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

        public bool wasCorrectlyAnswered()
        {
            if (CurrentPlayer.IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    CurrentPlayer.Purse++;
                    Console.WriteLine(CurrentPlayerName
                            + " now has "
                            + CurrentPlayer.Purse
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                CurrentPlayer.Purse++;
                Console.WriteLine(CurrentPlayerName
                        + " now has "
                        + CurrentPlayer.Purse
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayerName + " was sent to the penalty box");
            CurrentPlayer.IsInPenaltyBox = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(CurrentPlayer.Purse == 6);
        }
    }

}
