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
    /// Lógica de interacción para BuscarPage.xaml
    /// </summary>
    public partial class BuscarPage : Page
    {
        public BuscarPage()
        {
            InitializeComponent();

            dp_plan.BlackoutDates.AddDatesInPast();
            dp_op.BlackoutDates.AddDatesInPast();
            
            cb_nombre.ItemsSource = AppData.ProductoObs
                .Where(p => !string.IsNullOrEmpty(p.Nombre))
                .Select(p => p.Nombre).ToList();

            var dataJson = AppData.ProductoObs.ToList();
            var jsonString = JsonConvert.SerializeObject(dataJson, Formatting.Indented);

            cb_empresa.ItemsSource = AppData.EmpresasObs.Select(e => e.Nombre).ToList();


            Debug.WriteLine("datos cargados de produtos: ", jsonString);

            Debug.WriteLine("datos de empresas: ", AppData.EmpresasJson);

        }

        private void dp_plan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp_plan.SelectedDate.HasValue && dp_plan.SelectedDate.Value < DateTime.Today)
            {
                MessageBox.Show("Error, ingresa una fecha válida", "Error");
                dp_plan.SelectedDate = DateTime.Today; 
            }
        }

        private void onCancel(object sender, RoutedEventArgs e)
        {
            tb_unidades.Text = string.Empty;
            cb_nombre.SelectedItem = null;
            cb_id.SelectedItem = null;
            cb_empresa.SelectedItem = null;
        }

        private bool validateFields()
        {

            AppData.gettedUnidades = tb_unidades.Text;


            if (cb_nombre.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto", "Error");
                return false;
            }

            if (cb_id.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un ID Sistema", "Error");
                return false;
            }

            if (cb_empresa.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una empresa", "Error");
                return false;
            }

            if (dp_plan.SelectedDate == null)
            {
                MessageBox.Show("Selecciona una fecha de planeación", "Error");
                return false ;
            }


            if (dp_op.SelectedDate == null)
            {
                MessageBox.Show("Selecciona una fecha de OP", "Error");
                return false;
            }

            if (string.IsNullOrEmpty(AppData.gettedUnidades))
            {
                MessageBox.Show("Ingresa las unidades");
                return false;
            }

            if(!double.TryParse(AppData.gettedUnidades, out double unidades))
            {
                MessageBox.Show("Error, ingresa un valor númerico válido", "Error");
                return false;
            }

            AppData.finalUnidades = (int)Math.Round(unidades);


            return true;
        }



        private void SaveRegister(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validateFields())
                {
                    MessageBox.Show("Todos los campos validadedos, ", "Exito");

                    AppData.newOP.Nombre = cb_nombre.SelectedItem as string;
                    AppData.newOP.Id_Sistema = cb_id.SelectedItem as string;
                    AppData.newOP.Empresa = cb_empresa.SelectedItem as string;
                    AppData.newOP.Unidades = AppData.finalUnidades;
                    AppData.newOP.Fecha_Plan = dp_plan.SelectedDate.Value;
                    AppData.newOP.Fecha_OP = dp_op.SelectedDate.Value;


                    var jsonOP = JsonConvert.SerializeObject(AppData.newOP, Formatting.Indented);

                    Debug.WriteLine($"El nuevo registro es: {jsonOP}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


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
            if (cb_nombre.SelectedItem != null)
            {
                AppData.SelectedProducto = cb_nombre.SelectedItem as String;

                MessageBox.Show("elemento seleccionado y guardado: " + AppData.SelectedProducto);

                PopulateCBID();
            }
           
        }
    }
}
