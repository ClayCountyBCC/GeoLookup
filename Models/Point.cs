using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjNet;

namespace GeoLookup.Models
{
  public class Point
  {
    public decimal latitude { get; set; } = 0;
    public decimal longitude { get; set; } = 0;
    public double state_plane_x { get; set; } = 0;
    public double state_plane_y { get; set; } = 0;

    public Point(decimal latitude, decimal longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
      Convert_To_State_Plane();
    }

    private void Convert_To_State_Plane()
    {
      string source_wkt = @"PROJCS[""NAD_1983_HARN_StatePlane_Florida_East_FIPS_0901_Feet"", GEOGCS[""GCS_North_American_1983_HARN"", DATUM[""NAD83_High_Accuracy_Regional_Network"", SPHEROID[""GRS_1980"", 6378137.0, 298.257222101]], PRIMEM[""Greenwich"", 0.0], UNIT[""Degree"", 0.0174532925199433]], PROJECTION[""Transverse_Mercator""], PARAMETER[""False_Easting"", 656166.6666666665], PARAMETER[""False_Northing"", 0.0], PARAMETER[""Central_Meridian"", -81.0], PARAMETER[""Scale_Factor"", 0.9999411764705882], PARAMETER[""Latitude_Of_Origin"", 24.33333333333333], UNIT[""Foot_US"", 0.3048006096012192]]";
      var x = new ProjNet.CoordinateSystems.CoordinateSystemFactory();
      var projtarget = x.CreateFromWkt(source_wkt);
      try
      {
        var projsource = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
        var t = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory();
        var trans = t.CreateFromCoordinateSystems(projsource, projtarget);
        double[] point = { (double)longitude, (double)latitude };
        double[] convpoint = trans.MathTransform.Transform(point);
        state_plane_x = convpoint[0];
        state_plane_y = convpoint[1];
      }
      catch(Exception ex)
      {
        state_plane_x = 0;
        state_plane_y = 0;
        new ErrorLog(ex);
      }
      //var csource = 

    }

  }
}
