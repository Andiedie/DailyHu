using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using front_end.Model;
using front_end.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace front_end
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageVM vm = new MainPageVM();
        public MainPage()
        {
            this.InitializeComponent();
            update();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selected = e.AddedItems.First() as Article;
            webview.Source = new Uri(selected.Url);
        }

        private async void ToBottom(object sender, ScrollViewerViewChangedEventArgs e) {
            var verticalOffset = articlesScroll.VerticalOffset;
            var maxVerticalOffset = articlesScroll.ScrollableHeight;
            if (maxVerticalOffset < 0 || verticalOffset == maxVerticalOffset) {
                if (!(vm.Articles.Last() is BottomProcessRing)) {
                    vm.Articles.Add(new BottomProcessRing());
                    vm.Page++;
                    vm.addArticles(await Article.getArticles(vm.Site, vm.Page));
                }
            }
        }

        private void switch2Tuicool(object sender, RoutedEventArgs e) {
            vm.Site = Site.tuicool;
            vm.Page = 1;
            update();
        }

        private void switch2Zhihu(object sender, RoutedEventArgs e) {
            vm.Site = Site.zhihu;
            vm.Page = 1;
            update();
        }

        private void switch2Cnode(object sender, RoutedEventArgs e) {
            vm.Site = Site.cnode;
            vm.Page = 1;
            update();
        }

        private async void update() {
            vm.updateArticles(await Article.getArticles(vm.Site, vm.Page));
            articlesScroll.ChangeView(null, 0, null);
        }
    }
}
