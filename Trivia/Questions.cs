using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    public class Questions
    {
        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        public void MakeDumbDefaultQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast("Science Question " + i);
                sportsQuestions.AddLast("Sports Question " + i);
                rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public string GetAndDiscardQuestion(string category)
        {
            string question;
            if (category == "Pop")
            {
                question = popQuestions.First();
                popQuestions.RemoveFirst();
            }
            else if (category == "Science")
            {
                question = scienceQuestions.First();
                scienceQuestions.RemoveFirst();
            }
            else if (category == "Sports")
            {
                question = sportsQuestions.First();
                sportsQuestions.RemoveFirst();
            }
            else if (category == "Rock")
            {
                question = rockQuestions.First();
                rockQuestions.RemoveFirst();
            }
            else
            {
                throw new ArgumentException("Unknown category");
            }
            return question;
        }
    }
}
