using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;
using Player4.View;
using Player4.ViewModel;

namespace Player4.Service
{
	public class SongListService : TagsSelectService
	{
		public DelegateCommand<ListBox> SongListEditTagsMenuCommand { get; set; }

		/// <summary>
		/// 编辑歌曲标签按钮命令
		/// </summary>
		/// <param name="listBox"></param>
		private void SongListEditTagsMenuExecute(ListBox listBox)
		{
			var songListItem = listBox.SelectedItem as SongListStyle;
			if (songListItem == null) return;
			var song = SongListModel.GetPlayingSong(songListItem.Song.Path);
			TagsEditingWindow tagsEditingWindow = new TagsEditingWindow();

			//TagsEditingWindowViewModel.TagsList =
			//    songListStyle?.Song.Tags != null
			//        ? new List<string>(songListStyle.Song.Tags)
			//        : new List<string>();

			Songs.SongTags =
				song.Tags != null
					? new List<string>(song.Tags)
					: new List<string>();
			TagsEditingWindowViewModel tagsEditingWindowViewModel = new TagsEditingWindowViewModel();
			tagsEditingWindow.DataContext = tagsEditingWindowViewModel;
			tagsEditingWindow.ShowDialog();

			if (tagsEditingWindow.DialogResult != true) return;
			SongListModel.ClearSongTags(songListItem.Song);//将数据库中该歌曲的标签清空
			songListItem.Song.Tags = new List<string>(Songs.SongTags/*TagsEditingWindowViewModel.TagsList*/);
			SongListModel.SaveSongTags(songListItem.Song);
		}

		public DelegateCommand<ListBox> DouClickSongItemCommand { get; set; }
		/// <summary>
		/// 双击歌曲项或右键点击播放
		/// </summary>
		/// <param name="listBox"></param>
		private void DouClickSongItemExecute(Selector listBox)//由于PlayListItem在子类中无法改变，故放于此
		{
			var songListItem = listBox.SelectedItem as SongListStyle;
			if (songListItem != null)
			{
				var targetSong = SongListModel.GetPlayingSong(songListItem.Song.Path);
				if (!targetSong.Equals(PlayingSong))
				{
					PlayingSong = targetSong;
				}
				//PlayingSong = SongListModel.GetPlayingSong(songListItem.Song.Path);
			}

			PlayListItem.Clear();
			PlayListItem = new List<SongListStyle>(InitialSongs);

			IsPlayListChanged = true;//播放列表被修改

			PlayingListModel.ClearPlayingList();
			PlayingListModel.SavePlayingList(PlayListItem.Select(i => i.Song).ToList());
			if (ControlService.PlayState != PlayState.播放)
			{
				ControlService.PlayState = PlayState.播放;
			}
			else
			{
				ControlService.PlayService.MediaPlayer.Play();
			}
		}

		public ICommand AddPlayListCommand { get; set; }

		private void OnAddPlayList(ListBox listBox)
		{
			var songList = PlayListItem.Select(i => i.Song).ToList();
			var songListItem = listBox.SelectedItem as SongListStyle;
			if (songListItem == null) return;
			songList.Add(songListItem.Song);
			PlayListItem = SongListModel.InitialSongList(songList);

			IsPlayListChanged = true;

			PlayingListModel.ClearPlayingList();
			PlayingListModel.SavePlayingList(songList);
		}

		public DelegateCommand<ListBox> DoubleClickPlayingListCommand { get; set; }
		/// <summary>
		/// 双击播放列表歌曲项或右键点击播放
		/// </summary>
		/// <param name="playingList"></param>
		private void OnDoubleClickPlayingList(ListBox playingList)
		{
			var songListItem = playingList.SelectedItem as SongListStyle;
			if (songListItem == null) return;
			PlayingSong = SongListModel.GetPlayingSong(songListItem.Song.Path);
			if (ControlService.PlayState != PlayState.播放)
			{
				ControlService.PlayState = PlayState.播放;
			}
		}

		public ICommand DelPlayListCommand { get; set; }

		private void OnDelPlayList(ListBox listBox)
		{
			var songListItem = listBox.SelectedItem as SongListStyle;
			if (songListItem == null) return;
			var delSong = SongListModel.GetPlayingSong(songListItem.Song.Path);
			var songList = PlayListItem.Select(i => i.Song).ToList();

			songList.Remove(delSong);
			PlayListItem = SongListModel.InitialSongList(songList);

			IsPlayListChanged = true;

			PlayingListModel.ClearPlayingList();
			PlayingListModel.SavePlayingList(songList);

			//若删除的歌曲为正在播放的歌曲
			if (delSong.Equals(PlayingSong))
			{
				OnNext();//播放下一曲
			}
		}

		public ICommand ClearCommand { get; set; }

		private void ClearExecute()
		{
			PlayListItem = new List<SongListStyle>();
			PlayingSong = new Song();
			PlayingListModel.ClearPlayingList();
		}

		public SongListService(MediaElement mediaElement)
		{
			MediaElement = mediaElement;
			DouClickSongItemCommand = new DelegateCommand<ListBox>(DouClickSongItemExecute);
			SongListEditTagsMenuCommand = new DelegateCommand<ListBox>(SongListEditTagsMenuExecute);
			AddPlayListCommand = new DelegateCommand<ListBox>(OnAddPlayList);
			DoubleClickPlayingListCommand = new DelegateCommand<ListBox>(OnDoubleClickPlayingList);
			DelPlayListCommand = new DelegateCommand<ListBox>(OnDelPlayList);
			ClearCommand = new DelegateCommand(ClearExecute);
		}
	}
}