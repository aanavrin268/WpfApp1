using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Readebles
    {


        /*  list observables  CAUSALES */

        public static ObservableCollection<string> StatusList { get; } = new ObservableCollection<string>
    {
        "PLANEACION",
        "FABRICACION",
        "ALMACEN",
        "LIBERADO"
    };



        public static ObservableCollection<string> CausalFabricaPlaneacion { get; } = new ObservableCollection<string>
        {
            "EN PLANEACION",
            "MATERIALES COMPLETOS PARA PRODUCCION",
            "MANTENIMIENTO DE MAQUINARIA",
            "CLIMA NO FAVORABLE (HUMEDAD, TEMPERATURA)",
            "SOLICITANDO MP Y MA",
            "ARRIBO DE MP O MA",
            "ANALISIS DE MP PENDIENTE",
            "ALMACEN PENDIENTE DE LIBERAR",
            "INSUFICIENCIA DE MATERIALES"
        };


        public static ObservableCollection<string> CausalFabricaLiberado { get; } = new ObservableCollection<string>
        {
            "PARCIAL",
            "COMPLETO"
        };


        public static ObservableCollection<string> CausalFabricaAlmacen { get; } = new ObservableCollection<string>
        {
            "REVISION DE CALIDAD",
            "DICTAMEN DE CALIDAD"
        };


        public static ObservableCollection<string> CausalFabricaFabricacion { get; } = new ObservableCollection<string>
        {
          "MEZCLADO, COMPRESION, LIMPIEZA",
          "ACONDICIONADO",
          "FABRICA EN CAMPAÑA",
          "CLIMA NO FAVORABLE (HUMEDAD, TEMPERATURA)",
          "LIMPIEZA PROFUNDA",
          "FALLA EN EQUIPOS",
          "RE ACONDICIONADO(CHECAR CON LAURA)",

        };



        /* LISTAS OBSERVABLES DE OTROS    */

        public static ObservableCollection<string> ListEmpresasObs { get; } = new ObservableCollection<string>
        {
            "LABORATORIOS VANQUISH S.A. DE C.V.",
            "VANQUISH FARMACEUTICA",
            "VANTAGE SERVICIOS INTEGRALES DE SALUD",
            "MIREI PHARMA DE MÉXICO S.A. DE C.V"
        };
    }
}
