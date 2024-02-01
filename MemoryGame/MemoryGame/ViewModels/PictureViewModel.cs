using MemoryGame.Models;
using System.Windows.Media;

namespace MemoryGame.ViewModels
{
    public class PictureViewModel : ObservableObject
    {
        private PictureModel _model;
        public int Id { get; private set; }

        private bool _isViewed;
        private bool _isMatched;
        private bool _isFailed;

        public bool isViewed
        {
            get
            {
                return _isViewed;
            }
            private set
            {
                _isViewed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        public bool isMatched
        {
            get
            {
                return _isMatched;
            }
            private set
            {
                _isMatched = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        public bool isFailed
        {
            get
            {
                return _isFailed;
            }
            private set
            {
                _isFailed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
            }
        }

        public bool isSelectable
        {
            get
            {
                if (isMatched || isViewed)
                    return false;
                return true;
            }
        }

        public string SlideImage
        {
            get
            {
                if (isMatched)
                    return _model.ImageSource;
                if (isViewed)
                    return _model.ImageSource;


                return "/MemoryGame;component/Assets/mystery_box.png";
            }
        }
        public Brush BorderBrush
        {
            get
            {
                if (isFailed)
                    return Brushes.Red;
                if (isMatched)
                    return Brushes.Green;
                if (isViewed)
                    return Brushes.Yellow;

                return Brushes.Black;
            }
        }

        public PictureViewModel(PictureModel model)
        {
            _model = model;
            Id = model.Id;
        }
        public void MarkMatched()
        {
            isMatched = true;
        }

        public void MarkFailed()
        {
            isFailed = true;
        }

        public void ClosePeek()
        {
            isViewed = false;
            isFailed = false;
            OnPropertyChanged("isSelectable");
            OnPropertyChanged("SlideImage");
        }

        public void PeekAtImage()
        {
            isViewed = true;
            OnPropertyChanged("SlideImage");
        }
    }
}
