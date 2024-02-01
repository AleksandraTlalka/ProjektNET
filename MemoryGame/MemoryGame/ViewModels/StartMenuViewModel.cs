using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.ViewModels
{
    public class StartMenuViewModel
    {
        private MainWindow _mainWindow;
        public StartMenuViewModel(MainWindow main)
        {
            _mainWindow = main;
            SoundManager.PlayBackgroundMusic();
        }

        public void StartNewGame()
        {
            GameViewModel newGame = new GameViewModel();
            _mainWindow.DataContext = newGame;
        }

        public void QuitGame() 
        {
            _mainWindow.Close();
        }
    }
}
