using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;

namespace PP.Library.Services
{
    public class ClientService
    {
        private static ClientService? instance;
        private static object _lock = new object();
        public static ClientService Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();
                    }
                }
                return instance;
            }
            
        }

        private List<Client> clients;
        private ClientService()
        {
            clients = new List<Client>();
        }
        public List<Client> Clients 
        { 
            get { return clients; } 
        }
        public Client? GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddClient(Client? client)
        {
            if (client != null)
            {
                clients.Add(client);
            }
        }

        public void UpdateClient(Client client) 
        {
            var clientToUpdate = clients.FirstOrDefault(c => c.Id == client.Id);
            if (clientToUpdate != null) 
            {
                clientToUpdate.Name = client.Name;
                clientToUpdate.OpenDate = client.OpenDate;
                clientToUpdate.CloseDate = client.CloseDate;
                clientToUpdate.Notes = client.Notes;
                clientToUpdate.isActive = client.isActive;
            }
        }

        public void Read()
        {
            clients.ForEach(Console.WriteLine);
        }

        public void DeleteClient(int id) 
        {
            var clientToRemove = clients.FirstOrDefault(c => c.Id == id);
            if (clientToRemove != null) 
            {
                clients.Remove(clientToRemove);
            }
            
        }

        public IEnumerable<Client> Search(string query)
        {
            return clients
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }

        private int LastId
        {
            get
            {
                return clients.Any() ? clients.Select(c => c.Id).Max() : 0;
            }
        }
    }
}
