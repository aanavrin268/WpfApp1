using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class Reactividad : INotifyPropertyChanged
    {
        private static readonly Reactividad _instance = new Reactividad();
        public static Reactividad Instance => _instance;

        private OPFormatted _neoOp = new OPFormatted();
        private Productss _currentProductss = new Productss();


        public Productss currentProductss
        {
            get => _currentProductss;

            set
            {
                if(_currentProductss != value)
                {
                    _currentProductss = value;
                    OnPropertyChanged();
                }
            }
        }

        public OPFormatted neoOp
        {
            get => _neoOp;
            set
            {
                if (_neoOp != value)
                {
                    _neoOp = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Reactividad() { }
    }
}