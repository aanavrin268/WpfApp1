using ClosedXML.Excel;
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
using System.IO; 


namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para BuscarPage.xaml
    /// </summary>
    public partial class BuscarPage : Page, INotifyPropertyChanged
    {
        public bool _esVisible = false;

        public int uniqueFolios = 0;

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

            AppData.CurrentOPChanged += AppData_CurrentOPChanged;

            //Load Unique COunt
             uniqueFolios = AppData.initialListOP
                .Select(op => op.FolioOP)
                .Distinct()
                .Count();

            //MessageBox.Show($"Unique Folios: {uniqueFolios.ToString()}");





            //AppData.currentOP.Nombre = "Pruebasss";
            //AppData.currentOP.Id_Sistema = "100.2200.20";

            //AppData.ListaOps.Add(AppData.currentOP);

            var jsonTest = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);


            Debug.WriteLine($"Tes acutal de prueba {jsonTest}");

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

        private void AppData_CurrentOPChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if(AppData.currentOP != null)
                {
                    dtg_buscar.ItemsSource = null;
                    dtg_buscar.ItemsSource = AppData.currentOP.Arribos;
                }
            });
        }




        private void onFinishTask(object sender, RoutedEventArgs e)
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDOP";

            try
            {
                using (var workbook = File.Exists(excelPath)
                    ? new XLWorkbook(excelPath)
                    : new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == hojaExcel)
                                  ?? workbook.Worksheets.Add(hojaExcel);

                    if (worksheet.RowsUsed().Count() == 0)
                    {
                        string[] headers = { "FolioOP", "Op", "Nombre", "Id_Sistema", "Empresa", "Unidades",
                                   "Fecha_plan", "Fecha_op", "FolioArribo", "UnidadesArribo", "Status", "Causal" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }
                    }

                    int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;

                    foreach (var op in AppData.ListaOps)
                    {
                        foreach (var arribo in op.Arribos)
                        {
                            worksheet.Cell(lastRow, 1).Value = op.FolioOP;
                            worksheet.Cell(lastRow, 3).Value = op.Nombre;
                            worksheet.Cell(lastRow, 4).Value = op.Id_Sistema;
                            worksheet.Cell(lastRow, 5).Value = op.Empresa;
                            worksheet.Cell(lastRow, 6).Value = op.Unidades;
                            worksheet.Cell(lastRow, 7).Value = op.Fecha_Plan;
                            worksheet.Cell(lastRow, 8).Value = op.Fecha_OP;

                            worksheet.Cell(lastRow, 9).Value = arribo.Folio;
                            worksheet.Cell(lastRow, 10).Value = arribo.Unidades;
                            worksheet.Cell(lastRow, 11).Value = arribo.Status;
                            worksheet.Cell(lastRow, 12).Value = arribo.Causal;

                            lastRow++;
                        }
                    }

                    workbook.SaveAs(excelPath);
                }

                MessageBox.Show("Datos exportados con éxito (1 fila por Arribo).");



                AppData.ListaOps.Clear();
                //Go the other page

                this.NavigationService.Navigate(new HomePage());



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void onInsertRegister(object sender, RoutedEventArgs e )
        {
            var newValue = 0;
            var newFolio = "";

            if(uniqueFolios > 0)
            {
                newValue = uniqueFolios + 1;
            }else
            {
                newValue = 1;
            }

            newFolio = "FTOP000" + newValue.ToString();

                var nuevoRegistro = new OP()
                {
                    FolioOP =  newFolio,
                    Nombre = cb_nombre.SelectedItem as string,
                    Id_Sistema = cb_id.SelectedItem as string,
                    Empresa = cb_empresa.SelectedItem as string,
                    Unidades = AppData.finalUnidades,
                    Fecha_Plan = dp_plan.SelectedDate.Value,
                    Fecha_OP = dp_op.SelectedDate.Value
                };

     
            nuevoRegistro.Arribos.Clear();

            foreach (var arribo in AppData.currentOP.Arribos)
            {
                nuevoRegistro.Arribos.Add(arribo);
            }

            AppData.ListaOps.Add(nuevoRegistro);

            AppData.createdOP = new OP();
            AppData.currentOP = new OP();

            var currentJson = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);
            var listaJson = JsonConvert.SerializeObject(AppData.ListaOps, Formatting.Indented);

            Debug.WriteLine($"current op final: {currentJson}");
            Debug.WriteLine($"Currnet lista final: {listaJson}");


            //Clan data
            EsVisible = false;

            ResetAllFields();

        }

        private void EditArribo()
        {
            var parentWindow = Window.GetWindow(this);

            var modal = new EditArriboWindow();
            modal.Owner = parentWindow;

            if(modal.ShowDialog() == true)
            {

            }
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
            ResetAllFields();
        }


        private void ResetAllFields()
        {
            tb_unidades.Text = string.Empty;
            cb_nombre.SelectedItem = null;
            cb_id.SelectedItem = null;
            cb_empresa.SelectedItem = null;
            dp_plan.SelectedDate = null;
            dp_op.SelectedDate = null;

            if (AppData.currentOP.Arribos.Count > 0)
            {
                MessageBox.Show($"Si hay arribos: {AppData.currentOP.Arribos.Count}");
                EsVisible = false;

                // Reiniciar las propiedades de currentOP en lugar de crear uno nuevo
                AppData.currentOP.FolioOP = string.Empty;
                AppData.currentOP.Nombre = string.Empty;
                AppData.currentOP.Id_Sistema = string.Empty;
                AppData.currentOP.Empresa = string.Empty;
                AppData.currentOP.Unidades = 0;
                AppData.currentOP.Fecha_Plan = default;
                AppData.currentOP.Fecha_OP = default;
                AppData.currentOP.Arribos.Clear();
                AppData.currentOP.Arribos.Add(new Arribos());

                var jsonCurrent = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);
                Debug.WriteLine($"Current OP después del reset: {jsonCurrent}");

                var jsonList = JsonConvert.SerializeObject(AppData.ListaOps, Formatting.Indented);
                Debug.WriteLine($"Current LISTOP después del reset: {jsonList}");
            }
            else
            {
                MessageBox.Show("No hay arribos aún");
            }
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
            var folioOP = "FTOP0001";


            try
            {
                if (validateFields())
                {
                    MessageBox.Show("Todos los campos validadedos, ", "Exito");

                    AppData.currentOP.Arribos.Clear();

                    AppData.currentOP.FolioOP = folioOP;
                    AppData.currentOP.Nombre = cb_nombre.SelectedItem as string;
                    AppData.currentOP.Id_Sistema = cb_id.SelectedItem as string;
                    AppData.currentOP.Empresa = cb_empresa.SelectedItem as string;
                    AppData.currentOP.Unidades = AppData.finalUnidades;
                    AppData.currentOP.Fecha_Plan = dp_plan.SelectedDate.Value;
                    AppData.currentOP.Fecha_OP = dp_op.SelectedDate.Value;

                    AppData.currentOP.Arribos.Add(new Arribos());

                    var jsonOP = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);

                    Debug.WriteLine($"El nuevo registro es: {jsonOP}");


                 

                    //INSERT INTO LIST
                    //AppData.OpObs.Add(AppData.newOP);

                    //AppData.ListaOps.Add(AppData.createdOP);
                    //AppData.currentOP = AppData.newOP;

                    //lista debu

                    var jsonLista = JsonConvert.SerializeObject(AppData.ListaOps, Formatting.Indented);

                    Debug.WriteLine($"La lista actual es: {jsonLista}");


                    EsVisible = true;

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

                dynamic selectedItem = cb_arribo.SelectedItem;
                var selectedFolio = selectedItem.Folio;

                Debug.WriteLine($"El folio seleccionado es: {selectedFolio}");

                //var jsoncurrentOP = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);

                //Debug.WriteLine($"El current OP es: {jsoncurrentOP}");

                var selectedArribo = AppData.currentOP.Arribos.FirstOrDefault(a => a.Folio == selectedFolio);

                if (selectedArribo != null)
                {
                    MessageBox.Show($"Arribo encontrado!: {selectedArribo.Folio}", "Correcto");

                    //Mostrar la tabla si existe el arribo
                    AppData.CurrentArribo = selectedArribo;

                    EditArribo();

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
