using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Trivia
{
    public class Player : INotifyPropertyChanged
    {
        public const int NumberOfLocations = 11;

        public Player(string name) => Name = name;

        public string Name { get; }

        public int Location
        {
            get => _Location;
            set
            {
                if (value > NumberOfLocations)
                {
                    value -= NumberOfLocations + 1;
                }
                _Location = value;
            }
        }
        private int _Location = 0;

        public int Purse { get; set; } = 0;
        
        public bool IsInPenaltyBox { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
