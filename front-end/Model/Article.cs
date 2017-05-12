using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace front_end.Model {
    class Article {
        private string date;
        private string thumbnail;
        private string title;
        private string url;

        public string Date { set; get; }
        public string Thumbnail { set; get; }
        public string Title { set; get; }
        public string Url { set; get; }

        public Article(string d, string th, string t, string u) {
            Date = d;
            Thumbnail = th;
            Title = t;
            Url = u;
        }
    }
}
