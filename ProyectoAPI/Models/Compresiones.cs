using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectoAPI.Models
{
    public class Compresiones
    {
        public string Nombre { get; set; }//Nombre original
        public string NombreCompresion { get; set; }
        public string Ruta { get; set; }//Ruta del archivo comprimido y nombre

        public double Razon_Compresion { get; set; }
        public double Factor_Compresion { get; set; }
        public string Porcentaje_reduccion { get; set; }

    }
}
