using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace front_end.Model {
    // 模板选择器，用于在文章列表中加入不同的模板：文章、载入动画、到达底部提示
    public class ArticleListDataTemplateSelector : DataTemplateSelector {
        public ObservableCollection<TemplateMatch> Matches { get; set; }
        public ArticleListDataTemplateSelector() {
            Matches = new ObservableCollection<TemplateMatch>();
        }
        protected override DataTemplate SelectTemplateCore(object item) {
            return Matches.FirstOrDefault(m => m.TargetType.Equals(item.GetType().ToString()))?.Template;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            return Matches.FirstOrDefault(m => m.TargetType.Equals(item.GetType().ToString()))?.Template;
        }
    }
}
