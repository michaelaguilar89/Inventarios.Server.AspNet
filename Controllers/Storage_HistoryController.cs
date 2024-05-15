using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventarios.Server.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Storage_HistoryController : ControllerBase
    {
        // GET: api/<Storage_HistoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Storage_HistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Storage_HistoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Storage_HistoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Storage_HistoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
