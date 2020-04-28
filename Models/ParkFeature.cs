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
    public double Address_X { get; set; } = 0;
    public double Address_Y { get; set; } = 0;
    public Point Address_Point
    {
      get
      {
        return new Point(Address_X, Address_Y);
      }      
    }

    public ParkFeature() { }

    public ParkFeature(string name, string address, double x, double y) 
    {
      Park_Name = name;
      Park_Address = address;
      Matching_Feature = "";
      Address_X = x;
      Address_Y = y;
    }

    public static List<string> GetFeatures()
    {
      var features = new List<string>
      {
        "ADA Baseball Field",
        "Baseball Cage",
        "Baseball Field",
        "Basketball Court",
        "Batting Cage",
        "Big Cabin",
        "Block & Fame Dining Hall",
        "Block Cabin",
        "Boat Ramp",
        "Boat Slip",
        "Bunk House",
        "Cabin",
        "Campfire Circle",
        "Camping Full-Facility",
        "Canoe Launch",
        "Ceder Burrell Lodge",
        "Concession Stand",
        "Cook's Cabin",
        "Cypress Mian Dining Hall & Kitchen",
        "Dining Hall",
        "Fishing",
        "Fishing Pier",
        "Frisbee Golf",
        "Hiking",
        "Horse Riding Area",
        "Museum",
        "Muskogee Cabin",
        "Observation Site",
        "Pavilion",
        "Pavilion Bldg",
        "Picnic Table",
        "Pier",
        "Playground",
        "Pool",
        "R.V. Parking Area",
        "Sand Volleyball Court Area",
        "Soccer Field",
        "Soft Toss Station",
        "Swimming",
        "Tennis Court",
        "Tent Campsite",
        "Volleyball Court",
        "Vollyball Court",
        "Wood Bench",
        "Wood Deck",
        "Wood Deck Amphitheater",
        "Wood Ramp",
        "Wooded Boardwalk"
      };
      return features;
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
          ,ISNULL(PL.Address, '') Park_Address -- PL.Address field or fields
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

    public static List<ParkFeature> GetAllParksAndFeatures(string cs)
    {
      string query = @"
        WITH ParkFeatures
             AS (SELECT
                   'ADA Baseball Field' AS ParkFeature
                 UNION ALL
                 SELECT
                   'Baseball Cage'
                 UNION ALL
                 SELECT
                   'Baseball Field'
                 UNION ALL
                 SELECT
                   'Basketball Court'
                 UNION ALL
                 SELECT
                   'Batting Cage'
                 UNION ALL
                 SELECT
                   'Big Cabin'
                 UNION ALL
                 SELECT
                   'Block & Fame Dining Hall'
                 UNION ALL
                 SELECT
                   'Block Cabin'
                 UNION ALL
                 SELECT
                   'Boat Ramp'
                 UNION ALL
                 SELECT
                   'Boat Slip'
                 UNION ALL
                 SELECT
                   'Bunk House'
                 UNION ALL
                 SELECT
                   'Cabin'
                 UNION ALL
                 SELECT
                   'Campfire Circle'
                 UNION ALL
                 SELECT
                   'Camping Full-Facility'
                 UNION ALL
                 SELECT
                   'Canoe Launch'
                 UNION ALL
                 SELECT
                   'Ceder Burrell Lodge'
                 UNION ALL
                 SELECT
                   'Concession Stand'
                 UNION ALL
                 SELECT
                   'Cook''s Cabin'
                 UNION ALL
                 SELECT
                   'Cypress Mian Dining Hall & Kitchen'
                 UNION ALL
                 SELECT
                   'Dining Hall'
                 UNION ALL
                 SELECT
                   'Fishing'
                 UNION ALL
                 SELECT
                   'Fishing Pier'
                 UNION ALL
                 SELECT
                   'Frisbee Golf'
                 UNION ALL
                 SELECT
                   'Hiking'
                 UNION ALL
                 SELECT
                   'Horse Riding Area'
                 UNION ALL
                 SELECT
                   'Museum'
                 UNION ALL
                 SELECT
                   'Muskogee Cabin'
                 UNION ALL
                 SELECT
                   'Observation Site'
                 UNION ALL
                 SELECT
                   'Pavilion'
                 UNION ALL
                 SELECT
                   'Pavilion Bldg'
                 UNION ALL
                 SELECT
                   'Picnic Table'
                 UNION ALL
                 SELECT
                   'Pier'
                 UNION ALL
                 SELECT
                   'Playground'
                 UNION ALL
                 SELECT
                   'Pool'
                 UNION ALL
                 SELECT
                   'R.V. Parking Area'
                 UNION ALL
                 SELECT
                   'Sand Volleyball Court Area'
                 UNION ALL
                 SELECT
                   'Soccer Field'
                 UNION ALL
                 SELECT
                   'Soft Toss Station'
                 UNION ALL
                 SELECT
                   'Swimming'
                 UNION ALL
                 SELECT
                   'Tennis Court'
                 UNION ALL
                 SELECT
                   'Tent Campsite'
                 UNION ALL
                 SELECT
                   'Volleyball Court'
                 UNION ALL
                 SELECT
                   'Vollyball Court'
                 UNION ALL
                 SELECT
                   'Wood Bench'
                 UNION ALL
                 SELECT
                   'Wood Deck'
                 UNION ALL
                 SELECT
                   'Wood Deck Amphitheater'
                 UNION ALL
                 SELECT
                   'Wood Ramp'
                 UNION ALL
                 SELECT
                   'Wooded Boardwalk')
            ,Matches
             AS (SELECT
                   PIL.FacilityType Matching_Feature
                   ,PIL.ParkName Park_Name
                 FROM
                   PARK_INFRASTRUCTURE_LINE PIL
                   INNER JOIN ParkFeatures PF ON PIL.FacilityType LIKE '%' + PF.ParkFeature + '%'
                 UNION
                 SELECT
                   PT.FacilityType Matching_Feature
                   ,PT.ParkName Park_Name
                 FROM
                   PARK_INFRASTRUCTURE_PT PT
                   INNER JOIN ParkFeatures PF ON PT.FacilityType LIKE '%' + PF.ParkFeature + '%'
                 UNION
                 SELECT
                   PIP.FacilityType Matching_Feature
                   ,PIP.ParkName Park_Name
                 FROM
                   PARK_INFRASTRUCTURE_POLY PIP
                   INNER JOIN ParkFeatures PF ON PIP.FacilityType LIKE '%' + PF.ParkFeature + '%')
        SELECT DISTINCT
          M.Matching_Feature
          ,M.Park_Name
          ,ISNULL(PL.Address
                  ,'') Park_Address
          ,ISNULL(AD.Shape.STX, 0) Address_x
          ,ISNULL(AD.Shape.STY, 0) Address_y
        FROM
          Matches M
          INNER JOIN PARK_LOCATION PL ON M.Park_Name = PL.NAME
          LEFT OUTER JOIN ADDRESS_SITE AD ON PL.Address = AD.WholeAddress + ', ' + AD.Community
        ORDER  BY
          Park_Name
          ,Matching_Feature ";
      try
      {
        using (IDbConnection db = new SqlConnection(cs))
        {
          return (List<ParkFeature>)db.Query<ParkFeature>(query);
        }

      }
      catch (Exception ex)
      {
        new ErrorLog(ex);
        return null;
      }
    }

    public static List<ParkFeature>GetAllParks(string cs)
    {
      var all_park_features = (List<ParkFeature>)myCache.GetItem("all_parks_and_features", cs);
      var parks = (from p in all_park_features
                   select new ParkFeature(p.Park_Name, p.Park_Address, p.Address_X, p.Address_Y))
                   .Distinct()
                   .ToList<ParkFeature>();
      return parks;

    }

  }



}
