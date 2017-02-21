using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;

namespace Player4.Service
{
#pragma warning disable 618
    public class PlayModeService : NotificationObject
#pragma warning restore 618
    {
        #region 属性
        private Visibility _loopPlayVis = Visibility.Collapsed;

        public Visibility LoopPlayVis
        {
            get { return _loopPlayVis; }
            set
            {
                _loopPlayVis = value;
                RaisePropertyChanged("LoopPlayVis");
            }
        }
        private Visibility _singlePlayVis = Visibility.Collapsed;

        public Visibility SinglePlayVis
        {
            get { return _singlePlayVis; }
            set
            {
                _singlePlayVis = value;
                RaisePropertyChanged("SinglePlayVis");
            }
        }
        private Visibility _sequentialPlayVis = Visibility.Collapsed;

        public Visibility SequentialPlayVis
        {
            get { return _sequentialPlayVis; }
            set
            {
                _sequentialPlayVis = value;
                RaisePropertyChanged("SequentialPlayVis");
            }
        }
        private Visibility _randomPlayVis = Visibility.Collapsed;

        public Visibility RandomPlayVis
        {
            get { return _randomPlayVis; }
            set
            {
                _randomPlayVis = value;
                RaisePropertyChanged("RandomPlayVis");
            }
        }
        #endregion

        public PlayModeService(PlayMode playMode)
        {
            IniTialPlayMode(playMode);
            RandomNumList = new List<int>();
        }

        private void IniTialPlayMode(PlayMode playMode)
        {
            switch (playMode)
            {
                case PlayMode.列表循环:
                    LoopPlayVis = Visibility.Visible;
                    break;
                case PlayMode.单曲循环:
                    SinglePlayVis = Visibility.Visible;
                    break;
                case PlayMode.顺序播放:
                    SequentialPlayVis = Visibility.Visible;
                    break;
                case PlayMode.随机播放:
                    RandomPlayVis = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
            }
        }
        public List<int> RandomNumList { get; set; }
        /// <summary>
        /// 获取下一首播放的歌曲
        /// </summary>
        /// <param name="playMode">播放模式</param>
        /// <param name="playingList">歌曲列表</param>
        /// <param name="songIndex">当前歌曲索引</param>
        /// <returns>下一首歌曲</returns>
        public Song GetNextSong(PlayMode playMode, List<Song> playingList, int songIndex)
        {
            switch (playMode)
            {
                case PlayMode.列表循环:
                    return songIndex == playingList.Count - 1
                        ? playingList[0]
                        : playingList[++songIndex];
                case PlayMode.单曲循环:
                    return playingList[songIndex];
                case PlayMode.顺序播放:
                    return songIndex == playingList.Count - 1
                        ? null
                        : playingList[++songIndex];
                case PlayMode.随机播放:
                    var randomNum = RandomNumList.IndexOf(songIndex);
                    return randomNum == playingList.Count - 1
                        ? playingList[0]
                        : playingList[++randomNum];
            }
            return null;
        }

        public Song OnNextExecute(PlayMode playMode, List<Song> playingList, int songIndex)
        {
            switch (playMode)
            {
                case PlayMode.列表循环:
                case PlayMode.单曲循环:
                case PlayMode.顺序播放:
                    return songIndex == playingList.Count - 1
                        ? playingList[0]
                        : playingList[++songIndex];
                case PlayMode.随机播放:
                    var randomNum = RandomNumList.IndexOf(songIndex);
                    return randomNum == playingList.Count - 1
                        ? playingList[RandomNumList[0]]
                        : playingList[RandomNumList[++randomNum]];
                default:
                    throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
            }
        }

        public Song OnLastExecute(PlayMode playMode, List<Song> playingList, int songIndex)
        {
            switch (playMode)
            {
                case PlayMode.列表循环:
                case PlayMode.单曲循环:
                case PlayMode.顺序播放:
                    return songIndex == 0 ? playingList[playingList.Count - 1] : playingList[--songIndex];
                case PlayMode.随机播放:
                    var randomNum = RandomNumList.IndexOf(songIndex);
                    return randomNum == 0
                        ? playingList[RandomNumList[playingList.Count - 1]]
                        : playingList[RandomNumList[--randomNum]];
                default:
                    throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
            }
        }

        public List<int> GetRandomList(List<Song> playingList)
        {
            var randomNumList = new int[playingList.Count];
            for (int i = 0; i < playingList.Count; i++)
            {
                randomNumList[i] = i;
            }

            Random randomNumbers = new Random();
            for (int i = 0; i < playingList.Count; i++)
            {
                int randomNum = randomNumbers.Next(playingList.Count);
                var temp = randomNumList[i];
                randomNumList[i]=randomNumList[randomNum];
                randomNumList[randomNum] = temp;
            }
            return randomNumList.ToList();
        }

        public void ChangeRandomList(List<Song> playingList)
        {
            RandomNumList = GetRandomList(playingList);
        }

        public PlayMode OnModeChange(PlayMode playMode)
        {
            switch (playMode)
            {
                case PlayMode.列表循环:
                    LoopPlayVis = Visibility.Collapsed;
                    SinglePlayVis = Visibility.Visible;
                    SequentialPlayVis = Visibility.Collapsed;
                    RandomPlayVis = Visibility.Collapsed;
                    return PlayMode.单曲循环;
                case PlayMode.单曲循环:
                    LoopPlayVis = Visibility.Collapsed;
                    SinglePlayVis = Visibility.Collapsed;
                    SequentialPlayVis = Visibility.Visible;
                    RandomPlayVis = Visibility.Collapsed;
                    return PlayMode.顺序播放;
                case PlayMode.顺序播放:
                    LoopPlayVis = Visibility.Collapsed;
                    SinglePlayVis = Visibility.Collapsed;
                    SequentialPlayVis = Visibility.Collapsed;
                    RandomPlayVis = Visibility.Visible;
                    return PlayMode.随机播放;
                case PlayMode.随机播放:
                    LoopPlayVis = Visibility.Visible;
                    SinglePlayVis = Visibility.Collapsed;
                    SequentialPlayVis = Visibility.Collapsed;
                    RandomPlayVis = Visibility.Collapsed;
                    return PlayMode.列表循环;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playMode), playMode, null);
            }
        }
    }
}
