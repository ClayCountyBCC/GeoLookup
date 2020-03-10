using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLookup.Models
{
  public class ConnectionConfig
  {
    public string GIS { get; set; }
    public string LogProduction { get; set; }
    public string LogBackup { get; set; }
  }
}
