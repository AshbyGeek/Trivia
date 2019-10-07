using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.Events
{
    public class PlayerTryEscapeEventArgs : PlayerEventArgs
    {
        public bool Succeeded;
    }
}
