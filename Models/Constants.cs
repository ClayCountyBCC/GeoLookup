using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GeoLookup.Models
{
  public static class Constants
  {
    public static string Get_Connection_String(string cs_name)
    {
      //var builder = new ConfigurationBuilder()
      //  .SetBasePath(Directory.GetCurrentDirectory())
      //  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

      //var config = builder.Build();
      return Startup.Configuration.GetConnectionString(cs_name);
    }
  }
}
