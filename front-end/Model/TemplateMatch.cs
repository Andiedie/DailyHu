using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace front_end.Model {
    // 模板选择器的子类
    public class TemplateMatch {
        public string TargetType { get; set; }
        public DataTemplate Template { get; set; }
    }
}
