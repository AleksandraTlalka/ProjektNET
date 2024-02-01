using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MemoryGame
{
    public static class SoundManager
    {
        private static MediaPlayer _mediaPlayer = new MediaPlayer();
        private static MediaPlayer _effectPlayer = new MediaPlayer();

        public static void PlayBackgroundMusic()
        {
            _mediaPlayer.Open(new Uri(Path.Combine(Environment.CurrentDirectory, "Assets/background_music.mp3")));
            _mediaPlayer.Play();
        }

        public static void PlayCardFlip()
        {
            _effectPlayer.Open(new Uri(Path.Combine(Environment.CurrentDirectory, "Assets/SoundEffects/card_flip.mp3")));
            _effectPlayer.Play();
        }
    }
}
