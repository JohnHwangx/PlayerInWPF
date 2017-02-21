using System.Windows.Controls;
using Player4.Service;

namespace Player4.View.Resource.Tags
{
    /// <summary>
    /// TagStyle.xaml 的交互逻辑
    /// </summary>
    public partial class TagStyle : UserControl
    {
        public TagStyle()
        {
            InitializeComponent();
            var buttonServer = new CreateButtonServer();
            foreach (var button in buttonServer.GetButtonContent("StyleTags"))
            {
                WrapPanel.Children.Add(button);
            }
        }
    }
}
