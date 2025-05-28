using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Pt
    {

        private string _proveedor;

        public string Proveedor
        {
            get => _proveedor;

            set
            {
                if(_proveedor != value)
                {
                    _proveedor = value;
                }
            }
        }


        public string Nombre { get; set; }
        public string Id_sistema { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
