using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using front_end.Model;

namespace front_end.ViewModel {
    class MainPageVM {
        private ObservableCollection<Article> articles;
        private ObservableCollection<Article> Articles { set; get; }
        public MainPageVM(List<Article> list) {
            Articles = new ObservableCollection<Article>(list);
        }
    }
}
