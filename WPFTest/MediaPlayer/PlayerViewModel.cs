using System;
using System.Windows.Input;

namespace WPFTest.MediaPlayer
{
    public class PlayerViewModel
    {
        public System.Windows.Media.MediaPlayer MediaPlayer { get; set; }
        public ICommand PlayCommand { get; set; }

        private void PlayExecute()
        {
            MediaPlayer.Open(new Uri(@"C:\Users\john\Music\~memory~-久石譲.mp3"));
            MediaPlayer.Play();
        }

        public PlayerViewModel()
        {
            MediaPlayer = new System.Windows.Media.MediaPlayer();
            PlayCommand=new DelegateCommand(PlayExecute);
        }
    }
}