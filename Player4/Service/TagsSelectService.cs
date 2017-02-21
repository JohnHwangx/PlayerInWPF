//涉及歌曲列表和播放列表的显示，控制栏中歌曲信息的显示

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;

namespace Player4.Service
{
#pragma warning disable 618
    public class TagsSelectService : NotificationObject
#pragma warning restore 618
    {
		#region 属性
		public MediaElement MediaElement { get; set; }

		public SongListModel SongListModel { get; set; }
        public PlayingListModel PlayingListModel { get; set; }

        public ControlService ControlService { get; set; }
        //public PlayService PlayService { get; set; }
        private List<SongListStyle> _initialSongs;
        /// <summary> 初始歌曲样式列表 </summary>
        public List<SongListStyle> InitialSongs
        {
            get { return _initialSongs; }
            set
            {
                _initialSongs = value;
                RaisePropertyChanged("InitialSongs");
            }
        }

        private List<SongListStyle> _playListItem;
        /// <summary> 播放列表项显示 </summary>
        public List<SongListStyle> PlayListItem
        {
            get { return _playListItem; }
            set
            {
                _playListItem = value;
                RaisePropertyChanged("PlayListItem");
            }
        }
        protected bool IsPlayListChanged { get; set; }

        private Song _playingSong;
        /// <summary> 在播放类别,控制栏显示 </summary>
        public Song PlayingSong
        {
            get { return _playingSong; }
            set
            {
                //if (_playingSong != null /*&& value.Equals(_playingSong)*/)
                //{
                //    return;
                //}
                _playingSong = value;
                _playingSong.AlbumCover = value.AlbumCover ?? SongModel.GetAlbumCover(_playingSong.Path);
                ControlService.PlayService.Period = 0;
                ControlService.PlayService.Duration = GetDuration();//获取歌曲总时长
                ControlService.PlayService.SongPath = _playingSong.Path;
                if (File.Exists(_playingSong.Path))
                {
                    ControlService.PlayService.MediaPlayer.Source=(new Uri(_playingSong.Path, UriKind.Absolute));
				}
                RaisePropertyChanged("PlayingSong");
            }
        }

        private double GetDuration()
        {
            if (PlayingSong.Duration == null) return 0;
            var time = PlayingSong.Duration.Split(':');
            return int.Parse(time[0]) * 60 * 60 + int.Parse(time[1]) * 60 + int.Parse(time[2]);
        }

        #endregion

        #region 播放模式
        public PlayMode PlayMode { get; set; }
        public PlayModeService PlayModeService { get; set; }
        #endregion

        /// <summary> 初始化歌曲列表、播放列表和初始歌曲 </summary>
        private void InitialSongLists()
        {
            InitialSongs = SongListModel.InitialSongList(SongListModel.GetSongsDb());
            PlayListItem = SongListModel.InitialSongList(PlayingListModel.GetPlayingList());

            var firstOrDefault = PlayListItem.FirstOrDefault();
            if (firstOrDefault != null)
            {
                PlayingSong = firstOrDefault.Song;
                ControlService.PlayService.SongPath = PlayingSong.Path;
                ControlService.PlayState = PlayState.暂停;
            }

        }

        public DelegateCommand PlaySongCommand { get; set; }
        /// <summary> 播放选中标签的歌曲 </summary>
        private void PlaySongExecute()
        {
            PlayListItem.Clear();

            var songList = InitialSongs.Select(i => i.Song).ToList();
            PlayListItem = SongListModel.InitialSongList(songList);

            IsPlayListChanged = true;

            PlayingListModel.ClearPlayingList();
            PlayingListModel.SavePlayingList(songList);
            if (ControlService.PlayState != PlayState.播放)
            {
                ControlService.PlayState = PlayState.播放;
            }
        }

        public ICommand AddSongSetCommand { get; set; }
        /// <summary>
        /// 从目录添加歌曲
        /// </summary>
        private void OnAddSongSet()
        {
            var songList = new List<Song>();
            using (var dirChooser = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dirChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var songModel = new SongListModel();
                    songList = songModel.LoadSongList(dirChooser.SelectedPath);
                }
            }
            var initialSongs = new SongListModel();
            initialSongs.SaveSongsDb(songList);
            InitialSongs = new List<SongListStyle>(initialSongs.InitialSongList(songList));
        }

        public ICommand TestCommand { get; set; }
        private void TestExecute()
        {
            MessageBox.Show("This is Test Command");
        }

        public DelegateCommand SelectTagsCommand { get; set; }
        /// <summary>
        /// 确认选中的标签，在歌曲列表显示包含选中标签的歌曲
        /// </summary>
        private void OnSelectTags()
        {
            //Songs.SongList.Clear();
            var songList = SongListModel.GetSelectedSongs();
            InitialSongs = new List<SongListStyle>(SongListModel.InitialSongList(songList));
        }

        public DelegateCommand NextCommand { get; set; }

        protected void OnNext()
        {
            if (PlayListItem == null || PlayListItem.Count == 0)
            {
                return;
            }
            var songList = PlayListItem.Select(i => i.Song).ToList();
            var playingIndex = songList.IndexOf(PlayingSong);

            if (PlayMode == PlayMode.随机播放 && IsPlayListChanged)
            {
                PlayModeService.ChangeRandomList(songList);
                IsPlayListChanged = false;
            }
            PlayingSong = PlayModeService.OnNextExecute(PlayMode, songList, playingIndex);

            if (ControlService.PlayState != PlayState.播放)
            {
                ControlService.PlayState = PlayState.播放;
            }
            else
            {
                ControlService.PlayService.MediaPlayer.Play();
            }
        }

        public DelegateCommand LastCommand { get; set; }

        protected void OnLast()
        {
            if (PlayListItem == null || PlayListItem.Count == 0)
            {
                return;
            }
            var songList = PlayListItem.Select(i => i.Song).ToList();
            var playingIndex = songList.IndexOf(PlayingSong);

            if (PlayMode == PlayMode.随机播放 && IsPlayListChanged)
            {
                PlayModeService.ChangeRandomList(songList);
                IsPlayListChanged = false;
            }
            PlayingSong = PlayModeService.OnLastExecute(PlayMode, songList, playingIndex);

            if (ControlService.PlayState != PlayState.播放)
            {
                ControlService.PlayState = PlayState.播放;
            }
            else
            {
                ControlService.PlayService.MediaPlayer.Play();
            }
        }

        public DelegateCommand PlayPauseCommand { get; set; }

        private void OnPlayPause()
        {
            switch (ControlService.PlayState)
            {
                case PlayState.暂停:
                    ControlService.PlayState = PlayState.播放;
                    //ControlService.PlayService.Play(PlayingSong.Path);
                    break;
                case PlayState.播放:
                    ControlService.PlayState = PlayState.暂停;
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

        public DelegateCommand ModeChangeCommand { get; set; }

        private void OnModeChange()
        {
            PlayMode = PlayModeService.OnModeChange(PlayMode);
            if (PlayMode == PlayMode.随机播放)
            {
                PlayModeService.ChangeRandomList(PlayListItem.Select(i => i.Song).ToList());
            }
        }
        /// <summary>
        /// 先初始化构造函数，再初始化属性
        /// </summary>
        public TagsSelectService()
        {
            SongListModel = new SongListModel();
            PlayingListModel = new PlayingListModel();
            ControlService = new ControlService();
            PlayMode = PlayMode.列表循环;
            InitialSongs = new List<SongListStyle>();
            PlayListItem = new List<SongListStyle>();
            InitialSongLists();
            PlayModeService = new PlayModeService(PlayMode);

            ControlService.PlayService.MediaPlayer.MediaEnded += MediaEnd;

            SelectTagsCommand = new DelegateCommand(OnSelectTags);
            PlaySongCommand = new DelegateCommand(PlaySongExecute);
            AddSongSetCommand = new DelegateCommand(OnAddSongSet);
            NextCommand = new DelegateCommand(OnNext);
            LastCommand = new DelegateCommand(OnLast);
            ModeChangeCommand = new DelegateCommand(OnModeChange);
            SelectTagCommand = new DelegateCommand<Button>(SelectTagExecute);
            TestCommand = new DelegateCommand(TestExecute);
            PlayPauseCommand = new DelegateCommand(OnPlayPause);
        }

        private void MediaEnd(object sender, EventArgs e)
        {
            if (PlayListItem == null || PlayListItem.Count == 0)
            {
                return;
            }
            var songList = PlayListItem.Select(i => i.Song).ToList();
            var playingIndex = songList.IndexOf(PlayingSong);

            if (PlayMode == PlayMode.随机播放 && IsPlayListChanged)
            {
                PlayModeService.ChangeRandomList(songList);
                IsPlayListChanged = false;
            }
            PlayingSong = PlayModeService.GetNextSong(PlayMode, songList, playingIndex);
            ControlService.PlayService.MediaPlayer.Play();
        }

        public ICommand SelectTagCommand { get; set; }

        private void SelectTagExecute(Button button)
        {
            var tagModel = new TagModel();
            tagModel.SetTagModel(ref button);
        }
    }

    public class TagModel : Window
    {
        public void SetTagModel(ref Button button)
        {
            if (button.FontWeight == FontWeights.Normal)
            {
                button.FontWeight = FontWeights.Bold;
                button.Foreground = FindResource("MouseOverBrush") as SolidColorBrush;
                Songs.SelectedTags.Add(button.Content.ToString());
            }
            else if (button.FontWeight == FontWeights.Bold)
            {
                button.FontWeight = FontWeights.Normal;
                button.Foreground = FindResource("ForegroundBrush") as SolidColorBrush;
                Songs.SelectedTags.RemoveAt(Songs.SelectedTags.IndexOf(button.Content.ToString()));
            }
        }
    }
}