using System.Collections.Generic;

namespace Player4.Model
{
    public class Songs
    {
        //public static Song PlayingSong = new Song();
        //public static List<Song> SongList = new List<Song>();
        //public static List<string> SongTags = new List<string>();
        //public static List<string> SelectedTags = new List<string>();

        //public static Song PlayingSong { get; set; }
        /// <summary>
        /// 导入的目录中的所有曲目
        /// </summary>
        //public static List<Song> SongList { get; set; }
        /// <summary>
        /// 暂时存放选中歌曲的标签
        /// </summary>
        public static List<string> SongTags { get; set; }
        /// <summary>
        /// 标签栏中选中的标签
        /// </summary>
        public static List<string> SelectedTags { get; set; }
        ///// <summary>
        ///// 包含指定标签的歌曲
        ///// </summary>
        //public static List<Song> SelectedSongs { get; set; }
        //public static List<Song> PlayingSongs { get; set; }
        //public static List<string> AllTagList { get; set; }
    }
}