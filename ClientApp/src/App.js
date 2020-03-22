import React, { useState, useEffect, useRef } from 'react';
import AddressSelect from './AddressSelect';
//import useFetchData from './hooks/useFetchData';
//import { Route } from 'react-router';
//import { Layout } from './components/Layout';
//import { Home } from './components/Home';
//import { FetchData } from './components/FetchData';
//import { Counter } from './components/Counter';

//import './custom.css'

const App = () =>
{
  const houseMessageRef = useRef(null);
  const streetMessageRef = useRef(null);

  const [currentHouse, setCurrentHouse] = useState("");
  const [isHouseError, setIsHouseError] = useState(true);
  const [currentStreet, setCurrentStreet] = useState("");
  const [isStreetError, setIsStreetError] = useState(true);
  const [searchResults, setSearchResults] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [isError, setIsError] = useState(false);

  const fetchData = async (url) =>
  {
    setIsError(false);
    setIsLoading(true);

    try
    {
      await fetch(url,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json"//,"Upgrade-Insecure-Requests": "1"
          },
          cache: "no-cache",
          credentials: "include"
        }
      ).then(async results =>
      {
        if (!results.ok)
        {
          throw new Error(results.statusText);
        }
        let data = await results.json();
        console.log('search results', data);
        setSearchResults(data);
        setIsLoading(false);
      });

    } catch (error)
    {
      setIsError(true);
      console.log('error in getting data', error);
    }
    setIsLoading(false);
  }

  const Search = async () =>
  {
    if (isHouseError || isStreetError) return;
    let url = `api/GeoLookup/GetAddressFeatures?house=${currentHouse}&street=${currentStreet}`;
    await fetchData(url);
  }
    
  const validateHouseNumber = (houseNumber) =>
  {
    setIsHouseError(false);
    let e = houseMessageRef.current;
    e.className = "";
    e.innerText = "";
    if (houseNumber.trim().length === 0)
    {
      e.className = "help is-danger";
      e.innerText = "No house number was entered.  This value should be numeric only.";
      setIsHouseError(true);
      return false;
    }
    const globalRegex = RegExp(/[^\d]/, 'g');
    if (globalRegex.test(houseNumber))
    {
      e.className = "help is-danger"
      e.innerText = "The house number must be numbers only.  No punctuation or letters, please.";
      return false;
    }
    e.className = "help is-success"
    e.innerText = "This looks like a valid street number.";
    return true;
  }

  const Reset = () =>
  {
    setCurrentHouse("");
    setCurrentStreet("");
    setSearchResults(null);
    houseMessageRef.current.className = "";
    houseMessageRef.current.innerText = "";
    streetMessageRef.current.className = "";
    streetMessageRef.current.innerText = "";
    setIsHouseError(true);
    setIsStreetError(true);
    setIsError(false);
  }

  const validateStreetName = (streetName) =>
  {
    setIsStreetError(false);
    let e = streetMessageRef.current;
    e.className = "";
    e.innerText = "";
    if (streetName.trim().length === 0)
    {
      e.className = "help is-danger";
      e.innerText = "No street name was entered.  You don't need to include street types like Road, Drive, or Blvd.";
      setIsStreetError(true);
      return false;
    }
    const globalRegex = RegExp(/[^a-zA-Z ]/, 'g');
    if (globalRegex.test(streetName))
    {
      e.className = "help has-background-warning"
      e.innerText = "You entered more than just letters and spaces in here, so it's not likely to match anything.  But we'll try it all the same.  If you don't get any results, try just searching with just a piece of your street name.";
      return false;
    }
    e.className = "help is-success"
    e.innerText = "This looks like a valid street name.";
    return true;
  }

  useEffect(() =>
  {

  }, [searchResults]);

  return (
    <>
      <div className="container">
        <div className="message">
          <div className="message-header">
            <p>Look up Address Information</p>
          </div>
          <div className="message-body">
            <p>
              You can use this application to find information about a street address.
            </p>
            <p>
              Please type in the house number, and part of the street name to search. Street suffixes like Drive or Street are not needed. For example, instead of entering Blanding Blvd for the street name, you can just enter Blanding.
            </p>
          </div>
        </div>

        <div style={{ marginTop: "1em" }}>
          <div className="field">
            <label className="label">House Number</label>
            <div className="control">
              <input
                className="input"
                onChange={event =>
                {
                  setCurrentHouse(event.target.value);
                  validateHouseNumber(event.target.value);
                }}
                onKeyDown={event =>
                {
                  if (event.key === 'Enter')
                  {
                    event.preventDefault();
                    event.stopPropagation();
                    Search();
                  }
                }}
                onBlur={event => validateHouseNumber(event.target.value)}
                type="text"
                title="Please input numbers only."
                placeholder="House Number"
                value={currentHouse}
              />
            </div>
            <p
              style={{ paddingLeft: "1em", paddingRight: "1em" }}
              ref={houseMessageRef}></p>
          </div>
          <div className="field">
            <label className="label">Street Name</label>
            <div className="control">
              <input
                className="input"
                onChange={event =>
                {
                  setCurrentStreet(event.target.value);
                  validateStreetName(event.target.value);
                }}
                onKeyDown={event =>
                {
                  if (event.key === 'Enter')
                  {
                    event.preventDefault();
                    event.stopPropagation();
                    Search();
                  }
                }}
                onBlur={event => validateStreetName(event.target.value)}
                type="text"
                title="Please input street names only."
                placeholder="Street Name"
                value={currentStreet}
              />
            </div>
            <p
              style={{ paddingLeft: "1em", paddingRight: "1em" }}
              ref={streetMessageRef}></p>
          </div>
          <div className="field is-grouped">
            <div
              className="control">
              <button
                type="button"
                disabled={isHouseError || isStreetError}
                onClick={event => Search()}
                className={`button is-success ${isLoading ? 'is-loading' : ''}`}>
                Search
              </button>
            </div>
            <div className="control">
              <button
                type="button"                
                className="button"
                onClick={event =>
                {
                  Reset();
                }}>
                Clear
              </button>
            </div>
          </div>
        </div>
        <AddressSelect addresses={searchResults} />
        {isError ? <div className="is-danger">There was an error attempting your request.  Please try again.  If this message persists, please try again later.</div> : null}
      </div>
    </>
  );
}
export default App;
