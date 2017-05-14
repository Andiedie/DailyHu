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
using front_end.View;
using front_end.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
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
            // 设置title栏颜色
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(255, 128, 57, 173);
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 128, 57, 173);
            // 设置livetile
            vm.siteGot += TileController.run;
            // 加载首页
            vm.siteGot += (sites) => update();
            // 数据库存储
            vm.siteGot += DB.update;
            // 导航相关
            NavigationCacheMode = NavigationCacheMode.Enabled;
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
            // Adaptive UI
            var groups = VisualStateManager.GetVisualStateGroups(adaptiveRoot);
            groups[0].CurrentStateChanged += OnCurrentStateChanged;
        }

        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e) {
            if (e.NewState.Name == "Narrow" && vm.current != null) {
                Frame.Navigate(typeof(ArticlePage), vm.current);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            webview.Source = new Uri(vm.current == null ? "ms-appx-web:///Assets/index.html" : vm.current.Url);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selected = ((ListView)sender).SelectedItem as Article;
            if (selected != null && selected.Title != null) {
                vm.current = selected;
                if (webViewContainer.Visibility == Visibility.Visible) {
                    webview.Source = new Uri(selected.Url);
                    webLoadRing.Visibility = Visibility.Visible;
                    shareBtn.Visibility = Visibility.Visible;
                }
                else {
                    Frame.Navigate(typeof(ArticlePage), selected);
                }
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
                    if (!(vm.Articles.Last() is NoMore))
                        list.ForEach(vm.Articles.Add);
                }
            }
        }

        private void siteClick(object sender, SelectionChangedEventArgs e) {
            var site = siteList.SelectedItem as Site;
            vm.Site = site;
            vm.Page = 1;
            update();
        }

        private async void update() {
            vm.Articles.Clear();
            vm.Articles.Add(new BottomProcessRing());
            var list = await Article.getArticles(vm.Site, vm.Page);
            while (list.Count > 0 && !(list.First() is NoMore) && list.Count < 20) {
                vm.Page++;
                (await Article.getArticles(vm.Site, vm.Page)).ForEach(list.Add);
            }
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

        private void contentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args) {
            webLoadRing.Visibility = Visibility.Collapsed;
        }
    }
}
