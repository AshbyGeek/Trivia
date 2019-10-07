using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class QuestionAnsweredEventArgs : PlayerEventArgs
    {
        /// <summary>
        /// True if the player gave the correct answer
        /// </summary>
        public bool CorrectAnswer;
    }
}
