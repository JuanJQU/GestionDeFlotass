using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeFlotas
{
    
  public class ReporteUsoVehiculo
  {
  public int ReporteID { get; set; }
  public int ViajeID { get; set; }
  public string Observaciones { get; set; }
  public string EstadoVehiculoFinal { get; set; }
  public DateTime FechaReporte { get; set; }
  }
}

