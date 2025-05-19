using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Page
    {
        public InsertPage()
        {
            InitializeComponent();
            //loadData();
            cb_nombre.ItemsSource = AppData.ProductoObs.Select(p => p.Nombre).ToList();

            var dataJson = AppData.ProductoObs.ToList();
            var jsonString = JsonConvert.SerializeObject(dataJson, Formatting.Indented);


            Debug.WriteLine("datos cargados de produtos: ", jsonString);
        }

        private void PopulateCBID()
        {
            //cb_id.ItemsPanel = AppData.ProductoObs.Select(p)
            if (string.IsNullOrEmpty(AppData.SelectedProducto))
            {
                cb_id.ItemsSource = null;
                return;
            }

            var ids = AppData.ProductoObs
                .Where(p => !string.IsNullOrEmpty(p.Descripcion) &&
                p.Descripcion.Contains(AppData.SelectedProducto, StringComparison.OrdinalIgnoreCase))
                .Select(p => p.Id_sistema)
                .ToList();

            cb_id.ItemsSource = ids;

            if (ids.Any())
            {
                MessageBox.Show($"Se encontaron: {ids.Count}");
            }
            else
            {
                MessageBox.Show($"No se encontraron ids para  {AppData.SelectedProducto}");
            }
        }


        private void CbNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               if(cb_nombre.SelectedItem != null)
            {
                AppData.SelectedProducto = cb_nombre.SelectedItem as String;

                MessageBox.Show("elemento seleccionado y guardado: " + AppData.SelectedProducto);

                PopulateCBID();



            }else
            {
                MessageBox.Show("Erorr, formato raro");
            }
        }


        private void loadData()
        {
            var allData = AppData.Personas;

            if(allData == null || !allData.Any())
            {
                Debug.WriteLine("No hay datos para mostrar!");
                cb_nombre.ItemsSource = new List<string> { "No hay datos disponibles" };
                return;
            }

            var nombres = allData.Select(p => p.Nombre).ToList();
            cb_nombre.ItemsSource = nombres;

            string json = AppData.PersonasJson;
            Debug.WriteLine("Los datos a trabajar son: "+ json);
        }
    }
}
