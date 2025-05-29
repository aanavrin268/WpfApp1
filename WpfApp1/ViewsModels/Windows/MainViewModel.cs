using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewsModels.Windows
{
    public partial class MainViewModel: ObservableObject
    {

        [ObservableProperty]
        private string _message = "Holaa, desde VM";
    }
}
