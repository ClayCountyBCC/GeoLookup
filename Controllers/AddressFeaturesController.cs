using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeoLookup.Models;
using Microsoft.Extensions.Options;

namespace GeoLookup.Controllers
{
  [Route("api/GeoLookup")]
  [ApiController]
  public class AddressFeaturesController : ControllerBase
  {
    private ConnectionConfig _config;

    public AddressFeaturesController(IOptions<ConnectionConfig> connectionConfig)
    {
      _config = connectionConfig.Value;      
    }

    [HttpGet("GetPointFeatures")]    
    public ActionResult<List<PointFeature>> GetPointFeatures(decimal latitude, decimal longitude)
    {
      return PointFeature.GetFeaturesByPoint(new Point(latitude, longitude), _config.GIS);
    }

    [HttpGet("GetAddressFeatures")]
    public ActionResult<List<PointFeature>> GetAddressFeatures(int house, string street)
    {
      return PointFeature.GetFeaturesByAddress(house, street, _config.GIS);
    }

  }
}
