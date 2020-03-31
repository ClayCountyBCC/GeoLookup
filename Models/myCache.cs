using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace GeoLookup.Models
{
  public class myCache
  {
    private static MemoryCache _cache = new MemoryCache("myCache");

    public static object GetItem(string key, string cs)
    {
      return GetOrAddExisting(key, () => InitItem(key, cs));
    }

    public static object GetItem(string key, string cs, CacheItemPolicy CIP)
    {
      return GetOrAddExisting(key, () => InitItem(key, cs), CIP);
    }

    private static T GetOrAddExisting<T>(string key, Func<T> valueFactory, CacheItemPolicy CIP)
    {

      Lazy<T> newValue = new Lazy<T>(valueFactory);
      var oldValue = _cache.AddOrGetExisting(key, newValue, CIP) as Lazy<T>;
      try
      {
        return (oldValue ?? newValue).Value;
      }
      catch
      {
        // Handle cached lazy exception by evicting from cache. Thanks to Denis Borovnev for pointing this out!
        _cache.Remove(key);
        throw;
      }
    }

    private static T GetOrAddExisting<T>(string key, Func<T> valueFactory)
    {

      Lazy<T> newValue = new Lazy<T>(valueFactory);
      var oldValue = _cache.AddOrGetExisting(key, newValue, GetCIP()) as Lazy<T>;
      try
      {
        return (oldValue ?? newValue).Value;
      }
      catch
      {
        // Handle cached lazy exception by evicting from cache. Thanks to Denis Borovnev for pointing this out!
        _cache.Remove(key);
        throw;
      }
    }

    private static CacheItemPolicy GetCIP()
    {
      return new CacheItemPolicy()
      {
        AbsoluteExpiration = DateTime.Now.AddHours(8)
      };
    }

    private static object InitItem(string key, string cs)
    {
      switch (key)
      {
        case "gis_cs":
          return "";
        case "park_features":
          return ParkFeature.GetFeatures();
        default:
          return null;
      }
    }
  }
}
