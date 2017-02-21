using System.Windows.Controls;
using Player4.Service;

namespace Player4.View.Resource.Tags
{
    /// <summary>
    /// TagTheme.xaml 的交互逻辑
    /// </summary>
    public partial class TagTheme : UserControl
    {
        public TagTheme()
        {
            InitializeComponent();
            var buttonServer = new CreateButtonServer();
            foreach (var button in buttonServer.GetButtonContent("ThemeTags"))
            {
                WrapPanel.Children.Add(button);
            }
        }
    }
}
