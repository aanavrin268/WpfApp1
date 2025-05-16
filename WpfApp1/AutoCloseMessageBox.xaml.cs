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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para AutoCloseMessageBox.xaml
    /// </summary>
    public partial class AutoCloseMessageBox : Window
    {
        public AutoCloseMessageBox(string message, string title, int closeAfterMiliSeconds)
        {
            InitializeComponent();

            this.Title = title;
            this.MessageText.Text = message;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(closeAfterMiliSeconds);
            timer.Tick += (sender, e) => { timer.Stop(); this.Close(); };
            timer.Start();
        }

        public string Message { get; set; }
    }
}
