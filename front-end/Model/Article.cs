using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            set {
                if (value == "")
                    value = "ms-appx:///Assets/StoreLogo.scale-100.png";
                SetProperty(ref thumbnail, value);
            }
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

        public static async Task<List<Article>> getArticles(Site site, int page) {
            try {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create($"http://localhost:8008/list?site={site.Name}&page={page}");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Article>>(json);
            }
            catch (Exception) {
                var ans = new List<Article>();
                ans.Add(new NoMore());
                return ans;
            }
        }
    }
}
