using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Player4.Model;
using Player4.Properties;

namespace Player4.ViewModel
{
#pragma warning disable 618
    public class TagsEditingWindowViewModel:Window
#pragma warning restore 618
    {
        //public static List<string> TagsList { get; set; }

        public bool IsTagChanged = false;

        public DelegateCommand<Window> CloseWindowCommand { get; set; }
        public DelegateCommand<Window> ClearTagsCommand { get; set; }
        public DelegateCommand<Window> EditTagsCommand { get; set; }
        public DelegateCommand<Window> WindowLoadedCommand { get; set; }

        private void CloseWindowExecute(Window window)
        {
            window.Close();
        }

        private void ClearTagsExecute(Window window)
        {
            CheckTag(GetStackPanel(window).Children, false);
            Songs.SongTags.Clear();
            //TagsList.Clear();
        }

        private void EditTagsExecute(Window window)
        {
            window.DialogResult = true;
            window.Close();
        }

        private void WindowLoadedExecute(Window window)
        {
            CheckTag(GetStackPanel(window).Children, true);
        }

        private StackPanel GetStackPanel(Window window)
        {
            var grid = window.Content as Grid;
            if (grid == null) return null;
            var children = grid.Children;
            foreach (var child in children)
            {
                var userControl = (child as ScrollViewer)?.Content as UserControl;
                var stackPanel = userControl?.Content as StackPanel;
                if (stackPanel != null)
                    return stackPanel;
            }
            return null;
        }

        public TagsEditingWindowViewModel()
        {
            TagClickCommand=new DelegateCommand<Button>(OnTagClick);
            CloseWindowCommand = new DelegateCommand<Window>(CloseWindowExecute);
            ClearTagsCommand = new DelegateCommand<Window>(ClearTagsExecute);
            EditTagsCommand = new DelegateCommand<Window>(EditTagsExecute);
            WindowLoadedCommand = new DelegateCommand<Window>(WindowLoadedExecute);
        }

        public DelegateCommand<Button> TagClickCommand { get; set; }

        private void OnTagClick(Button tag)
        {
            if (tag.FontWeight == FontWeights.Normal)
            {
                tag.FontWeight = FontWeights.Bold;
                tag.Foreground = FindResource("MouseOverBrush") as SolidColorBrush;
                Songs.SongTags.Add(tag.Content.ToString());
            }
            else if (tag.FontWeight == FontWeights.Bold)
            {
                tag.FontWeight = FontWeights.Normal;
                tag.Foreground = FindResource("ForegroundBrush") as SolidColorBrush;
                Songs.SongTags.RemoveAt(Songs.SongTags.IndexOf(tag.Content.ToString()));
            }
        }

        private void CheckTag(IEnumerable uiControls, bool flag)
        {
            foreach (UIElement element in uiControls)
            {
                if (element is Button)
                {
                    Button temp = element as Button;
                    if (Songs.SongTags == null) continue;
                    //if (TagsList == null) continue;
                    foreach (var tag in /*TagsList*/Songs.SongTags)
                    {
                        if (tag != temp.Content.ToString()) continue;
                        if (flag)
                        {
                            temp.FontWeight = FontWeights.Bold;
                            var resourceDictionary = new ResourceDictionary
                            {
                                Source = new Uri(@"View/Resource/TagViewsResource.xaml",
                                    UriKind.RelativeOrAbsolute)
                            };
                            temp.Foreground = resourceDictionary["MouseOverBrush"] as SolidColorBrush;
                        }
                        else
                        {
                            temp.FontWeight = FontWeights.Normal;
                            var resourceDictionary = new ResourceDictionary
                            {
                                Source = new Uri(@"View/Resource/TagViewsResource.xaml",
                                    UriKind.RelativeOrAbsolute)
                            };
                            temp.Foreground = resourceDictionary["ForegroundBrush"] as SolidColorBrush;
                        }
                    }
                }
                else if (element is GroupBox)
                {
                    var userControl = (element as GroupBox).Content as UserControl;
                    var wrapPanel = userControl?.Content as WrapPanel;
                    if (wrapPanel != null)
                        CheckTag(wrapPanel.Children, flag);
                }
            }
        }
    }
}