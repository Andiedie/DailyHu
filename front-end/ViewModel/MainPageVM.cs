using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using front_end.Model;

namespace front_end.ViewModel {
    class MainPageVM : BindableBase {
        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles {
            get { return articles; }
            set { SetProperty(ref articles, value); }
        }
        public MainPageVM() {
            Articles = new ObservableCollection<Article>();
        }
        public void updateArticles(List<Article> list) {
            Articles.Clear();
            list.ForEach(Articles.Add);
        }
        public void addArticles(List<Article> list) {
            if (Articles.Last() is BottomProcessRing) {
                Articles.RemoveAt(Articles.Count - 1);
            }
            list.ForEach(Articles.Add);
        }
        public Site Site = Site.tuicool;
        public int Page = 1;
    }
}
