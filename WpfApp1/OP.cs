using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp1
{
    public class OP : INotifyPropertyChanged
    {
        private string _nombre;
        private string _idSistema;
        private string _empresa;
        private int _unidades;
        private DateTime _fechaPlan;
        private DateTime _fechaOP;
        private ObservableCollection<Arribos> _arribos = new ObservableCollection<Arribos>();

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Id_Sistema
        {
            get => _idSistema;
            set
            {
                if (_idSistema != value)
                {
                    _idSistema = value;
                    OnPropertyChanged(nameof(Id_Sistema));
                }
            }
        }

        public string Empresa
        {
            get => _empresa;
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    OnPropertyChanged(nameof(Empresa));
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
                    OnPropertyChanged(nameof(Unidades));
                }
            }
        }

        public DateTime Fecha_Plan
        {
            get => _fechaPlan;
            set
            {
                if (_fechaPlan != value)
                {
                    _fechaPlan = value;
                    OnPropertyChanged(nameof(Fecha_Plan));
                }
            }
        }

        public DateTime Fecha_OP
        {
            get => _fechaOP;
            set
            {
                if (_fechaOP != value)
                {
                    _fechaOP = value;
                    OnPropertyChanged(nameof(Fecha_OP));
                }
            }
        }

        public ObservableCollection<Arribos> Arribos
        {
            get => _arribos;
            set
            {
                if (_arribos != value)
                {
                    _arribos = value;
                    OnPropertyChanged(nameof(Arribos));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}