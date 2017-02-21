using System;
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
using Player4.Model;
using Player4.ViewModel;

namespace Player4.View
{
    /// <summary>
    /// TagsSelectView.xaml 的交互逻辑
    /// </summary>
    public partial class TagsSelectView : UserControl
    {
        public TagsSelectView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button tag = (Button)e.OriginalSource;
            if (tag.FontWeight == FontWeights.Normal)
            {
                tag.FontWeight = FontWeights.Bold;
                tag.Foreground = FindResource("MouseOverBrush") as SolidColorBrush;
                Songs.SelectedTags.Add(tag.Content.ToString());
            }
            else if (tag.FontWeight == FontWeights.Bold)
            {
                tag.FontWeight = FontWeights.Normal;
                tag.Foreground = FindResource("ForegroundBrush") as SolidColorBrush;
                Songs.SelectedTags.RemoveAt(Songs.SelectedTags.IndexOf(tag.Content.ToString()));
            }
        }
    }
}
