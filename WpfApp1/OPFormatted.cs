using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class OPFormatted : INotifyPropertyChanged
    {
        private string _op;
        private string _folioOp;
        private ObservableCollection<Productss> _products;

        public string Op
        {
            get => _op;
            set
            {
                if (_op != value)
                {
                    _op = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FolioOp
        {
            get => _folioOp;
            set
            {
                if (_folioOp != value)
                {
                    _folioOp = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Productss> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }

        public OPFormatted()
        {
            _products = new ObservableCollection<Productss>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}