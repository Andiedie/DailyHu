using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace front_end.Model {
    abstract public class BindableBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T source, T value, [CallerMemberName] string prop = null) {
            source = value;
            OnPropertyChanged(prop);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string prop = null) {
            if (PropertyChanged != null) {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
