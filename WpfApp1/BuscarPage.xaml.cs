using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class BuscarPage : Page, INotifyPropertyChanged
    {
        public bool _esVisible = false;

        public bool EsVisible
        {
            get => _esVisible;
            set
            {
                if(_esVisible != value)
                {
                    _esVisible = value;
                    OnPropertyChanged(nameof(EsVisible));
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BuscarPage()
        {
            InitializeComponent();
            DataContext = this;

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


        private void AddNewArribo(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);

            var modal = new ArriboModalWindow();
            modal.Owner = parentWindow;

            if(modal.ShowDialog() == true)
            {
                //App.curre
            }
        }

        private void ChangeVisibility(object sender, RoutedEventArgs e)
        {
            EsVisible = !EsVisible;
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
            var  arriboSize = 0;



            try
            {
                if (validateFields())
                {
                    MessageBox.Show("Todos los campos validadedos, ", "Exito");

                    AppData.newOP.Arribos.Clear();

                    AppData.currentOP.Nombre = cb_nombre.SelectedItem as string;
                    AppData.currentOP.Id_Sistema = cb_id.SelectedItem as string;
                    AppData.currentOP.Empresa = cb_empresa.SelectedItem as string;
                    AppData.currentOP.Unidades = AppData.finalUnidades;
                    AppData.currentOP.Fecha_Plan = dp_plan.SelectedDate.Value;
                    AppData.currentOP.Fecha_OP = dp_op.SelectedDate.Value;

                    AppData.currentOP.Arribos.Add(new Arribos());

                    var jsonOP = JsonConvert.SerializeObject(AppData.newOP, Formatting.Indented);

                    Debug.WriteLine($"El nuevo registro es: {jsonOP}");

                    //INSERT INTO LIST
                    //AppData.OpObs.Add(AppData.newOP);

                    //AppData.currentOP = AppData.newOP;

                    Debug.WriteLine($"Cantidad de OPS: {AppData.OpObs.ToList().Count}");

                    EsVisible = !EsVisible;

                    //POPULATE THE CBOX ARRIBOS
                    //cb_arribo.ItemsSource = AppData.currentOP.Arribos.Select(a => a.Folio).ToList();


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


        private void CbArribo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cb_arribo.SelectedItem != null)
            {
                //Busscar el arribo por Folio en el array de arribos del currnet 
                var selectedFolio = cb_arribo.SelectedItem as string;

                var selectedArribo = AppData.newOP.Arribos.FirstOrDefault(a => a.Folio == selectedFolio);

                if(selectedArribo != null)
                {
                    MessageBox.Show($"Arribo encontrado!: {selectedArribo.Folio}", "Correcto");

                    //Mostrar la tabla si existe el arribo
                    AppData.CurrentArribo = selectedArribo;
                }
                else
                {
                    MessageBox.Show($"Error, folio no encontrado!: {selectedArribo.Folio}", "Error");

                }
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
