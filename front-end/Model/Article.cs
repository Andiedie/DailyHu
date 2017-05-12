using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace front_end.Model {
    public class Article : BindableBase {
        private string date;
        private string thumbnail;
        private string title;
        private string url;

        public string Date {
            get { return date; }
            set { SetProperty(ref date, value); }
        }
        public string Thumbnail {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }
        public string Title {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public string Url {
            get { return url; }
            set { SetProperty(ref url, value); }
        }

        public Article(string d, string th, string t, string u) {
            Date = d;
            Thumbnail = th;
            Title = t;
            Url = u;
        }

        public Article() {}
    }
}
