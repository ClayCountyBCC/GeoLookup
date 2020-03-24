using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace GeoLookup.Models
{
  public class ParkFeature
  {
    public string Park_Name { get; set; } = "";
    public string Park_Address { get; set; } = "";
    public string Matching_Feature { get; set; } = "";

    public ParkFeature() { }

    public static List<string> GetFeatures(string cs)
    {
      string query = @"
        SELECT DISTINCT FacilityType
        FROM PARK_INFRASTRUCTURE_LINE
        UNION
        SELECT DISTINCT FacilityType
        FROM PARK_INFRASTRUCTURE_POLY
        UNION
        SELECT DISTINCT FacilityType
        FROM PARK_INFRASTRUCTURE_PT";

      try
      {
        using (IDbConnection db = new SqlConnection(cs))
        {
          return (List<string>)db.Query<string>(query);
        }

      }
      catch (Exception ex)
      {
        new ErrorLog(ex);
        return null;
      }
    }


    public static List<ParkFeature> SearchParks(string feature, string cs)
    {
      
      string query = @"
        WITH Matches AS (
        SELECT
          PIL.FacilityType Matching_Feature
          ,PIL.ParkName Park_Name
        FROM PARK_INFRASTRUCTURE_LINE PIL
        WHERE 
          PIL.FacilityType LIKE '%' + @Feature + '%'
        UNION
        SELECT
          PT.FacilityType Matching_Feature
          ,PT.ParkName Park_Name
        FROM PARK_INFRASTRUCTURE_PT PT
        WHERE 
          PT.FacilityType LIKE '%' + @Feature + '%'
        UNION
        SELECT
          PIP.FacilityType Matching_Feature
          ,PIP.ParkName Park_Name
        FROM PARK_INFRASTRUCTURE_POLY PIP
        WHERE 
          PIP.FacilityType LIKE '%' + @Feature + '%'
        )

        SELECT DISTINCT
          M.Matching_Feature
          ,M.Park_Name
          ,'' Park_Address -- PL.Address field or fields
        FROM Matches M
        INNER JOIN PARK_LOCATION PL ON M.Park_Name = PL.NAME
        ORDER BY Park_Name, Matching_Feature";
      try
      {
        using (IDbConnection db = new SqlConnection(cs))
        {
          return (List<ParkFeature>)db.Query<ParkFeature>(query, new { Feature = feature });
        }

      }
      catch (Exception ex)
      {
        new ErrorLog(ex);
        return null;
      }

    }

  }



}
