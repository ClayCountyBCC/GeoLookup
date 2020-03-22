import React from 'react';
import AddressSelectRow from './AddressSelectRow';


const AddressSelect = (props) =>
{
  let showAddress = false;

  if (props.addresses === null) return null;

  if (props.addresses.length === 0) return (
    <table className="table is-fullwidth">
      <thead>
        <tr>
          <th style={{ width: "50%" }}>
            Address
            </th>
          <th style={{ width: "25%" }}>
            City
            </th>
          <th style={{ width: "25%" }}>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td colSpan={3}>
            This House and Street combination did not match any of our addresses.
          </td>
        </tr>
      </tbody>
    </table>
    )

  if (props.addresses.length === 1) showAddress = true;

  return (
    <>
      <table className="table is-fullwidth">
        <thead>
          <tr>
            <th style={{ width: "50%" }}>
              Address
            </th>
            <th style={{ width: "25%" }}>
              City
            </th>
            <th style={{ width: "25%" }}>
            </th>
          </tr>
        </thead>
        <tbody>
          {props.addresses.map((address, index) =>
          {
            return (
              <AddressSelectRow
                key={address.OBJECTID}
                show={showAddress}
                address={address} />
            )
          }
          )}
        </tbody>
      </table>
    </>
  )
}

export default AddressSelect;