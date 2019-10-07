using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class PlayerAddedEventArgs : PlayerEventArgs
    {
        /// <summary>
        /// The player's placement in turn order
        /// </summary>
        public int PlayerNumber;
    }
}
