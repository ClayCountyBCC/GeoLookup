import React, { useState, useEffect } from 'react';

export const useFetchData = (url: string, setData: Function, initial_load: boolean) =>
{
  const [isLoading, setIsLoading] = useState(false);
  const [isError, setIsError] = useState(false);
   
  const fetchData = async () =>
  {
    setIsError(false);
    setIsLoading(true);

    try
    {
      await fetch(url).then(results =>
      {
        setData(results);
        setIsLoading(false);
      });
      
    } catch (error)
    {
      setIsError(true);
      console.log('error in useFetchData', error);
    }
    setIsLoading(false);
  }

  useEffect(() =>
  {
    fetchData();
  }, [url]);

  return { isLoading, isError, fetchData };
}

export default useFetchData;