using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class PlayerLocationChangedEventArgs : PlayerEventArgs
    {
        public int newLocation;
    }
}
