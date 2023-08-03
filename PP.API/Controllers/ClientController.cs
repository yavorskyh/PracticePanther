using Microsoft.AspNetCore.Mvc;
using PP.API.EC;
using PP.Library.Models;

namespace PP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Client> Get([FromQuery] string query = null)
        {
            return new ClientEC().Get(query);
        }

        [HttpGet("{id}")]
        public Client GetById(int id)
        {
            return new ClientEC().GetById(id);
        }

        [HttpDelete("Delete/{id}")]
        public Client? DeleteById(int id)
        {
            return new ClientEC().DeleteById(id);
        }

        [HttpPost]
        public Client AddOrUpdate([FromBody]Client client) 
        {
            return new ClientEC().AddOrUpdate(client);
        }

    }
}
