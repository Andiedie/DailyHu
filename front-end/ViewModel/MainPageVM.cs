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
        public Site Site { get; set; }
        public int Page { get; set; }
        public Article current { get; set; }
        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles {
            get { return articles; }
            set { SetProperty(ref articles, value); }
        }
        private ObservableCollection<Site> sites;
        public ObservableCollection<Site> Sites {
            get { return sites; }
            set { SetProperty(ref sites, value); }
        }
        public MainPageVM() {
            Articles = new ObservableCollection<Article>();
            Sites = new ObservableCollection<Site>();
            Page = 1;
            init();
        }
        private async void init () {
            var siteList = await Site.getSites();
            siteList.ForEach(Sites.Add);
            Site = siteList.First();
            siteGot(sites.ToList());
        }
        public delegate void siteGotHandler(List<Site> sites);
        public event siteGotHandler siteGot = delegate { };
    }
}
