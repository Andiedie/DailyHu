using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using front_end.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace front_end.View {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ArticlePage : Page {
        public ArticlePage() {
            this.InitializeComponent();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(255, 128, 57, 173);
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 128, 57, 173);
            var groups = VisualStateManager.GetVisualStateGroups(adaptiveRoot);
            groups[0].CurrentStateChanged += OnCurrentStateChanged;
        }

        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e) {
            if (e.NewState.Name == "Wide") {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            var article = e.Parameter as Article;
            webview.Source = new Uri(article.Url);
            Frame rootFrame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                = rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void share(object sender, RoutedEventArgs e) {
            DataTransferManager.ShowShareUI();
        }

        private void contentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args) {
            webLoadRing.Visibility = Visibility.Collapsed;
        }
    }
}
