using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Player4.Service
{
#pragma warning disable 618
    public class VisibilityService:NotificationObject
#pragma warning restore 618
    {
        private Visibility _mnitPlayVisibility;
        /// <summary>
        /// 歌曲列表右键菜单——播放按钮
        /// </summary>
        public Visibility MnitPlayVisibility
        {
            get { return _mnitPlayVisibility; }
            set
            {
                _mnitPlayVisibility = value;
                RaisePropertyChanged("MnitPlayVisibility");
            }
        }

        private Visibility _mnitEditTagsVis;
        /// <summary>
        /// 歌曲列表右键菜单——编辑标签按钮
        /// </summary>
        public Visibility MnitEditTagsVis
        {
            get { return _mnitEditTagsVis; }
            set
            {
                _mnitEditTagsVis = value;
                RaisePropertyChanged("MnitEditTagsVis");
            }
        }
        private Visibility _mnitAddToPlayListVis;
        /// <summary>
        /// 歌曲列表右键菜单——添加播放列表按钮
        /// </summary>
        public Visibility MnitAddToPlayListVis
        {
            get { return _mnitAddToPlayListVis; }
            set
            {
                _mnitAddToPlayListVis = value;
                RaisePropertyChanged("MnitAddToPlayListVis");
            }
        }

        public DelegateCommand<ListBox> SongListMenuItemCommand { get; set; }
        /// <summary>
        /// 显示歌曲列表右键菜单
        /// </summary>
        private void SongListMenuItemExecute(ListBox obj)
        {
            if (obj.SelectedItems.Count > 1)
            {
                MnitEditTagsVis = Visibility.Collapsed;
                MnitAddToPlayListVis = Visibility.Visible;
                MnitPlayVisibility = Visibility.Visible;
            }
            else if (obj.SelectedItems.Count == 1)
            {
                MnitEditTagsVis = Visibility.Visible;
                MnitAddToPlayListVis = Visibility.Visible;
                MnitPlayVisibility = Visibility.Visible;
            }
            else
            {
                MnitEditTagsVis = Visibility.Collapsed;
                MnitAddToPlayListVis = Visibility.Collapsed;
                MnitPlayVisibility = Visibility.Collapsed;
            }
        }

        private Visibility _playListVisibility = Visibility.Collapsed;

        public Visibility PlayListVisibility
        {
            get { return _playListVisibility; }
            set
            {
                _playListVisibility = value;
                RaisePropertyChanged("PlayListVisibility");
            }
        }

        public DelegateCommand ShowPlayListCommand { get; set; }
        /// <summary>
        /// 显示播放列表
        /// </summary>
        private void ShowPlayListExecute()
        {
            switch (PlayListVisibility)
            {
                case Visibility.Collapsed:
                    PlayListVisibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    PlayListVisibility = Visibility.Collapsed;
                    break;
            }
        }

        public ICommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            PlayListVisibility = Visibility.Collapsed;
        }

        public VisibilityService()
        {
            ShowPlayListCommand=new DelegateCommand(ShowPlayListExecute);
            SongListMenuItemCommand = new DelegateCommand<ListBox>(SongListMenuItemExecute);
            CancelCommand=new DelegateCommand(CancelExecute);
        }
    }
}
