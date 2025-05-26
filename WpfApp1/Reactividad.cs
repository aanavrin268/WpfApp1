using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class Reactividad : INotifyPropertyChanged
    {
        // Singleton: única instancia de Reactividad
        private static readonly Reactividad _instance = new Reactividad();
        public static Reactividad Instance => _instance;

        private OPFormatted _neoOp = new OPFormatted();

        public OPFormatted neoOp
        {
            get => _neoOp;
            set
            {
                if (_neoOp != value)
                {
                    _neoOp = value;
                    OnPropertyChanged();
                }
            }
        }

        // Evento no estático para cumplir con INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor privado para el singleton
        private Reactividad() { }
    }
}