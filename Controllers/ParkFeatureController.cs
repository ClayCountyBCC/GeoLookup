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
  [Route("api/[controller]")]
  [ApiController]
  public class ParkFeatureController : ControllerBase
  {
    private ConnectionConfig _config;

    public ParkFeatureController(IOptions<ConnectionConfig> connectionConfig)
    {
      _config = connectionConfig.Value;
    }
    // GET: api/ParkFeature
    [HttpGet("GetValidParkFeatures")]
    public List<string> GetValidParkFeatures()
    {
      return (List<string>)myCache.GetItem("park_features", _config.GIS);
    }

    [HttpGet("SearchParksByFeature")]
    public List<ParkFeature> SearchParksByFeature(string feature)
    {
      return ParkFeature.SearchParks(feature, _config.GIS);
    }

  }
}
