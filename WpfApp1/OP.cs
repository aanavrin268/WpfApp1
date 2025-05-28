using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp1
{
    public class OP : INotifyPropertyChanged
    {

        private string _folioOP;
        private string _op;
        private string _nombre;
        private string _idSistema;
        private string _descripcion = "";
        private string _proveedorDesc = "";
        private string _empresa;
        private int _costo = 0;
        private int _unidades;
        private int _montoOrden = 0;
        private string _moneda = "MXN";
        private string _incoterms = "";
        private string _cComerciales = "Efectivo";
        private DateTime _fechaPlan = DateTime.Today.Date;
        private DateTime _fechaOP = DateTime.Today.Date;
        private int _totalPzsArribos = 0;
        private string _statusOrden = "";
        private string _tipoProducto = "";
        private ObservableCollection<Arribos> _arribos = new ObservableCollection<Arribos>();

        public string TipoProducto
        {
            get => _tipoProducto;
            set
            {
                if (_tipoProducto != value)
                {
                    _tipoProducto = value;
                    OnPropertyChanged(nameof(TipoProducto));
                }
            }
        }


        public string StatusOrden
        {
            get => _statusOrden;
            set
            {
                if (_statusOrden != value)
                {
                    _statusOrden = value;
                    OnPropertyChanged(nameof(StatusOrden));
                }
            }
        }


        public int TotalPzsArribos
        {
            get => _totalPzsArribos;
            set
            {
                if (_totalPzsArribos != value)
                {
                    _totalPzsArribos = value;
                    OnPropertyChanged(nameof(TotalPzsArribos));
                }
            }
        }

        public string CComerciales
        {
            get => _cComerciales;
            set
            {
                if (_cComerciales != value)
                {
                    _cComerciales = value;
                    OnPropertyChanged(nameof(CComerciales));
                }
            }
        }


        public string Incoterms
        {
            get => _incoterms;
            set
            {
                if (_incoterms != value)
                {
                    _incoterms = value;
                    OnPropertyChanged(nameof(Incoterms));
                }
            }
        }


        public string Moneda
        {
            get => _moneda;
            set
            {
                if (_moneda != value)
                {
                    _moneda = value;
                    OnPropertyChanged(nameof(Moneda));
                }
            }
        }

        public int MontoOrden
        {
            get => _montoOrden;
            set
            {
                if (_montoOrden != value)
                {
                    _montoOrden = value;
                    OnPropertyChanged(nameof(MontoOrden));
                }
            }
        }


        public int Costo
        {
            get => _costo;
            set
            {
                if(_costo != value)
                {
                    _costo = value;
                    OnPropertyChanged(nameof(Costo));
                }
            }
        }

        public string ProveedorDesc
        {
            get => _proveedorDesc;

            set
            {
                if (_proveedorDesc != value)
                {
                    _proveedorDesc = value;
                    OnPropertyChanged(nameof(ProveedorDesc));
                }
            }
        }


        public string Descripcion
        {
            get => _descripcion;

            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    OnPropertyChanged(nameof(Descripcion));
                }
            }
        }


        public string Op
        {
            get => _op;
            set
            {
                if(_op != value)
                {
                    _op = value;
                    OnPropertyChanged(nameof(Op));
                }
            }
        }

        public string FolioOP
        {
            get => _folioOP;
            set
            {
                if(_folioOP != value)
                {
                    _folioOP = value;
                    OnPropertyChanged(nameof(FolioOP));
                }
            }

        }

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