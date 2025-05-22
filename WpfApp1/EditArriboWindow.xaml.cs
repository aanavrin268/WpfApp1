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
    /// Lógica de interacción para EditArriboWindow.xaml
    /// </summary>
    public partial class EditArriboWindow : Window
    {
        public EditArriboWindow()
        {
            InitializeComponent();

            var units = AppData.CurrentArribo.Unidades;


            lb_units.Content = units.ToString();

            

        }

        private void onSave_Click(object sender, RoutedEventArgs e)
        {
            onUpdateValue2();
        }


        private void onUpdateValue2()
        {
            if (!double.TryParse(tb_new_units.Text, out double parsedDouble))
            {
                MessageBox.Show("Ingresa un número válido");
                return;
            }

            int newUnits = (int)parsedDouble;
            var folio = AppData.CurrentArribo.Folio;

            var arribo = AppData.currentOP.Arribos.FirstOrDefault(a => a.Folio == folio);
            if (arribo != null)
            {
                arribo.Unidades = newUnits; 

                Debug.WriteLine($"Unidades actualizadas para {folio}: {newUnits}");

                this.DialogResult = true;
                this.Close();
            }
        }

        private void onUpdateValue()
        {

            int newUnits = 0;

            if(double.TryParse(tb_new_units.Text, out double parsedDouble))
            {
                 newUnits = (int)parsedDouble;
            }
            else
            {
                MessageBox.Show("Ingresa un número válido");
            }

                //Encotnrar el indice y actualziar la propiedad.
                var folio = AppData.CurrentArribo.Folio;

            int index = AppData.currentOP.Arribos.ToList().FindIndex(a => a.Folio == folio);

            if(index != -1)
            {
                AppData.currentOP.Arribos[index].Unidades = newUnits;

                var jsonOP = JsonConvert.SerializeObject(AppData.currentOP, Formatting.Indented);

                Debug.WriteLine($"op actual: {jsonOP}");

            }

        }
    }
}
