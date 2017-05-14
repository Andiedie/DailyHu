using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace front_end.Model {
    // 数据源
    public class Site {
        public string Name { set; get; }
        public string Icon { set; get; }
        public TileImage TileImage { set; get; }
        public Site(string n, string i, TileImage t) {
            Name = n;
            Icon = i;
            TileImage = t;
        }
        public Site() { }
        // 从服务器获取数据源，如果失败则从数据库获取缓存
        public static async Task<List<Site>> getSites() {
            try {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create($"http://andiedie.cc:8008/meta");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Site>>(json);
            }
            catch (Exception) {
                return DB.getSites();
            }
        }
    }
}
