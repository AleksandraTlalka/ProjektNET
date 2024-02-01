using System;


namespace MemoryGame.ViewModels
{
    public class GameViewModel : ObservableObject
    {
        public SlideCollectionViewModel Slides { get; private set; }
        public GameInfoViewModel GameInfo { get; private set; }
        public TimerViewModel Timer { get; private set; }

        public GameViewModel()
        {
            SetupGame();
        }

        private void SetupGame()
        {

            Slides = new SlideCollectionViewModel();
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1));
            GameInfo = new GameInfoViewModel();

            GameInfo.ClearInfo();
            Slides.CreateSlides("Assets/Cosmos");
            Timer.Start();

            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        public void ClickedSlide(object slide)
        {
            if(Slides.canSelect)
            {
                var selected = slide as PictureViewModel;
                if(selected.isSelectable)
                    Slides.SelectSlide(selected);
            }

            if(!Slides.areSlidesActive)
            {
                if (Slides.CheckIfMatched())
                    GameInfo.Award();
                else
                    GameInfo.Penalize();
            }

            GameStatus();
        }

        private void GameStatus()
        {
            if(GameInfo.MatchAttempts < 0)
            {
                GameInfo.WinStatus(false);
                Slides.RevealUnmatched();
                Timer.Stop();
            }

            if(Slides.allSlidesMatched)
            {
                GameInfo.WinStatus(true);
                Timer.Stop();
            }
        }

        public void Restart()
        {
            SetupGame();
        }
    }
}
