using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjNet;

namespace GeoLookup.Models
{
  public class Point
  {
    private const string local_state_plane_wkt = @"PROJCS[""NAD_1983_HARN_StatePlane_Florida_East_FIPS_0901_Feet"", GEOGCS[""GCS_North_American_1983_HARN"", DATUM[""NAD83_High_Accuracy_Regional_Network"", SPHEROID[""GRS_1980"", 6378137.0, 298.257222101]], PRIMEM[""Greenwich"", 0.0], UNIT[""Degree"", 0.0174532925199433]], PROJECTION[""Transverse_Mercator""], PARAMETER[""False_Easting"", 656166.6666666665], PARAMETER[""False_Northing"", 0.0], PARAMETER[""Central_Meridian"", -81.0], PARAMETER[""Scale_Factor"", 0.9999411764705882], PARAMETER[""Latitude_Of_Origin"", 24.33333333333333], UNIT[""Foot_US"", 0.3048006096012192]]";
    public decimal latitude { get; set; } = 0;
    public decimal longitude { get; set; } = 0;
    public double state_plane_x { get; set; } = 0;
    public double state_plane_y { get; set; } = 0;

    public Point(decimal latitude, decimal longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
      state_plane_x = 0;
      state_plane_y = 0;
      Convert_To_State_Plane();
    }

    public Point(double state_plane_x, double state_plane_y)
    {
      this.state_plane_x = state_plane_x;
      this.state_plane_y = state_plane_y;
      latitude = 0;
      longitude = 0;
      Convert_To_Lat_Long();
    }

    private void Convert_To_State_Plane()
    {
      
      var x = new ProjNet.CoordinateSystems.CoordinateSystemFactory();
      var projtarget = x.CreateFromWkt(Point.local_state_plane_wkt);
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
    }    

    private void Convert_To_Lat_Long()
    {
      var x = new ProjNet.CoordinateSystems.CoordinateSystemFactory();
      var projsource = x.CreateFromWkt(Point.local_state_plane_wkt);
      try
      {
        var projtarget = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
        var t = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory();
        var trans = t.CreateFromCoordinateSystems(projsource, projtarget);
        double[] point = { state_plane_x, state_plane_y };
        double[] convpoint = trans.MathTransform.Transform(point);
        latitude = (decimal)convpoint[1];
        longitude = (decimal)convpoint[0];
      }
      catch (Exception ex)
      {
        latitude = 0;
        longitude = 0;
        new ErrorLog(ex);
      }
    }

  }
}
