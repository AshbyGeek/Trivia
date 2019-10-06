using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class Player
    {
        public Player(string name) => Name = name;

        public string Name { get; }

        public int Place { get; set; } = 0;
        public int Purse { get; set; } = 0;
        public bool IsInPenaltyBox { get; set; } = false;
    }
}
