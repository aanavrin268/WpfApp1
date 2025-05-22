using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

        }

        private void goToUpdate(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UpdatePage());

        }

        private void goToSearch(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BuscarPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e )
        {
            this.NavigationService.Navigate(new InsertPage());
        }

      
    }
}
