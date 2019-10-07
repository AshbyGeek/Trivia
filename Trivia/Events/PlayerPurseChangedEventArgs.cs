using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class PlayerPurseChangedEventArgs : PlayerEventArgs
    {
        public int NewPurseValue;
    }
}
