using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player4.Model
{
    /// <summary>
    /// 播放列表数据库操作
    /// </summary>
    public class PlayingListModel:DbOperator
    {
        private const string DbName = "PlayerDb";
        private const string TableName = "PlayingList";
        /// <summary>
        /// 从播放列表数据库读取
        /// </summary>
        public List<Song> GetPlayingList()
        {
            var playingList=new List<Song>();
            if (!IsExistDb(DbName)||!IsExistTable(TableName))
            {
                return playingList;
            }
            var selectSql = $"select * from {TableName}";
            var dataReader = TableDataReader(DbName, selectSql);
            var songPathList = new List<string>();
            while (dataReader.Read())
            {
                songPathList.Add(dataReader[0].ToString().Trim());
            }
            dataReader.Dispose();
            foreach (var songPath in songPathList)
            {
                var songListModel = new SongListModel();
                var song = songListModel.GetPlayingSong(songPath);
                playingList.Add(song);
            }
            return playingList;
        }
        /// <summary>
        /// 将播放列表存入数据库
        /// </summary>
        public void SavePlayingList(List<Song> playingList)
        {
            if (!IsExistDb(DbName))
            {
                return;
            }
            if (!IsExistTable(TableName))//无表建表
            {
                string createTableSql = $"path nvarchar(400) primary key,title text,artist text,duration text";
                CreateTable(DbName,TableName,createTableSql);
            }
            else//有表清表
            {
                ClearTable(DbName, TableName);
            }
            var columnSql = "path,title,artist,duration";
            foreach (var playingSong in playingList)
            {
                var insertSql= "'" + EscConvertor(playingSong.Path) + "','" + EscConvertor(playingSong.Title) + "','" + EscConvertor(playingSong.Artist) + "','"  +
                                playingSong.Duration + "'";
                InsertTable(DbName, TableName, columnSql, insertSql);
            }
        }
        /// <summary>
        /// 清空播放列表数据库
        /// </summary>
        public void ClearPlayingList(List<Song> playingList)
        {
            if (!IsExistDb(DbName)||!IsExistTable(TableName))
            {
                return;
            }
            playingList.Clear();
            ClearTable(DbName,TableName);
        }
        public void ClearPlayingList()
        {
            if (!IsExistDb(DbName) || !IsExistTable(TableName))
            {
                return;
            }
            ClearTable(DbName, TableName);
        }
    }
}
