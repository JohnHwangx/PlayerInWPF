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

namespace WPFTest.MediaElement
{
	/// <summary>
	/// MediaElementView.xaml 的交互逻辑
	/// </summary>
	public partial class MediaElementView : UserControl
	{
		public MediaElemViewModel MediaElemViewModel { get; set; }
		public MediaElementView()
		{
			InitializeComponent();
			DataContext = new MediaElemViewModel(MediaElement);
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			//MediaElemViewModel = new MediaElemViewModel();
			
			//var path=MediaElemViewModel.OnClick();
			//MediaElement.Source=new Uri(path);
			//MediaElement.Play();
		}
	}
}
