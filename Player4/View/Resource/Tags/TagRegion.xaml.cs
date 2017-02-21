using System.Windows;
using System.Windows.Controls;
using Player4.Service;
using Player4.ViewModel;

namespace Player4.View.Resource.Tags
{
    /// <summary>
    /// TagRegion.xaml 的交互逻辑
    /// </summary>
    public partial class TagRegion : UserControl
    {
        public TagRegion()
        {
            InitializeComponent();
            var buttonServer = new CreateButtonServer();
            foreach (var button in buttonServer.GetButtonContent("RegionTags"))
            {
                WrapPanel.Children.Add(button);
            }
            var tagsEditingWindowViewModel = DataContext as TagsEditingWindowViewModel;
            if (tagsEditingWindowViewModel != null)
                MessageBox.Show(tagsEditingWindowViewModel.Name);
        }
    }
}
