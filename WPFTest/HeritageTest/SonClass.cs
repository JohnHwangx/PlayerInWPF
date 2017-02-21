using System.Windows.Input;

namespace WPFTest.HeritageTest
{
    public class SonClass:FatherClass
    {
        public ICommand SonCommand { get; set; }

        private void OnSonCommand()
        {
            Type2 += "-->SonType";
        }

        public SonClass()
        {
            SonCommand=new DelegateCommand(OnSonCommand);
        }
    }
}