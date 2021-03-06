﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Player4.Model;

namespace Player4.View
{
    /// <summary>
    /// TagsEditingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TagsEditingWindow : Window
    {
        public TagsEditingWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //Songs.SongTags = new List<string>();
            var tag = (Button)e.OriginalSource;
            if (tag.FontWeight == FontWeights.Normal)
            {
                tag.FontWeight = FontWeights.Bold;
                tag.Foreground = FindResource("MouseOverBrush") as SolidColorBrush;
                Songs.SongTags.Add(tag.Content.ToString());
            }
            else if (tag.FontWeight == FontWeights.Bold)
            {
                tag.FontWeight = FontWeights.Normal;
                tag.Foreground = FindResource("ForegroundBrush") as SolidColorBrush;
                Songs.SongTags.RemoveAt(Songs.SongTags.IndexOf(tag.Content.ToString()));
            }
        }
    }
}
