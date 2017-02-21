using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;
using MessageBox = System.Windows.MessageBox;
using Timer = System.Timers.Timer;

namespace Player4.Service
{
	public class PlayService : Window, INotifyPropertyChanged
	{
		public string SongPath { get; set; }
		private bool _isChanged;

		public bool IsChanged
		{
			get { return _isChanged; }
			set
			{
				_isChanged = value;
				if (_isChanged)
				{
					OnChangePlayTime();
					_isChanged = false;
				}
				OnPropertyChanged();
			}
		}

		private bool _isDrag;

		public bool IsDrag
		{
			get { return _isDrag; }
			set
			{
				_isDrag = value;
				if (!_isDrag)
				{
					OnChangePlayTime();
					//    _isDrag = false;
				}
				OnPropertyChanged();
			}
		}


		private double _period;
		/// <summary> 当前进度,对应Slider的Value </summary>
		public double Period
		{
			get { return _period; }
			set
			{
				_period = value;
				OnPeriodChanged(_period);
				OnPropertyChanged();
			}
		}

		private void OnPeriodChanged(double period)
		{
			Rate = GetSongDuration(period);
		}

		private string _rate;
		/// <summary> 用于显示的时长 </summary>
		public string Rate
		{
			get { return _rate; }
			set
			{
				_rate = value;
				OnPropertyChanged();
			}
		}
		/// <summary>
		/// 根据进度条值计算歌曲进度，以mm:ss格式显示
		/// </summary>
		private string GetSongDuration(double period)
		{
			var songDuration = period / 500.0 * Duration;
			return ((int)songDuration / 60 < 10 ? "0" + (int)songDuration / 60 : ((int)songDuration / 60).ToString()) +
				   " : " +
				   ((int)songDuration % 60 < 10 ? "0" + (int)songDuration % 60 : ((int)songDuration % 60).ToString());
		}

		public PlayState PlayState { get; set; }
		public Timer Timer { get; set; }
		public MediaElement MediaPlayer { get; set; }
		/// <summary> 播放歌曲的总时长 </summary>
		public double Duration { get; set; }

		private delegate void MethodDelegate();

		public PlayService()
		{
			Timer = new Timer();
			Timer.Elapsed += Timer_Elapsed;
			Timer.Interval = 100;
			Timer.Start();

			MediaPlayer = new MediaElement();
			//MediaPlayer.LoadedBehavior = MediaState.Manual;
			//MediaPlayer.UnloadedBehavior = MediaState.Pause;
			//MediaPlayer.MediaEnded += Player_Ended;
		}

		private void OnChangePlayTime()
		{
			MediaPlayer.Position = TimeSpan.FromSeconds(Period / 500.0 * Duration);
		}

		public void Play()
		{
			MediaPlayer.Play();
		}

		private void Player_Ended(object sender, EventArgs e)
		{
			MessageBox.Show("Song end");
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (PlayState == PlayState.播放)
			{
				Dispatcher.Invoke(new MethodDelegate(SetPrograssBar));
			}
		}

		private void SetPrograssBar()
		{
			if (!IsDrag)
			{
				int second = (int)MediaPlayer.Position.TotalSeconds % 60;
				int minute = (int)MediaPlayer.Position.TotalMinutes;
				Rate = (minute < 10 ? "0" + minute : minute.ToString()) + " : " +
					   (second < 10 ? "0" + second : second.ToString());
				Period = MediaPlayer.Position.TotalSeconds / Duration * 500.0;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
