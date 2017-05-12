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
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI;
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
        public MainPage()  {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            vm.Site = DB.getSite();
            changeSite(vm.Site);
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selected = ((ListView)sender).SelectedItem as Article;
            if (selected != null && selected.Title != null) {
                vm.current = selected;
                webview.Source = new Uri(selected.Url);
            }
        }

        private async void ToBottom(object sender, ScrollViewerViewChangedEventArgs e) {
            var verticalOffset = articlesScroll.VerticalOffset;
            var maxVerticalOffset = articlesScroll.ScrollableHeight;
            if (maxVerticalOffset < 0 || verticalOffset == maxVerticalOffset) {
                if (vm.Articles.Count != 0 && !(vm.Articles.Last() is BottomProcessRing)) {
                    vm.Articles.Add(new BottomProcessRing());
                    vm.Page++;
                    var list = await Article.getArticles(vm.Site, vm.Page);
                    vm.Articles.Remove(vm.Articles.Last());
                    list.ForEach(vm.Articles.Add);
                }
            }
        }

        private void switch2Tuicool(object sender, RoutedEventArgs e) {
            changeSite(Site.tuicool);
        }

        private void switch2Zhihu(object sender, RoutedEventArgs e) {
            changeSite(Site.zhihu);
        }

        private void switch2Cnode(object sender, RoutedEventArgs e) {
            changeSite(Site.cnode);
        }

        private void changeSite(Site site) {
            vm.Site = site;
            vm.Page = 1;
            var trans = new SolidColorBrush(Colors.Transparent);
            var selected = new SolidColorBrush(Colors.Gray);
            tuicoolBtn.Background = trans;
            zhihuBtn.Background = trans;
            cnodeBtn.Background = trans;
            switch (site) {
                case Site.zhihu:
                    zhihuBtn.Background = selected; break;
                case Site.tuicool:
                    tuicoolBtn.Background = selected; break;
                case Site.cnode:
                    cnodeBtn.Background = selected; break;
            }
            DB.updateSite(site);
            update();
        }

        private async void update() {
            vm.Articles.Clear();
            vm.Articles.Add(new BottomProcessRing());
            var list = await Article.getArticles(vm.Site, vm.Page);
            list.ForEach(vm.Articles.Add);
            vm.Articles.Remove(vm.Articles.First());
            articlesScroll.ChangeView(null, 0, null);
        }

        private void share(object sender, RoutedEventArgs e) {
            if (vm.current != null)
                DataTransferManager.ShowShareUI();
        }

        void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args) {
            var dp = args.Request.Data;
            dp.Properties.Title = vm.current.Title;
            dp.Properties.Description = vm.current.Url;
            var deferral = args.Request.GetDeferral();
            dp.SetBitmap(RandomAccessStreamReference.CreateFromUri(new Uri(vm.current.Thumbnail)));
            deferral.Complete();
        }

        private void contentLoading(WebView sender, WebViewContentLoadingEventArgs args) {
            webLoadRing.Visibility = Visibility.Visible;
        }

        private void contentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args) {
            webLoadRing.Visibility = Visibility.Collapsed;
        }
    }
}
