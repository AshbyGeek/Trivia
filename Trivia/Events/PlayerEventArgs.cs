using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class PlayerEventArgs : EventArgs
    {
        /// <summary>
        /// The name of the player on which this event is enacted
        /// </summary>
        public string PlayerName;
    }
}
