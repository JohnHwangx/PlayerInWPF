using System.Windows.Controls;
using Player4.Service;

namespace Player4.View.Resource.Tags
{
    /// <summary>
    /// TagScene.xaml 的交互逻辑
    /// </summary>
    public partial class TagScene : UserControl
    {
        public TagScene()
        {
            InitializeComponent();
            var buttonServer = new CreateButtonServer();
            foreach (var button in buttonServer.GetButtonContent("SceneTags"))
            {
                WrapPanel.Children.Add(button);
            }
        }
    }
}
