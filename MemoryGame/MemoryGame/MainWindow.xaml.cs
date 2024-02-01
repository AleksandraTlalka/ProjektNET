using MemoryGame.ViewModels;
using System;
using System.Windows;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StartMenuViewModel(this);
        }
    }
}
