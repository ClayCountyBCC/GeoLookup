using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLookup.Controllers
{
    [Route("api/GeoLookup")]
    [ApiController]
    public class AddressFeaturesController : ControllerBase
    {
        // GET: api/AddressFeatures

        [HttpGet]
        public IEnumerable<string> GetPointFeatures(decimal latitude, decimal longitude)
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AddressFeatures/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AddressFeatures
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AddressFeatures/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
