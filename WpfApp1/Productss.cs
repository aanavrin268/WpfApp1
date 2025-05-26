using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Productss
    {
        public  string Nombre { get; set; }
        public  string Id_Sistema { get; set; }
        public  string Empresa { get; set; }
        public  int Unidades { get; set; }
        public  DateTime Fecha_Plan { get; set; }
        public  DateTime Fecha_OP { get; set; }

        public  ObservableCollection<Arribos> Arribos { get; set; } = new ObservableCollection<Arribos>();
    }
}
