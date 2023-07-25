using Microsoft.AspNetCore.Mvc;
using PP.API.Database;
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
            if (query == null || query.Length == 0)
                return FakeDatabase.Clients;

            return FakeDatabase.Clients
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }

        [HttpGet("{id}")]
        public Client GetById(int id)
        {
            return FakeDatabase.Clients.FirstOrDefault(c => c.Id == id) ?? new Client();
        }

        [HttpDelete("Delete/{id}")]
        public Client? DeleteById(int id)
        {
            var clientToDelete =  FakeDatabase.Clients.FirstOrDefault(c => c.Id == id) ?? new Client();
            if (clientToDelete != null)
            {
                FakeDatabase.Clients.Remove(clientToDelete);
            }
            return clientToDelete;
        }

        [HttpPost]
        public Client AddOrUpdate([FromBody]Client client) 
        {
            if (client.Id == 0)
            {
                client.Id = FakeDatabase.LastClientId + 1;
                FakeDatabase.Clients.Add(client);
            }
            else
            {
                var clientToUpdate = FakeDatabase.Clients.FirstOrDefault(c => c.Id == client.Id);
                if (clientToUpdate != null)
                {
                    FakeDatabase.Clients.Remove(clientToUpdate);
                }
                FakeDatabase.Clients.Add(client);
            }
            return client;
        }

    }
}
