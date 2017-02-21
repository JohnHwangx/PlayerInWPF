using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using WPFTest.ListBoxConvert;

namespace WPFTest.MediaElement
{
	public class MediaElemViewModel : BindableBase
	{
		public System.Windows.Controls.MediaElement MediaElement { get; set; }

		private string _mediaSource;

		public string MediaSource
		{
			get { return _mediaSource; }
			set
			{
				_mediaSource = value;
				OnPropertyChanged("MediaSource");
			}
		}

		public ICommand ClickCommand { get; set; }

		private void OnClickExecute()
		{
			using (var dirChooser = new System.Windows.Forms.OpenFileDialog())
			{
				if (dirChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					//var songModel = new SongListModel();
					//songList = songModel.LoadSongList(dirChooser.SelectedPath);
					var path = dirChooser.FileName;
					//MessageBox.Show(path);
					SetMediaElement(path);
				}
			}
		}

		public string OnClick()
		{
			using (var dirChooser = new System.Windows.Forms.OpenFileDialog())
			{
				if (dirChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					return dirChooser.FileName;
				}
				return null;
			}
		}

		private void SetMediaElement(string path)
		{
			//var mediaElem = new System.Windows.Controls.MediaElement();
			//mediaElem.LoadedBehavior = MediaState.Manual;
			//mediaElem.Name = "mediaElement";

			//var mediaView=new MediaView();
			//var mediaElem = mediaView.MediaElement;
			MediaElement.LoadedBehavior = MediaState.Manual;
			MediaElement.Source = new Uri(path);
			MediaElement.Play();
		}

		public MediaElemViewModel(System.Windows.Controls.MediaElement mediaElement)
		{
			MediaElement = mediaElement;
			ClickCommand=new DelegateCommand(OnClickExecute);
		}
	}
}
