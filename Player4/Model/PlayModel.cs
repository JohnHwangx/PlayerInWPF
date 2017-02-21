using System;
using System.Windows.Media;

namespace Player4.Model
{
    /// <summary>
    /// 歌曲播放模块
    /// </summary>
    public class PlayModel
    {
        private MediaPlayer MediaPlayer { get; set; }

        /// <summary>
        /// 启动播放
        /// </summary>
        /// <param name="songPath"></param>
        public void Start(string songPath)
        {
            if (songPath != null)
            {
                MediaPlayer = new MediaPlayer();
                MediaPlayer.Open(new Uri(songPath));
                MediaPlayer.Play();
            }

        }


    }
}