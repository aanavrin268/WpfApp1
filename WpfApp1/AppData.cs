using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class AppData
    {

        public static ObservableCollection<Producto> ProductoObs { get; set; } = new ObservableCollection<Producto>();
        public static ObservableCollection<Persona> PersonasObs { get; set; } = new ObservableCollection<Persona>();

        public static List<Persona> Personas { get; set; } = new List<Persona>();

        public static string ProductosJson => JsonConvert.SerializeObject(ProductoObs, Formatting.Indented);

        public static string PersonasJson => JsonConvert.SerializeObject(Personas, Formatting.Indented);

        public static string SelectedProducto { get; set; }

    }
}
