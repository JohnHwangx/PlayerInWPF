using System.Windows.Controls;
using Player4.Service;

namespace Player4.View.Resource.Tags
{
    /// <summary>
    /// TagFeeling.xaml 的交互逻辑
    /// </summary>
    public partial class TagFeeling : UserControl
    {
        public TagFeeling()
        {
            InitializeComponent();
            var buttonServer = new CreateButtonServer();
            foreach (var button in buttonServer.GetButtonContent("FeelingTags"))
            {
                WrapPanel.Children.Add(button);
            }
        }
    }
}
