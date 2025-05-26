using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {


        public UpdatePage()
        {
            InitializeComponent();

            AppData.CurrentOPChanged += AppData_CurrentOPChanged;

            //FTOP0002



        }

        /*
        protected override void OnClosed(EventArgs e)
        {
            AppData.CurrentOPChanged -= AppData_CurrentOPChanged;
            base.OnClosed(e);
        }

        */

        private void AppData_CurrentOPChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (AppData.currentOP != null)
                {
                    myDataGrid.ItemsSource = null;
                    myDataGrid.ItemsSource = AppData.currentOP.Arribos;
                }
            });
        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            string inputValue = tb_input.Text?.Trim();

            if (string.IsNullOrEmpty(inputValue))
            {
                MessageBox.Show("Error, ingresa algo para buscar", "Error");
                return;
            }

            var resultados = AppData.initialListOP
                .Where(op =>
                    (op.FolioOP?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) == true) ||
                    (op.Op?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) == true))
                .ToList();

            if (resultados.Any())
            {
                MessageBox.Show($"Se encontraron {resultados.Count} coincidencias", "Éxito");

                // Debug logs
                var resultadosJson = JsonConvert.SerializeObject(resultados, Formatting.Indented);
                Debug.WriteLine($"LOS RESULTADOS SON: {resultadosJson}");

                foreach (var op in resultados)
                {
                    Debug.WriteLine($"FolioOP: {op.FolioOP}, OP: {op.Op}");
                }

                // Actualización optimizada de currentOP
                var primerResultado = resultados.First();
                var nuevoOP = new OP
                {
                    FolioOP = primerResultado.FolioOP,
                    Op = primerResultado.Op,
                    Nombre = primerResultado.Nombre,
                    Id_Sistema = primerResultado.Id_Sistema,
                    Empresa = primerResultado.Empresa,
                    Unidades = primerResultado.Unidades,
                    Fecha_Plan = primerResultado.Fecha_Plan,
                    Fecha_OP = primerResultado.Fecha_OP
                };

                // Copia los Arribos manteniendo la ObservableCollection
                nuevoOP.Arribos = new ObservableCollection<Arribos>(primerResultado.Arribos);

                // Asignación final que disparará las notificaciones
                AppData.currentOP = nuevoOP;

                // Debug log final
                Debug.WriteLine($"Current OP actualizado: {JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented)}");
            }
            else
            {
                MessageBox.Show("No se encontraron coincidencias", "Información");
            }
        }
    }
}
