using System.Windows.Input;

namespace WPFTest.HeritageTest
{
    public class FatherClass:NotificationObject
    {
        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value; 
                OnPropertyChanged();
            }
        }

        private string _type2;

        public string Type2
        {
            get { return _type2; }
            set
            {
                _type2 = value; 
                OnPropertyChanged();
            }
        }


        public ICommand Command { get; set; }

        private void OnCommand()
        {
            Type += "-->FatherType";
            Type2 += "->FatherType";
        }

        public FatherClass()
        {
            Command=new DelegateCommand(OnCommand);
        }
    }
}
