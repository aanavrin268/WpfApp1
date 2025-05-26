using MahApps.Metro.Controls;
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
using System.Windows.Media.Animation;
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

            /*
            // 1. Configura la nueva página
            var newPage = new BuscarPage();
            newPage.RenderTransform = new TranslateTransform();
            newPage.Opacity = 0; // Inicia con opacidad 0 para la animación de entrada

            // 2. Configura la página actual
            this.RenderTransform = new TranslateTransform();

            // 3. Obtén la animación de salida
            var exitAnim = (Storyboard)Application.Current.Resources["SlideOutToRight"];
            if (exitAnim == null)
            {
                NavigationService.Navigate(newPage);
                return;
            }

            // 4. Clona y configura la animación de salida
            exitAnim = exitAnim.Clone();
            Storyboard.SetTarget(exitAnim, this);

            // 5. Maneja la finalización de la animación de salida
            exitAnim.Completed += (s, _) =>
            {
                // Navega a la nueva página
                NavigationService.Navigate(newPage);

                // 6. Obtén y ejecuta la animación de entrada
                var enterAnim = (Storyboard)Application.Current.Resources["SlideInFromLeft"];
                if (enterAnim != null)
                {
                    enterAnim = enterAnim.Clone();
                    Storyboard.SetTarget(enterAnim, newPage);
                    newPage.BeginStoryboard(enterAnim);
                }
            };

            // 7. Inicia la animación de salida
            BeginStoryboard(exitAnim);

            */
        }


        private void Button_Click(object sender, RoutedEventArgs e )
        {
            this.NavigationService.Navigate(new InsertPage());
        }

      
    }
}
