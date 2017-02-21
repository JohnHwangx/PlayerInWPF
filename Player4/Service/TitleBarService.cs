using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Player4.Service
{
#pragma warning disable 618
    public class TitleBarService:NotificationObject
#pragma warning restore 618
    {
        public DelegateCommand<Window> CloseWindowCommand { get; set; }

        private void CloseWindowExecute(Window window)
        {
            window.Close();
        }
        public DelegateCommand<Window> MinWindowCommand { get; set; }

        private void MinWindowExecute(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public DelegateCommand<Window> DragWindowCommand { get; set; }

        private void DragWindowExecute(Window window)
        {
            window.DragMove();
        }

        public DelegateCommand<Window> DragHorizontalCommand { get; set; }

        private void OnDragHorizontal(Window window)
        {
            
        }

        public TitleBarService()
        {
            CloseWindowCommand = new DelegateCommand<Window>(CloseWindowExecute);
            DragWindowCommand = new DelegateCommand<Window>(DragWindowExecute);
            MinWindowCommand = new DelegateCommand<Window>(MinWindowExecute);
        }
    }
}