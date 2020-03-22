import React, { useState } from 'react';
import AddressDetail from './AddressDetail';



const AddressSelectRow = (props) =>
{
  console.log('address object id test', props.address.OBJECTID);

  const [isVisible, setIsVisible] = useState(props.show);


  return (
    <>
      <tr key={props.address.OBJECTID}>
        <td>
          {props.address.WholeAddress}
        </td>
        <td>
          {props.address.City}
        </td>
        <td>
          <button
            type="button"
            className="button is-success"
            onClick={event =>
            {
              setIsVisible(!isVisible);
            }}>
            {isVisible ? 'Hide ' : 'Show '}
          Address Information
        </button>
        </td>
      </tr>
      {!isVisible ? null : (
        <AddressDetail
          key={'Detail' + '-' + props.address.OBJECTID}
          address={props.address} />        
        
        )}

    </>

  )
  //
}

export default AddressSelectRow;