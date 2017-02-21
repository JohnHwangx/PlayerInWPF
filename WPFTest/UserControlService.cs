using System.Windows;
using System.Windows.Input;

namespace WPFTest
{
    public class UserControlService:NotificationObject
    {
        private string _song;

        public string Song
        {
            get { return _song; }
            set
            {
                _song = value; 
                OnPropertyChanged();
            }
        }

        public UserControlService()
        {
            Command = new DelegateCommand(OnCommand);
            //Command = new DelegateCommand(OnCommandParam);
        }

        //private void OnCommand()

        public ICommand Command { get; set; }

        private void OnCommand()
        {
            Song += "-->I'm new Song";
            MainWindowViewModel.Song += "-->Song";
            MessageBox.Show("test");
        }

        private void OnCommandParam(object param)
        {
            MessageBox.Show("test");
        }
    }
}