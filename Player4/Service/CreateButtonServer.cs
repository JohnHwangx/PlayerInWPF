using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using Player4.ViewModel;

namespace Player4.Service
{
    class CreateButtonServer : Window/*,ICommand*/
    {
        public List<Button> GetButtonContent(string categoryName)
        {
            const string path = @"..\..\Image\SongTags.xml";
            XDocument xDoc = XDocument.Load(path/*@"C:\Users\john\Source\Repos\Player4\Player4\Image\SongTags.xml"*/);
            var tags = xDoc.Descendants(categoryName);
            var buttonList = new List<Button>();
            var count = 0;
            foreach (var category in tags)
            {
                //tagList.AddRange(category.Elements().Select(tag => tag.Value));
                foreach (var xElement in category.Elements())
                {
                    buttonList.Add(CreateButton(xElement.Value,count));
                    count++;
                }
            }
            return buttonList;
        }

        public Button CreateButton(string content,int count)
        {
            var button = new Button
            {
                Name="btn"+count,
                Content = content,
                Width = 70,
                Height = 20,
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                Foreground = FindResource("ForegroundBrush") as SolidColorBrush,
                Template = FindResource("TagsButtonTemplate") as ControlTemplate,
                //TODO 添加Command和CommandParameter
            };
            button.SetBinding(ButtonBase.CommandProperty, new Binding("SongListService.SelectTagCommand"));
            button.SetBinding(ButtonBase.CommandParameterProperty, new Binding{Source = button});
            return button;
        }
    }
}
