using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WPFTest.SliderTest
{
    public class SliderViewModel:Window
    {
        public ICommand IncreaseCommand { get; set; }
        public ICommand DecreaseCommand { get; set; }

        private void IncreaseExecute()
        {
            MessageBox.Show("Increase");
        }

        private void DecreaseExecute()
        {
            MessageBox.Show("Decrease");
        }

        public SliderViewModel()
        {
            IncreaseCommand=new DelegateCommand(IncreaseExecute);
            DecreaseCommand=new DelegateCommand(DecreaseExecute);
        }
    }
}