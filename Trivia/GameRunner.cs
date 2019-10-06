using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Trivia;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(String[] args)
        {
            if (args.Count() == 1 && args.First() == "-GenerateUnitTestData")
            {
                GenerateUnitTestData();
            }
            else
            {
                RunNormalGame();
            }
        }

        public static void RunNormalGame() => RunGame(new Random());

        public static void GenerateUnitTestData()
        {
            TextWriter oldOut = Console.Out;

            for (int i = 0; i < 50; i++)
            {
                using (var fileStream = new FileStream($"Game{i:00}.txt", FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(fileStream))
                {
                    Console.SetOut(writer);

                    RunGame(new Random(i));

                    Console.SetOut(oldOut);
                }
            };
        }

        /// <summary>
        /// Runs the game using the given random number generator.
        /// This abstraction allows unit tests to initialize a beneficial random number generator
        /// </summary>
        /// <param name="rand"></param>
        public static void RunGame(Random rand)
        {
            Game aGame = new Game();
            bool notAWinner;

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);
        }
    }

}

