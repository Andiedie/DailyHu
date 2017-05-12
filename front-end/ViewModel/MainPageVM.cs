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
            list.ForEach(Articles.Add);
        }
    }
}
