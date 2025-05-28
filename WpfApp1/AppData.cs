using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class AppData

    {


        //private static AppData _instance;
        //public static AppData Instance => _instance ?? = new AppData();


        private static OP _currentOP = new OP();

        public static event EventHandler<PropertyChangedEventArgs> CurrentOPChanged;



        public static OPFormatted currentOPFormatted { get; set; }
        public static event EventHandler<PropertyChangedEventArgs> CurrentOPFormattedChanged;



        public static void UpdateCurrentOP(OPFormatted newOp)
        {
            currentOPFormatted = newOp;
            CurrentOPFormattedChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(currentOPFormatted)));
        }






        public static OP currentOP
        {
            get => _currentOP;
            set
            {
                if (_currentOP != value)
                {
                    _currentOP = value;
                    CurrentOPChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(currentOP)));
                }
            }
        }

    





        private static void OnCurrentOPChanged([CallerMemberName] string propertyName = null)
        {
            CurrentOPChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        /*
            OBSERVABLES
         */


        public static ObservableCollection<OPFormatted> initialFormattedOP { get; set; } = new ObservableCollection<OPFormatted>();


        public static ObservableCollection<Pt> initialPtLists { get; } = new ObservableCollection<Pt>();

        public static ObservableCollection<OP> initialListOP { get; set; } = new ObservableCollection<OP>();

        public static ObservableCollection<Arribos> ArribosList { get; set; } = new ObservableCollection<Arribos>();

        public static ObservableCollection<OP> ListaOps { get; } = new ObservableCollection<OP>();


        public static ObservableCollection<OP> OpObs { get; set; } = new ObservableCollection<OP>();


        public static ObservableCollection<Arribos> ArribosObs { get; set; } = new ObservableCollection<Arribos>();

        public static ObservableCollection<Empresa> EmpresasObs { get; set; } = new ObservableCollection<Empresa>();
        public static ObservableCollection<Producto> ProductoObs { get; set; } = new ObservableCollection<Producto>();
        public static ObservableCollection<Persona> PersonasObs { get; set; } = new ObservableCollection<Persona>();

        public static List<Persona> Personas { get; set; } = new List<Persona>();


        /*
                JSON'S
         */

        public static string OPJson => JsonConvert.SerializeObject(OpObs, Formatting.Indented);

        public static string EmpresasJson => JsonConvert.SerializeObject(EmpresasObs, Formatting.Indented);

        public static string ProductosJson => JsonConvert.SerializeObject(ProductoObs, Formatting.Indented);

        public static string PersonasJson => JsonConvert.SerializeObject(Personas, Formatting.Indented);


        /*
                LISTAS
         
         */

        public static IEnumerable<Arribos> SingleArriboCollection => new List<Arribos> { CurrentArribo };

        public static IEnumerable<OP> SingleOPCollections => new List<OP> { currentOP };




        public static Arribos SelectedArribo { get; set; } = new Arribos();
        public static Arribos CurrentArribo { get; set; } = new Arribos();


        public static OP createdOP { get; set; } = new OP();

        //public static OP currentOP { get; set; } = new OP();

        public static OP newOP { get; set; } = new OP();

        public static int finalUnidades { get; set; }
        public static string gettedUnidades { get; set; }
        public static string SelectedEmpresa { get; set; }
        public static string SelectedProducto { get; set; }


   

    }


}
