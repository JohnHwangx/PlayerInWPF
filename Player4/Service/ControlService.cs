using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;

namespace Player4.Service
{
#pragma warning disable 618
    public class ControlService : NotificationObject
#pragma warning restore 618
    {
        //public Song PlayingSong { get; set; }
        public PlayService PlayService { get; set; }

        private PlayState _playState;

        public PlayState PlayState
        {
            get { return _playState; }
            set
            {
                _playState = value;
                switch (_playState)
                {
                    case PlayState.播放:
                        PlayPauseToolTip = "暂停";
                        PlayVis = Visibility.Collapsed;
                        PauseVis = Visibility.Visible;
                        PlayService.PlayState = _playState;
                        PlayService.MediaPlayer.Play();
                        PlayService.Timer.Start();
                        break;
                    case PlayState.暂停:
                        PlayPauseToolTip = "播放";
                        PlayVis = Visibility.Visible;
                        PauseVis = Visibility.Collapsed;
                        PlayService.PlayState = _playState;
                        PlayService.MediaPlayer.Pause();
                        break;
                    case PlayState.无文件:
                        break;
                    case PlayState.无列表:
                        break;
                    case PlayState.停止:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        private string _playPauseToolTip = "播放";

        public string PlayPauseToolTip
        {
            get { return _playPauseToolTip; }
            set
            {
                _playPauseToolTip = value; 
                RaisePropertyChanged("PlayPauseToolTip");
            }
        }

        private Visibility _playVis;

        public Visibility PlayVis
        {
            get { return _playVis; }
            set
            {
                _playVis = value;
                RaisePropertyChanged("PlayVis");
            }
        }

        private Visibility _pauseVis;

        public Visibility PauseVis
        {
            get { return _pauseVis; }
            set
            {
                _pauseVis = value;
                RaisePropertyChanged("PauseVis");
            }
        }

        public ControlService()
        {
            PlayState=PlayState.停止;
            PlayVis = Visibility.Visible;
            PauseVis=Visibility.Collapsed;

            PlayService = new PlayService();
        }
    }
}