using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Trivia
{
    public class Player : INotifyPropertyChanged
    {
        public const int NumberOfPlaces = 11;

        public Player(string name) => Name = name;

        public string Name { get; }

        public int Place
        {
            get => _Place;
            set
            {
                if (value > NumberOfPlaces)
                {
                    value -= NumberOfPlaces + 1;
                }
                _Place = value;
            }
        }
        private int _Place = 0;

        public int Purse { get; set; } = 0;
        
        public bool IsInPenaltyBox { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
