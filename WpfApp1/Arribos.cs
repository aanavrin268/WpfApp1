using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class Arribos : INotifyPropertyChanged
    {
        private string _idArribo = "";
        private string _folio = "FARR0001";
        private string _folioOrden = "";
        private string _proveedor = "";
        private string _fPreFactura = "";
        private string _prefactura = "";
        private string _fFactura = "";
        private string _factura = "";
        private int _unidades;
        private string _status = "PLANEACION";
        private string _causal = "EN PLANEACION";
        private DateTime _envioFabrica = DateTime.Today.Date;
        private DateTime _aduanaAnalisis = DateTime.Today.Date;
        private DateTime _fechaAlmacen = DateTime.Today.Date;
        private DateTime _liberacionWms = DateTime.Today.Date;
        private int _retencion = 0;
        private int _noConforme = 0;
        private int _disponibleWms = 0;
        private int _noLotes = 0;
        private int _noSemana = 0;
        private DateTime _lastUpdate = DateTime.Today.Date;



        public DateTime LastUpdates
        {
            get => _lastUpdate;

            set
            {
                if (_lastUpdate != value)
                {
                    _lastUpdate = value;
                    OnPropertyChanged();
                }
            }
        }




        public int NoSemana
        {
            get => _noSemana;

            set
            {
                if (_noSemana != value)
                {
                    _noSemana = value;
                    OnPropertyChanged();
                }
            }
        }



        public int NoLotes
        {
            get => _noLotes;

            set
            {
                if (_noLotes != value)
                {
                    _noLotes = value;
                    OnPropertyChanged();
                }
            }
        }


        public int DisponibleWms
        {
            get => _disponibleWms;

            set
            {
                if (_disponibleWms != value)
                {
                    _disponibleWms = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NoConforme
        {
            get => _noConforme;

            set
            {
                if (_noConforme != value)
                {
                    _noConforme = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Retencion
        {
            get => _retencion;

            set
            {
                if(_retencion != value)
                {
                    _retencion = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Factura
        {
            get => _factura;

            set
            {
                if (_factura != value)
                {
                    _factura = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FFactura
        {
            get => _fFactura;

            set
            {
                if (_fFactura != value)
                {
                    _fFactura = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Prefactura
        {
            get => _prefactura;

            set
            {
                if (_prefactura != value)
                {
                    _prefactura = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FPrefactura
        {
            get => _fPreFactura;

            set
            {
                if (_fPreFactura != value)
                {
                    _fPreFactura = value;
                    OnPropertyChanged();
                }
            }
        }


        public string Proveedor
        {
            get => _proveedor;

            set
            {
                if(_proveedor != value)
                {
                    _proveedor = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime LiberacionWms
        {
            get => _liberacionWms;

            set
            {
                if (_liberacionWms != value)
                {
                    _liberacionWms = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime FechaAlmacen
        {
            get => _fechaAlmacen;

            set
            {
                if (_fechaAlmacen != value)
                {
                    _fechaAlmacen = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime AduanaAnalisis
        {
            get => _aduanaAnalisis;

            set
            {
                if (_aduanaAnalisis != value)
                {
                    _aduanaAnalisis = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EnvioFabrica
        {
            get => _envioFabrica;

            set
            {
                if (_envioFabrica != value)
                {
                    _envioFabrica = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IdArribo
        {
            get => _idArribo;

            set
            {
                if(_idArribo != value)
                {
                    _idArribo = value;
                    OnPropertyChanged();
                }
            }
        }


        public string FolioOrden
        {
            get => _folioOrden;

            set
            {
                if(_folioOrden != value)
                {
                    _folioOrden = value;
                    OnPropertyChanged();
                }
            }
        }

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