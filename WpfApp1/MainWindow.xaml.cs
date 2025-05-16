using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Persona> _personas = new List<Persona>();
        private List<Persona> list_persona;
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new HomePage());

            this.Loaded += async (sender, e) => await InitDataAsyncSimply();

            //cb_nombre.Items.Add("Juan");
            //cb_nombre.Items.Add("Maria");
            //cb_nombre.Items.Add("Caros");

            list_persona = new List<Persona>
            {
                new Persona { Nombre = "uno", Ciudad = "CDMX", Edad = 34 },
                new Persona { Nombre = "dos", Ciudad = "CDMX", Edad = 30 }
            };

            //cb_nombre_2.ItemsSource = list_persona;
            //cb_nombre_2.DisplayMemberPath = "Nombre";
            //cb_nombre_2.SelectedValuePath = "Nombre";
           

        }


        private async Task InitDataAsyncSimply()
        {
            string excelPath = @"C:\excel\Libro1.xlsx";
            string hojaExcel = "Hoja1";

            try
            {
                LoaderControl.Visibility = Visibility.Visible;

                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var personasTemp = new List<Persona>();

                        foreach (var row in filas)
                        {
                            personasTemp.Add(new Persona
                            {
                                Nombre = row.Cell(1).GetString(),
                                Edad = row.Cell(2).GetValue<int>(),
                                Ciudad = row.Cell(3).GetString()
                            });
                        }

                        // Actualizar la colección en el hilo principal
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _personas.Clear();
                            _personas.AddRange(personasTemp);
                        });
                    }
                });

                Debug.WriteLine(JsonConvert.SerializeObject(_personas, Formatting.Indented));

                var autoCloseMsg = new AutoCloseMessageBox(
                    "Datos cargados con éxito!",
                    "Éxito",
                    1000
                    );
                //autoCloseMsg.Show();
            }
            catch (Exception ex)
            {

                //prueb a de commit 
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error");
            }
            finally
            {
                LoaderControl.Visibility = Visibility.Collapsed;
            }
        }


        private async Task InitDataAsync()
        {
            string excelPath = @"C:\excel\Libro1.xlsx";
            string hojaExcel = "Hoja1";

            try
            {
                _personas.Clear();

                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var rangoUsado = worksheet.RangeUsed();
                        var filas = rangoUsado.Rows().Skip(1);

                        foreach(var row in filas)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                _personas.Add(new Persona
                                {
                                    Nombre = row.Cell(1).GetString(),
                                    Edad = row.Cell(2).GetValue<int>(),
                                    Ciudad = row.Cell(3).GetString()
                                });
                            });
                        }    
                    }
                });

                string json = JsonConvert.SerializeObject(_personas, Formatting.Indented);

                Debug.WriteLine("Datos fetched desde Excel...");
                Debug.WriteLine(json);
                MessageBox.Show("Datos cargados con éxito!", "Éxito");

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error");
            }
        }


        private void cb_nombre_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(cb_nombre_2.SelectedItem is Persona personaSeleccionada)
            //{
                //MessageBox.Show($"Ciudad: {personaSeleccionada.Ciudad}");
            //}
        }

        private void btn_save_excel_Click(object sender, RoutedEventArgs e)
        {
            string excelPath = @"C:\excel\Libro1.xlsx";
            string hojaExcel = "Hoja1";

            try
            {
                using (var workbook = new XLWorkbook (excelPath))
                {
                    var worksheet = workbook.Worksheet(hojaExcel);

                    var filasABorrar = worksheet.RowsUsed().Skip(1).Reverse();

                    foreach(var fila in filasABorrar)
                    {
                        fila.Delete();
                    }

                    int filaInicio = 2;
                    foreach(var persona in _personas)
                    {
                        worksheet.Cell(filaInicio, 1).Value = persona.Nombre;
                        worksheet.Cell(filaInicio, 2).Value = persona.Edad;
                        worksheet.Cell(filaInicio, 3).Value = persona.Ciudad;
                        filaInicio++;
                    }

                    workbook.Save();

                    Debug.WriteLine("Datos guardados en excel!");
                    MessageBox.Show("Datos guardados correctamente en exlce!", "Exito");

                }
            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al guardar el libro: {ex.Message}", "Error");

            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _personas.Add(new Persona
                {
                    Nombre = "Juliana",
                    Edad = 99,
                    Ciudad = "CDMX1"
                });

                string jsonActualizado = JsonConvert.SerializeObject(_personas, Formatting.Indented);
                Debug.WriteLine("Json con nuevo registro: ");
                Debug.WriteLine(jsonActualizado);
                MessageBox.Show("Registro agreado al sjson", "Exito");
            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al agregar registro: {ex.Message}", "Error");
            }
        }

        private void btn_excel_Click(object sender, RoutedEventArgs e)
        {
            string excelPath = @"C:\excel\Libro1.xlsx";
            string hojaExcel = "Hoja1";

            try
            {
                _personas.Clear();
                using (var workbook = new XLWorkbook(excelPath ))
                {
                    var worksheet = workbook.Worksheet(hojaExcel);
                    var rangoUsado = worksheet.RangeUsed();

                    var filas = rangoUsado.Rows().Skip(1);

                    foreach(var row in filas)
                    {
                        _personas.Add(new Persona
                        {
                           Nombre = row.Cell(1).GetString(),
                           Edad = row.Cell(2).GetValue<int>(),
                           Ciudad = row.Cell(3).GetString()
                        });
                    }
                 
                }

                string json = JsonConvert.SerializeObject(_personas, Formatting.Indented);

                Debug.WriteLine("Datos fetched desde excel...");
                Debug.WriteLine(json);
                MessageBox.Show("Datos cargados con exito!", "Exito");

            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos:  {ex.Message}", "Error");
            }
        }

       

      
    }
}