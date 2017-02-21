using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Service;
using Player4.Model;
using Player4.View;

namespace Player4.ViewModel
{
#pragma warning disable 618
    public class MainWindowViewModel : NotificationObject
#pragma warning restore 618
    {
        //public PlayService PlayService { get; set; }
        //控制区
        //private ControlService _controlService;
        //public ControlService ControlService
        //{
        //    get { return _controlService; }
        //    set
        //    {
        //        _controlService = value;
        //        RaisePropertyChanged("ControlService");
        //    }
        //}
        //歌曲列表区
        private SongListService _songListService;
        public SongListService SongListService
        {
            get { return _songListService; }
            set
            {
                _songListService = value;
                RaisePropertyChanged("SongListService");
            }
        }
        //标题栏区
        private TitleBarService _titleBarService;
        public TitleBarService TitleBarService
        {
            get { return _titleBarService; }
            set
            {
                _titleBarService = value;
                RaisePropertyChanged("TitleBarService");
            }
        }
        //标签选择区
        private TagsSelectService _tagsSelectService;
        public TagsSelectService TagsSelectService
        {
            get { return _tagsSelectService; }
            set
            {
                _tagsSelectService = value;
                RaisePropertyChanged("SongListService");
            }
        }

        private VisibilityService _visibilityService;

        public VisibilityService VisibilityService
        {
            get { return _visibilityService; }
            set
            {
                _visibilityService = value; 
                RaisePropertyChanged("VisibilityService");
            }
        }


        public MainWindowViewModel(MediaElement mediaElement)
        {
            Songs.SongTags = new List<string>();
            //Songs.PlayingSong = new Song();
            //Songs.SongList = new List<Song>();
            Songs.SelectedTags = new List<string>();
            //Songs.PlayingSongs = new List<Song>();


            //ControlService = new ControlService();
            //TagsSelectService = new TagsSelectService();
            TitleBarService = new TitleBarService();
            SongListService = new SongListService(mediaElement);
            VisibilityService = new VisibilityService();
            //PlayService = new PlayService();
        }
    }
}