using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class Arribos : INotifyPropertyChanged
    {
        private string _folio = "ARR0001";
        private int _unidades;
        private string _status = "PLANEACION";
        private string _causal = "EN PLANEACION";

        public string Folio
        {
            get => _folio;
            set
            {
                if (_folio != value)
                {
                    _folio = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Unidades
        {
            get => _unidades;
            set
            {
                if (_unidades != value)
                {
                    _unidades = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Causal
        {
            get => _causal;
            set
            {
                if (_causal != value)
                {
                    _causal = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}