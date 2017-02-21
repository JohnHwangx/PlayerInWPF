using System.Collections.Generic;
using System.Windows.Input;
using WPFTest.HeritageTest;
using WPFTest.MediaPlayer;
using WPFTest.SliderTest;
using WPFTest.SQLiteTest;

namespace WPFTest
{
    public class MainWindowViewModel:NotificationObject
    {
        private SliderViewModel _sliderViewModel;

        public SliderViewModel SliderViewModel
        {
            get { return _sliderViewModel; }
            set
            {
                _sliderViewModel = value; 
                OnPropertyChanged();
            }
        }

        public PlayerViewModel PlayerViewModel { get; set; }

        public SqliteOperater SqliteOperater { get; set; }

        private UserControlService _controlService;

        public UserControlService ControlService
        {
            get { return _controlService; }
            set
            {
                _controlService = value;
                OnPropertyChanged();
                //Song = ControlService.Song;
            }
        }

        private SonClass _sonClass;

        public SonClass SonClass
        {
            get { return _sonClass; }
            set
            {
                _sonClass = value; 
                OnPropertyChanged();
            }
        }

        private FatherClass _fatherClass;

        public FatherClass FatherClass
        {
            get { return _fatherClass; }
            set
            {
                _fatherClass = value; 
                OnPropertyChanged();
            }
        }

        private string _b;

        public string B
        {
            get { return _b; }
            set
            {
                _b = value; 
                OnPropertyChanged();
            }
        }

        public static string Song { get; set; }//静态属性无法进行

        private List<string> _tags;

        public List<string> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value; 
                OnPropertyChanged();
            }
        }

        public ICommand Command { get; set; }

        private void OnCommand()
        {
            B += "-->B";
        }

        public MainWindowViewModel()
        {
            //var binding = new Binding();
            /*******常规测试*******
            ControlService = new UserControlService();
            Command=new DelegateCommand(OnCommand);
            /**************/

            //var dbOperator=new DbOperator();
            //dbOperator.InsertTable();

            /******XML读取测试**********
            var xmlDataServer=new XmlDataServer();
            Tags = new List<string>();
            Tags = xmlDataServer.GetTagList();
            /****************/

            /****继承下属性改变对绑定控件的影响
            SonClass = new SonClass();
            //FatherClass = new FatherClass();****/

            /**测试按钮的参数为自己的情况**/
            //ControlService = new UserControlService();

            /*****测试Slider控制条***/
            //SliderViewModel=new SliderViewModel();

            /**SQLite数据库测试**/
            //SqliteOperater = new SqliteOperater();
            //SqliteOperater.DbCreate();

            /*****MediaPlayerTest****/
            PlayerViewModel = new PlayerViewModel();
        }
    }
}