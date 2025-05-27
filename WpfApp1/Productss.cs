using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Productss: INotifyPropertyChanged
    {

        private string _nombre;
        private string _id_sistema;
        private string _empresa;
        private int _unidades;
        private DateTime _fecha_plan;
        private DateTime _fecha_op;
        private ObservableCollection<Arribos> _arribos = new ObservableCollection<Arribos>();


        public  string Nombre
        {
            get => _nombre;

            set
            {
                if(_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));

                }
            }
        }
        public  string Id_Sistema
        {
            get => _id_sistema;


            set
            {
                if(_id_sistema != value)
                {
                    _id_sistema = value;
                    OnPropertyChanged(nameof(Id_Sistema));

                }
            }

        }
        public  string Empresa
        {
            get => _empresa;

            set
            {
                if(_empresa != value)
                {
                    _empresa = value;
                    OnPropertyChanged(nameof(Empresa));

                }
            }
        
        }
        public  int Unidades 
        {
            get => _unidades;

            set
            {
                if(_unidades != value)
                {
                    _unidades = value;
                    OnPropertyChanged(nameof(Unidades));

                }
            }
        
        }
        public  DateTime Fecha_Plan 
        {
            get => _fecha_plan;
            set 
            {
                if(_fecha_plan  != value)
                {
                    _fecha_plan = value;
                    OnPropertyChanged(nameof(Fecha_Plan));

                }
            }
        }
        public  DateTime Fecha_OP 
        {
            get => _fecha_op;
            set
            {
                if(_fecha_op != value)
                {
                    _fecha_op = value;
                    OnPropertyChanged(nameof(Fecha_OP));

                }
            }
        }

        public  ObservableCollection<Arribos> Arribos 
        {
            get => _arribos;
            set
            {
                if(_arribos != value)
                {
                    _arribos = value;
                    OnPropertyChanged(nameof(Arribos));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
