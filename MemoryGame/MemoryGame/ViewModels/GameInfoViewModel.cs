using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryGame.ViewModels
{
    public class GameInfoViewModel : ObservableObject
    {
        private const int _maxAttempts = 6;
        private const int _pointAward = 75;
        private const int _pointDeduction = 15;

        private int _matchAttempts;
        private int _score;

        private bool _gameLost = false;
        private bool _gameWon = false;

        public int MatchAttempts
        {
            get
            {
                return _matchAttempts;
            }
            private set
            {
                _matchAttempts = value;
                OnPropertyChanged("MatchAttempts");
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        public Visibility LostMessage
        {
            get
            {
                return _gameLost ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility WinMessage
        {
            get
            {
                return _gameWon ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public void WinStatus(bool win)
        {
            if (!win)
            {
                _gameLost = true;
                OnPropertyChanged("LostMessage");
            }
            else
            {
                _gameWon = true;
                OnPropertyChanged("WinMessage");
            }
        }

        public void ClearInfo()
        {
            Score = 0;
            MatchAttempts = _maxAttempts;
            _gameLost = false;
            _gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }

        public void Award()
        {
            Score += _pointAward;
        }

        public void Penalize()
        {
            Score -= _pointDeduction;
            MatchAttempts--;
        }
    }
}
