using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace GeoLookup.Models
{
  public class AddressFeature
  {
    public Point point_used { get; set; }
    public string Commissioner_District { get; set; } = "";
    public string Commissioner_Name { get; set; } = "";
    public string Electric_Service_Zone { get; set; } = "";
    public string Evacuation_Zone_Code { get; set; } = "";
    public string Evacuation_Zone { get; set; } = "";
    public string Florida_House_Name { get; set; } = "";
    public string Florida_Senate_Name { get; set; } = "";
    public string Voting_Precinct { get; set; } = "";
    public string Congressional_Senator_Name { get; set; } = "";
    public string Congressional_District { get; set; } = "";
    public string Waste_Collection_Garbage_Pickup_Day { get; set; } = "";
    public string Waste_Collection_Yard_Waste_Pickup_Day { get; set; } = "";
    public string Waste_Collection_Recycling_Pickup_Day { get; set; } = "";
    public string Waste_Collection_Contractor_Name { get; set; } = "";
    public string Waste_Collection_Contractor_Phone_Number { get; set; } = "";
    public string Waste_Collection_website { get; set; } = "";
    public string Water_Provider { get; set; } = "";
    public string Sewage_Service_Provider { get; set; } = "";
    public string Elementary_School_Name { get; set; } = "";
    public string Elementary_School_Website { get; set; } = "";
    public string Sixth_Grade_School_Name { get; set; } = "";
    public string Sixth_Grace_School_Website { get; set; } = "";
    public string Junior_High_School_Name { get; set; } = "";
    public string Junior_High_School_Website { get; set; } = "";
    public string High_School_Name { get; set; } = "";
    public string High_School_Website { get; set; } = "";
    public string School_District { get; set; } = "";
    public string School_District_Board_Member_Name { get; set; } = "";
    public string School_District_Baord_Member_Email_Address { get; set; } = "";
    public string Clay_County_Check { get; set; } = "";

    public AddressFeature()
    {

    }

    public static List<AddressFeature> GetFeatures(Point p)
    {
      if (p.state_plane_x == 0) return new List<AddressFeature>();

      var param = new DynamicParameters();
      param.Add("@x", p.state_plane_x);
      param.Add("@y", p.state_plane_y);

      string query = @"
        WITH Points AS (

          SELECT
            @Point Point  

        )

        SELECT  
          CD.District Commisioner_District
          ,CD.CommissionerName Commisioner_Name
          ,ESZ.NAME Electric_Service_Zone
          ,ISNULL(EVZ.EvacZoneCode, 'None') Evacuation_Zone_Code
          ,ISNULL(EVZ.EvacZone, 'None') Evacuation_Zone
          ,FH.NAME Florida_House_Name
          ,FH.District Florida_House_District
          ,FS.NAME Florida_Senate_Name
          ,FS.District Florida_Senate_District
          ,PB.PRECINCT Voting_Precinct
          ,C.NAME Congressional_Senator_Name
          ,C.District Congressional_District
          ,WC.Garbage Waste_Collection_Garbage_Pickup_Day
          ,WC.Yard Waste_Collection_Yard_Waste_Pickup_Day
          ,WC.Recycle Waste_Collection_Recycling_Pickup_Day
          ,WC.Contractor Waste_Collection_Contractor_Name
          ,WC.Phone Waste_Collection_Contractor_Phone_Number
          ,WC.Website Waste_Collection_Website
          ,WSSZ.Water Water_Provider
          ,WSSZ.Sewer Sewage_Service_Provider
          ,ES.Name Elementary_School_Name
          ,ES.Website Elementary_School_Website
          ,ES6.Name Sixth_Grade_School_Name
          ,ES6.Website Sixth_Grade_School_Website  
          ,JS.Name Junior_High_School_Name
          ,JS.Website Junior_High_School_Website
          ,HS.Name High_School_Name
          ,HS.Website High_School_Website
          ,SD.District School_District
          ,SD.Name School_District_Board_Member_Name
          ,SD.Email School_District_Board_Member_Email_Address
          ,CASE WHEN CTY.OBJECTID IS NULL 
            THEN 'Point is not inside of Clay County'
            ELSE 'Ok'
            END Clay_County_Check
        FROM Points P
        LEFT OUTER JOIN Clay.dbo.COMMISSIONERDISTRICT CD ON CD.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.ELECTRIC_SERVICE_ZONES ESZ ON ESZ.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.EVACUATIONZONES EVZ ON EVZ.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.FLORIDA_HOUSE FH ON FH.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.FLORIDA_SENATE FS ON FS.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.PRECINCT_BOUNDARY PB ON PB.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.US_CONGRESS C ON C.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.WASTE_COLLECTION WC ON WC.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.WATER_SEWER_SERVICE_ZONES WSSZ ON WSSZ.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.ELEM_SCHOOL ES ON ES.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.ELEM6_SCHOOL ES6 ON ES6.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.JRHIGH_SCHOOL JS ON JS.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.HIGH_SCHOOL HS ON HS.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.SCHOOL_DISTRICT SD ON SD.Shape.STIntersects(P.Point) = 1
        LEFT OUTER JOIN Clay.dbo.COUNTY CTY ON C.Shape.STIntersects(P.Point) = 1
        ";

      try
      {
        using (IDbConnection db = new SqlConnection(cs))
        {
          var results = (List<AddressFeature>)db.Query(query, param);

          foreach (AddressFeature r in results)
          {
            r.point_used = p;
          }
          return results;
        }

      }
      catch (Exception ex)
      {
        new ErrorLog(ex);
      }


    }
  }
}
