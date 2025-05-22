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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para ArriboModalWindow.xaml
    /// </summary>
    public partial class ArriboModalWindow : Window
    {
        public ArriboModalWindow()
        {
            InitializeComponent();

            var jsonOP = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);
            Debug.WriteLine($"OP para ver datos de pruebas:   {jsonOP}");

            lb_units.Content = AppData.currentOP.Unidades;


        }


        private void onSave(object sender, RoutedEventArgs e)
        {
            var units = tb_na_units.Text;

            if(string.IsNullOrEmpty(units))
            {
                MessageBox.Show("Ingresa un valor para las unidades", "Error");
                return;
            }

            if (!double.TryParse(units, out double unidades))
            {
                MessageBox.Show("Error, ingresa un valor númerico válido", "Error");
                return;
            }

            var finalUnits = (int)Math.Round(unidades);

            if(finalUnits <= 0)
            {
                MessageBox.Show("Error, Ingresa una cantidad númerica válida", "Error");
                return;
            }


            if(finalUnits > AppData.currentOP.Unidades)
            {
                MessageBox.Show("Error, debes ingresar una cantidad menor al valor fincado de unidadess", "Error");
                return;
            }


            var newArribo = new Arribos();

            newArribo.Folio = "Arribo2";
            newArribo.Unidades = finalUnits;

            string jsons = JsonConvert.SerializeObject(newArribo, Formatting.Indented);

            Debug.WriteLine($"El nuevo arriboa  a insertar es: {jsons}");

            AppData.currentOP.Arribos.Add(newArribo);

            string opjson = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);

            Debug.WriteLine($"OP hasta el momento: {opjson}");

            MessageBox.Show($"Valor a ingresar: {finalUnits}");

        }


        private void onCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();

        }
    }
}
