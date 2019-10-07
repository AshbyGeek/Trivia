using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class QuestionAskedEventArgs : EventArgs
    {
        /// <summary>
        /// The category of the question
        /// </summary>
        public string Category;

        /// <summary>
        /// The actual question asked
        /// </summary>
        public string Question;
    }
}
