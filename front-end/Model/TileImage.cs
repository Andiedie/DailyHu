using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace front_end.Model {
    // 动态磁贴图片
    public class TileImage {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Small { get; set; }
        public string Wide { get; set; }
        public TileImage(string l, string m, string s, string w) {
            Large = l;
            Medium = m;
            Small = s;
            Wide = w;
        }
        public TileImage() { }
    }
}
