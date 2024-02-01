using MemoryGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;

namespace MemoryGame.ViewModels
{
    public class SlideCollectionViewModel : ObservableObject
    {
        public ObservableCollection<PictureViewModel> MemorySlides { get; private set; }

        private PictureViewModel SelectedSlide1;
        private PictureViewModel SelectedSlide2;

        private DispatcherTimer _peekTimer;
        private DispatcherTimer _openingTimer;

        private const int _peekMiliseconds = 500;
        private const int _openSeconds = 3;

        public bool areSlidesActive
        {
            get
            {
                return SelectedSlide1 == null || SelectedSlide2 == null;
            }
        }

        public bool allSlidesMatched
        {
            get
            {
                foreach(var slide in MemorySlides)
                {
                    if (!slide.isMatched)
                        return false;
                }

                return true;
            }
        }

        public bool canSelect { get; private set; }

        public SlideCollectionViewModel()
        {
            _peekTimer = new DispatcherTimer();
            _peekTimer.Interval = new TimeSpan(0, 0, 0, 0, _peekMiliseconds);
            _peekTimer.Tick += PeekTimer_Tick;

            _openingTimer = new DispatcherTimer();
            _openingTimer.Interval = new TimeSpan(0, 0, _openSeconds);
            _openingTimer.Tick += OpeningTimer_Tick;
        }

        public void CreateSlides(string imagesPath)
        {
            MemorySlides = new ObservableCollection<PictureViewModel>();
            var models = GetModelsFrom(@imagesPath);

            for (int i = 0; i < 6; i++)
            {
                var newSlide = new PictureViewModel(models[i]);
                var newSlideMatch = new PictureViewModel(models[i]);
                MemorySlides.Add(newSlide);
                MemorySlides.Add(newSlideMatch);
                newSlide.PeekAtImage();
                newSlideMatch.PeekAtImage();
            }
            ShuffleSlides();
            OnPropertyChanged("MemorySlides");
            _openingTimer.Start();
        }

        public void SelectSlide(PictureViewModel slide)
        {
            slide.PeekAtImage();

            if (SelectedSlide1 == null)
            {
                SelectedSlide1 = slide;
            }
            else if (SelectedSlide2 == null && SelectedSlide1 != slide)
            {
                SelectedSlide2 = slide;
                _peekTimer.Start();
            }

            SoundManager.PlayCardFlip();
            OnPropertyChanged("areSlidesActive");
        }

        public bool CheckIfMatched()
        {
            if (SelectedSlide1.Id == SelectedSlide2.Id)
            {
                SelectedSlide1.MarkMatched();
                SelectedSlide2.MarkMatched();
                ClearSelected();
                return true;
            }
            else
            {
                SelectedSlide1.MarkFailed();
                SelectedSlide2.MarkFailed();
                ClearSelected();
                return false;
            }
        }

        private void ClearSelected()
        {
            SelectedSlide1 = null;
            SelectedSlide2 = null;
            canSelect = false;
        }

        public void RevealUnmatched()
        {
            foreach(var slide in MemorySlides)
            {
                if(!slide.isMatched)
                {
                    _peekTimer.Stop();
                    slide.MarkFailed();
                    slide.PeekAtImage();
                }
            }
        }

        private List<PictureModel> GetModelsFrom(string relativePath)
        {
            var models = new List<PictureModel>();
            var images = Directory.GetFiles(@relativePath, "*.jpg", SearchOption.TopDirectoryOnly);
            var id = 0;

            foreach (string i in images)
            {
                models.Add(new PictureModel() { Id = id, ImageSource = "/MemoryGame;component/" + i });
                id++;
            }

            return models;
        }

        private void ShuffleSlides()
        {
            var random = new Random();
            for (int i = 0; i < 64; i++)
            {
                MemorySlides.Reverse();
                MemorySlides.Move(random.Next(0, MemorySlides.Count), random.Next(0, MemorySlides.Count));
            }
        }

        private void OpeningTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                slide.ClosePeek();
                canSelect = true;
            }
            OnPropertyChanged("areSlidesActive");
            _openingTimer.Stop();
        }

        private void PeekTimer_Tick(object sender, EventArgs e)
        {
            foreach(var slide in MemorySlides)
            {
                if(!slide.isMatched)
                {
                    slide.ClosePeek();
                    canSelect = true;
                }
            }
            OnPropertyChanged("areSlidesActive");
            _peekTimer.Stop();
        }
    }
}
