using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
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

            this.Loaded += async (sender, e) => await InitProductsDataAsync();

            this.Loaded += async (sender, e) => await InitCompanysDataAsync();

            this.Loaded += async (sender, e) => await InitBDOrdersDataAsync();

            this.Loaded += async (sender, e) => await InitBDCatalogoDataAsync();

            //this.Loaded += async (sender, e) => await InitBDOPFormattedDataAsync();

            //this.Loaded += async (sender, e) => await InitBDOPDataAsync();

            this.Loaded += async (sender, e) => await InitBDArribosDataAsync();





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


        private async Task InitBDOPFormattedDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDOPOC";

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);

                        var opDictionary = new Dictionary<string, OPFormatted>();

                        foreach (var row in filas)
                        {
                            var folioOP = row.Cell(1).GetString();
                            var idSistema = row.Cell(4).GetString(); 

                            var arribo = new Arribos
                            {
                                Folio = row.Cell(9).GetString(),
                                Unidades = row.Cell(10).GetValue<int>(),
                                Status = row.Cell(11).GetString(),
                                Causal = row.Cell(12).GetString()
                            };

                            if (opDictionary.TryGetValue(folioOP, out var existingOp))
                            {
                                var existingProduct = existingOp.Products.FirstOrDefault(p => p.Id_Sistema == idSistema);

                                if (existingProduct != null)
                                {
                                    // Si el producto ya existe, solo añadimos el arribo
                                    existingProduct.Arribos.Add(arribo);
                                }
                                else
                                {
                                    // Si no existe, creamos un nuevo producto
                                    var newProduct = new Productss
                                    {
                                        Nombre = row.Cell(3).GetString(),
                                        Id_Sistema = idSistema,
                                        Empresa = row.Cell(5).GetString(),
                                        Unidades = row.Cell(6).GetValue<int>(),
                                        Fecha_Plan = row.Cell(7).GetDateTime(),
                                        Fecha_OP = row.Cell(8).GetDateTime(),
                                        Arribos = new ObservableCollection<Arribos> { arribo }
                                    };
                                    existingOp.Products.Add(newProduct);
                                }
                            }
                            else
                            {
                                // Si no existe la OP, creamos una nueva con su primer producto
                                var newProduct = new Productss
                                {
                                    Nombre = row.Cell(3).GetString(),
                                    Id_Sistema = idSistema,
                                    Empresa = row.Cell(5).GetString(),
                                    Unidades = row.Cell(6).GetValue<int>(),
                                    Fecha_Plan = row.Cell(7).GetDateTime(),
                                    Fecha_OP = row.Cell(8).GetDateTime(),
                                    Arribos = new ObservableCollection<Arribos> { arribo }
                                };

                                var newOp = new OPFormatted
                                {
                                    FolioOp = folioOP,
                                    Op = row.Cell(2).GetString(),
                                    Products = new ObservableCollection<Productss> { newProduct }
                                };
                                opDictionary.Add(folioOP, newOp);
                            }
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.initialFormattedOP.Clear();
                            foreach (var op in opDictionary.Values)
                            {
                                AppData.initialFormattedOP.Add(op);
                            }
                        });
                    }
                });

                var jsonInit = JsonConvert.SerializeObject(AppData.initialFormattedOP, Formatting.Indented);
                Debug.WriteLine($"Contenido inicial FORMATEADO: : {jsonInit}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de la BD OP: {ex.Message}", "Error");
            }
        }

        private async Task InitBDOPDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDOPOC";

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);

                        // Primero creamos un diccionario para agrupar por FolioOP
                        var opDictionary = new Dictionary<string, OP>();

                        foreach (var row in filas)
                        {
                            var folioOP = row.Cell(1).GetString();

                            // Creamos el objeto Arribo para cada fila
                            var arribo = new Arribos
                            {
                                Folio = row.Cell(9).GetString(),
                                Unidades = row.Cell(10).GetValue<int>(),
                                Status = row.Cell(11).GetString(),
                                Causal = row.Cell(12).GetString()
                            };

                            // Si ya existe un OP con este FolioOP, solo agregamos el Arribo
                            if (opDictionary.TryGetValue(folioOP, out var existingOp))
                            {
                                existingOp.Arribos.Add(arribo);
                            }
                            else
                            {
                                // Si no existe, creamos un nuevo OP con su primer Arribo
                                var newOp = new OP
                                {
                                    FolioOP = folioOP,
                                    Op = row.Cell(2).GetString(),
                                    Nombre = row.Cell(3).GetString(),
                                    Id_Sistema = row.Cell(4).GetString(),
                                    Empresa = row.Cell(5).GetString(),
                                    Unidades = row.Cell(6).GetValue<int>(),
                                    Fecha_Plan = row.Cell(7).GetDateTime(),
                                    Fecha_OP = row.Cell(8).GetDateTime(),
                                    Arribos = new ObservableCollection<Arribos> { arribo }
                                };
                                opDictionary.Add(folioOP, newOp);
                            }
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.initialListOP.Clear();
                            foreach (var op in opDictionary.Values)
                            {
                                AppData.initialListOP.Add(op);
                            }
                        });
                    }
                });

                var jsonInit = JsonConvert.SerializeObject(AppData.initialListOP, Formatting.Indented);
                Debug.WriteLine($"Contenido inicial de la bd op: {jsonInit}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de la BD OP: {ex.Message}", "Error");
            }
        }


        private async Task InitBDCatalogoDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDCatalogo";

            try
            {
                await Task.Run(() =>
                {

                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var dataTemp = new List<Pt>();

                        foreach (var row in filas)
                        {
                            dataTemp.Add(new Pt
                            {
                                Id_sistema = row.Cell(1).GetString(),
                                Clave = row.Cell(2).GetString(),
                                Proveedor = row.Cell(3).GetString(),
                                Descripcion = row.Cell(4).GetString()
                            });
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.initialPtLists.Clear();
                            foreach (var pt in dataTemp)
                            {
                                AppData.initialPtLists.Add(pt);
                            }
                        });
                    }
                });

                var jsonList = JsonConvert.SerializeObject(AppData.initialPtLists, Formatting.Indented);
                Debug.WriteLine($"La lista de pts es: {jsonList}");

            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de los pts: {ex.Message}", "Error");
            }
        }


        private async Task InitBDOrdersDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDOPOC";

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var dataTemp = new List<OP>();

                        foreach (var row in filas)
                        {
                            dataTemp.Add(new OP { 
                                FolioOP = row.Cell(1).GetString(),
                                Op = row.Cell(2).GetString(),
                                Nombre = row.Cell(3).GetString(),
                                Id_Sistema = row.Cell(4).GetString(),
                                Descripcion = row.Cell(5).GetString(),
                                ProveedorDesc = row.Cell(6).GetString(),
                                Empresa = row.Cell(7).GetString(),
                                Costo = row.Cell(8).GetValue<int>(),
                                Unidades = row.Cell(9).GetValue<int>(),
                                MontoOrden = row.Cell(10).GetValue<int>(),
                                Moneda = row.Cell(11).GetString(),
                                Incoterms = row.Cell(12).GetString(),
                                CComerciales = row.Cell(13).GetString(),
                                Fecha_Plan = row.Cell(14).GetDateTime(),
                                Fecha_OP = row.Cell(15).GetDateTime(),
                                TotalPzsArribos = row.Cell(16).GetValue<int>(),
                                StatusOrden = row.Cell(17).GetString(),
                                TipoProducto = row.Cell(18).GetString(),

                            });
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.initialListOP.Clear();
                            foreach (var op in dataTemp)
                            {
                                AppData.initialListOP.Add(op);
                            }
                        });
                    }
                });

                var jsonInit = JsonConvert.SerializeObject(AppData.initialListOP, Formatting.Indented);
                Debug.WriteLine($"El nuevo OP list es: {jsonInit}");


            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de los arribos: {ex.Message}", "Error");
            }
        }

        private async Task InitBDArribosDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDArribos";

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var dataTemp = new List<Arribos>();

                        foreach (var row in filas)
                        {
                            dataTemp.Add(new Arribos
                            {
                                IdArribo = row.Cell(1).GetString(),
                                Folio = row.Cell(2).GetString(),
                                FolioOrden = row.Cell(3).GetString(),
                                Proveedor = row.Cell(4).GetString(),
                                Status = row.Cell(9).GetString(),
                                Causal = row.Cell(10).GetString(),
                                EnvioFabrica = row.Cell(11).GetDateTime(),
                                AduanaAnalisis = row.Cell(12).GetDateTime(),
                                FechaAlmacen = row.Cell(13).GetDateTime(),
                                LiberacionWms = row.Cell(14).GetDateTime(),
                                Unidades = row.Cell(15).GetValue<int>(),
                                Retencion = row.Cell(16).GetValue<int>(),
                                NoConforme = row.Cell(17).GetValue<int>(),
                                DisponibleWms = row.Cell(18).GetValue<int>(),
                                NoLotes = row.Cell(19).GetValue<int>(),
                                NoSemana = row.Cell(20).GetValue<int>(),
                                LastUpdates = row.Cell(21).GetDateTime()


                            });
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.ArribosObs.Clear();
                            foreach (var arrivo in dataTemp)
                            {
                                AppData.ArribosObs.Add(arrivo);
                            }
                        });
                    }
                });

                var arribosObsJson = JsonConvert.SerializeObject(AppData.ArribosObs, Formatting.Indented);
                Debug.WriteLine($"Contenido de ArribosObs: {arribosObsJson}");
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de los arribos: {ex.Message}", "Error");
            }
        }



        private async Task InitCompanysDataAsync()
        {
            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDEmpresas";

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var dataTemp = new List<Empresa>();

                        foreach(var row in filas)
                        {
                            dataTemp.Add(new Empresa
                            {
                                Nombre = row.Cell(2).GetString()
                            });
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.EmpresasObs.Clear();
                            foreach(var empresa in dataTemp)
                            {
                                AppData.EmpresasObs.Add(empresa);
                            }
                        });
                    }
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar los datos de las empresas:  {ex.Message}", "Error");
            }
        }








        private async Task InitProductsDataAsync()
        {

            string excelPath = @"C:\excel\BD.xlsx";
            string hojaExcel = "BDProductos";

            try
            {

                await Task.Run(() => {
                    using (var workbook = new XLWorkbook(excelPath))
                    {
                        var worksheet = workbook.Worksheet(hojaExcel);
                        var filas = worksheet.RangeUsed().Rows().Skip(1);
                        var dataTemp = new List<Producto>();

                        foreach(var row in filas)
                        {
                            dataTemp.Add(new Producto
                            {
                                Nombre = row.Cell(4).GetString(),
                                Id_sistema = row.Cell(1).GetString(),
                                Clave = row.Cell(2).GetString(),  
                                Descripcion = row.Cell(3).GetString()
                            });
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.ProductoObs.Clear();
                            foreach(var producto in dataTemp)
                            {
                                AppData.ProductoObs.Add(producto);
                            }
                        });

                     
                    }
                });

            }catch(Exception ex )
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error al cargar datos de produtos: {ex.Message}", "Error");
            }
        }

        private async Task InitDataAsyncSimply()
        {
            string excelPath = @"C:\excel\BD.xlsx";
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
                            //_personas.Clear();
                            //_personas.AddRange(personasTemp);
                            //AppData.Personas.Clear();
                            //AppData.Personas.AddRange(personasTemp);
                            AppData.PersonasObs.Clear();
                            foreach (var persona in personasTemp)
                            {
                                AppData.PersonasObs.Add(persona);
                            }
                        });
                    }
                });

                Debug.WriteLine(AppData.PersonasJson);

                var autoCloseMsg = new AutoCloseMessageBox(
                    "Datos cargados con éxito!",
                    "Éxito",
                    1000
                    );
                //autoCloseMsg.Show();
            }
            catch (Exception ex)
            {

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