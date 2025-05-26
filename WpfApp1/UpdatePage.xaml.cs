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

            AppData.CurrentOPFormattedChanged += OnCurrentOPFormattedChanged;
            this.Unloaded += (s, e) => AppData.CurrentOPFormattedChanged -= OnCurrentOPFormattedChanged;



        }


        private void tb_input_Keydown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                onSearchFormatted(sender, new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent, btn_search ));
            }
        }




        private void OnCurrentOPFormattedChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Forzar actualización de los bindings
                lb_folio.GetBindingExpression(ContentProperty)?.UpdateTarget();
            });
        }

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

        private void onSearchFormatted(object sender, RoutedEventArgs e)
        {
            string inputValue = tb_input.Text?.Trim();

            if (string.IsNullOrEmpty(inputValue))
            {
                MessageBox.Show("Error, ingresa algo para buscar", "Error");
                return;
            }

            // Debug: Verificar el contenido de initialFormattedOP
            Debug.WriteLine($"initialFormattedOP: {JsonConvert.SerializeObject(AppData.initialFormattedOP, Formatting.Indented)}");

            // Buscar solo por FolioOp u Op de la OP
            var resultados = AppData.initialFormattedOP
                .Where(op =>
                    (op.FolioOp?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) == true) ||
                    (op.Op?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) == true))
                .ToList();

            if (resultados.Any())
            {
                MessageBox.Show($"Se encontraron {resultados.Count} coincidencias", "Éxito");

                // Debug: Mostrar resultados
                var resultadosJson = JsonConvert.SerializeObject(resultados, Formatting.Indented);
                Debug.WriteLine($"LOS RESULTADOS SON: {resultadosJson}");

                foreach (var op in resultados)
                {
                    Debug.WriteLine($"FolioOP: {op.FolioOp}, OP: {op.Op}");
                }

                // Tomar el primer resultado
                var primerResultado = resultados.First();

                // Debug: Mostrar primerResultado
                Debug.WriteLine($"Primer resultado: {JsonConvert.SerializeObject(primerResultado, Formatting.Indented)}");

                // Crear una nueva OPFormatted con los datos del primer resultado
                var nuevoOP = new OPFormatted
                {
                    Op = primerResultado.Op,
                    FolioOp = primerResultado.FolioOp,
                    Products = new ObservableCollection<Productss>()
                };

                // Copiar todos los productos (y sus arribos) manteniendo las ObservableCollection
                foreach (var producto in primerResultado.Products)
                {
                    var nuevoProducto = new Productss
                    {
                        Nombre = producto.Nombre,
                        Id_Sistema = producto.Id_Sistema,
                        Empresa = producto.Empresa,
                        Unidades = producto.Unidades,
                        Fecha_Plan = producto.Fecha_Plan,
                        Fecha_OP = producto.Fecha_OP,
                        Arribos = new ObservableCollection<Arribos>(producto.Arribos)
                    };
                    nuevoOP.Products.Add(nuevoProducto);
                }

                // Debug: Mostrar nuevoOP antes de asignar
                Debug.WriteLine($"nuevoOP antes de asignar: {JsonConvert.SerializeObject(nuevoOP, Formatting.Indented)}");

                // Asignación final que disparará las notificaciones
                AppData.UpdateCurrentOP(nuevoOP);
                Reactividad.Instance.neoOp = nuevoOP;

                // Debug: Mostrar neoOp después de asignar
                var neoJsn = JsonConvert.SerializeObject(Reactividad.Instance.neoOp, Formatting.Indented);
                Debug.WriteLine($"NEOOP VALUE: {neoJsn}");




                //Populate ComboBOX


                cb_folios.ItemsSource = Reactividad.Instance.neoOp.Products.Select(p => p.Nombre).ToList();



            }
            else
            {
                MessageBox.Show("No se encontraron coincidencias", "Información");
            }
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
