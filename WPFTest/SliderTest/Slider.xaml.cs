using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest.SliderTest
{
    /// <summary>
    /// Slider.xaml 的交互逻辑
    /// </summary>
    public partial class Slider : UserControl
    {
        public Slider()
        {
            InitializeComponent();
        }
#region Slider
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            SliderPlay.Value += Mouse.GetPosition((IInputElement)e.Source).X / SliderPlay.ActualWidth * SliderPlay.Maximum;
        }

        private void RepeatButton_Click_1(object sender, RoutedEventArgs e)
        {
            SliderPlay.Value = Mouse.GetPosition((IInputElement)e.Source).X / SliderPlay.ActualWidth * SliderPlay.Maximum;
        }

        private void RepeatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SliderPlay.Value += Mouse.GetPosition((IInputElement)sender).X / SliderPlay.ActualWidth * SliderPlay.Maximum;
        }
        private void RepeatButton_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SliderPlay.Value = Mouse.GetPosition((IInputElement)sender).X / SliderPlay.ActualWidth * SliderPlay.Maximum;
        }
        #endregion
#region Track
        private void IncreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Track.Value += (Mouse.GetPosition((IInputElement)e.Source).X+6.5) / (Track.ActualWidth-14) * Track.Maximum;
        }

        private void DecreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Track.Value = Mouse.GetPosition((IInputElement)e.Source).X / (Track.ActualWidth -14)* Track.Maximum;
        }

        private void Thumb_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            //Button.Content = SliderPlay.Value;
            
            //MessageBox.Show("start");
        }

        private void Thumb_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            TextBlock.Text = "complate";
            //MessageBox.Show("Complate");
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            TextBlock.Text = SliderPlay.Value.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

    }
}
