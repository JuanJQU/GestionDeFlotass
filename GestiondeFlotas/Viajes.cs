using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeFlotas
{
    public class Viajes
    {
        public int ViajeID { get; set; }
        public int VehiculoID { get; set; }
        public int ConductorID { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string Estado { get; set; }
    }
}
