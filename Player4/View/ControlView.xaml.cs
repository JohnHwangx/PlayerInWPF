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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Player4.View
{
    /// <summary>
    /// ControlView.xaml 的交互逻辑
    /// </summary>
    public partial class ControlView : UserControl
    {
        public ControlView()
        {
            InitializeComponent();
        }

        private void IncreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            SliderPlay.Value += (Mouse.GetPosition((IInputElement)e.Source).X + 6.5) / (SliderPlay.ActualWidth - 14) * SliderPlay.Maximum;
            IsChangeBox.IsChecked = true;
        }

        private void DecreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            SliderPlay.Value = Mouse.GetPosition((IInputElement)e.Source).X / (SliderPlay.ActualWidth-14) * SliderPlay.Maximum;
            IsChangeBox.IsChecked = true;
        }

        private void Thumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            IsDragBox.IsChecked = true;
        }

        private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            IsDragBox.IsChecked = false;
        }
    }
}
