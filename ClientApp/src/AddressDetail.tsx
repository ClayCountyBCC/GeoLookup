import React, { useEffect } from 'react';



const AddressDetail = (props) =>
{
  // { label: "", field: '' },
  
  const sections = [
    {
      id: 1,
      title: "Address Information",
      detail: [
        { label: "Address", field: 'WholeAddress' },
        { label: "City", field: 'City' },
        { label: "Zip", field: 'Zip' },
        { label: "Latitude", field: 'point_used.latitude' },
        { label: "Longitude", field: 'point_used.longitude' },

      ]
    },
    {
      id: 2,
      title: "County Commissioner",
      detail: [
        { label: "District", field: 'Commissioner_District' },
        { label: "Name", field: 'Commissioner_Name' }
      ]
    },
    {
      id: 3,
      title: "Evacuation Zone Information",
      detail: [
        { label: "Zone", field: 'Evacuation_Zone' },
        { label: "Code", field: 'Evacuation_Zone_Code' }
      ]
    },
    {
      id: 4,
      title: "Voting Precinct",
      detail: [
        { label: "Voting Precinct", field: 'Voting_Precinct' },
        { label: "Voting Precinct Location", field: 'Voting_Precinct_Name' },
        { label: "Voting Precinct Address", field: 'Voting_Precinct_Address' },
      ]
    },
    {
      id: 5,
      title: "Florida State Lawmakers",
      detail: [
        { label: "House District", field: 'Florida_House_District' },
        { label: "House Representative", field: 'Florida_House_Representative_Name' },
        { label: "Senate District", field: 'Florida_Senate_District' },
        { label: "Senate Representative", field: 'Florida_Senator_Name' },
      ]
    },
    {
      id: 6,
      title: "US House",
      detail: [
        { label: "District", field: 'US_House_District' },
        { label: "Representative", field: 'US_House_Representative_Name' }
      ]
    },
    {
      id: 7,
      title: "Schools",
      detail: [
        { label: "Elementary School", field: 'Elementary_School_Name' },
        { label: "Elementary School Website", field: 'Elementary_School_Website' },
        { label: "Sixth Grade School", field: 'Sixth_Grade_School_Name' },
        { label: "Sixth Grade School Website", field: 'Sixth_Grade_School_Website' },
        { label: "Junior High School", field: 'Junior_High_School_Name' },
        { label: "Junior High School Website", field: 'Junior_High_School_Website' },
        { label: "High School", field: 'High_School_Name' },
        { label: "High School Website", field: 'High_School_Website' },
      ]
    },
    {
      id: 8,
      title: "School Board",
      detail: [
        { label: "District", field: 'School_District' },
        { label: "Board Member", field: 'School_District_Board_Member_Name' },
        { label: "Email Address", field: 'School_District_Board_Member_Email_Address' },
      ]
    },
    {
      id: 9,
      title: "Water & Power",
      detail: [
        { label: "Electric Service Provider", field: 'Electric_Service_Zone' },
        { label: "Water Servie Provider", field: 'Water_Service_Provider' },
        { label: "Sewer Service Provider", field: 'Sewage_Service_Provider' },
      ]
    },
    {
      id: 10,
      title: "Waste Collection",
      detail: [
        { label: "Governed By", field: 'Waste_Collection_Governed_By' },
        { label: "Garbage Pick up Day", field: 'Waste_Collection_Garbage_Pickup_Day' },
        { label: "Yard Waste Pick up Day", field: 'Waste_Collection_Yard_Waste_Pickup_Day' },
        { label: "Recycling Pick up Day", field: 'Waste_Collection_Recycling_Pickup_Day' },
        { label: "Website", field: 'Waste_Collection_Website' },

      ]
    }
    //{
    //  title: "",
    //  detail: [
    //    { label: "", field: '' },
    //  ]
    //},

  ];
  const Municipality_Waste_Fields = ['Waste_Collection_Recycling_Pickup_Day', 'Waste_Collection_Yard_Waste_Pickup_Day', 'Waste_Collection_Garbage_Pickup_Day'];

  const getFieldValue = (field, address) =>
  {
    
    if (field.indexOf(".") === -1)
    {
      let v = address[field];    
      if (Municipality_Waste_Fields.indexOf(field) > -1 && address['Waste_Collection_Governed_By'] !== 'Unincorp Clay County')
      {
        v = 'Check your Municipality website for this information.';
      }
      return v;
    }
    //
    let f = field.split(".");
    return address[f[0]][f[1]];
  }

  return (
    <tr>
      <td colSpan={3}>
        <table className="table is-fullwidth">
          <tbody>
            {sections.map((section, index) =>
            {
              return (
                <>
                  <tr key={section.id }>
                    <td colSpan={3}>
                      {section.title}
                    </td>
                  </tr>
                  {section.detail.map((detail, index) =>
                  {
                    return (
                      <tr key={section.id.toString() + '-' + index.toString()}>
                        <td style={{ width: "10%" }}>
                        </td>
                        <td style={{ width: "30%" }}>
                          {detail.label}
                        </td>
                        <td style={{ width: "60%" }}>
                          { getFieldValue(detail.field, props.address)}
                        </td>
                      </tr>
                    )
                  })}
                </>
              )
            })}
          </tbody>
        </table>
      </td>
    </tr>

  )
}

export default AddressDetail;

