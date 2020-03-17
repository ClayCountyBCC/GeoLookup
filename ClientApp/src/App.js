import React, { useState } from 'react';
//import { Route } from 'react-router';
//import { Layout } from './components/Layout';
//import { Home } from './components/Home';
//import { FetchData } from './components/FetchData';
//import { Counter } from './components/Counter';

//import './custom.css'

const App = () =>
{

  const [currentHouse, setCurrentHouse] = useState("");
  const [currentStreet, setCurrentStreet] = useState("");
  const [searchResults, setSearchResults] = useState([]);

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

        <div style={{marginTop: "1em"}}>
          <div className="field">
            <label className="label">House Number</label>
            <div className="control">
              <input
                className="input"
                onchange={event => setCurrentHouse(event.target.value)}
                type="text"
                title="Please input numbers only."
                placeholder="House Number"
                value={currentHouse}
              />
            </div>
          </div>
          <div className="field">
            <label className="label">Street Name</label>
            <div className="control">
              <input
                className="input"
                onchange={event => setCurrentStreet(event.target.value)}
                type="text"
                title="Please input street names only."
                placeholder="Street Name"
                value={currentHouse}
              />
            </div>
          </div>
          <div className="field">
            <div className="control">
              <button type="button" className="button is-success">
                Search
              </button>
            </div>
          </div>
        </div>

      </div>

    </>
  );

}

export default App;
