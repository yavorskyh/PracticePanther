using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PP.Library.Models;
using PP.Library.Utilities;

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
            RefreshClients();
        }
        public List<Client> Clients 
        { 
            get {
                
                return clients ?? new List<Client>(); 
            } 
        }
        public Client? GetClient(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Client/{id}")
                    .Result;
            var client = JsonConvert.DeserializeObject<Client>(response);
            return client;
            //return Clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdateClient(Client client)
        {
            var response
                = new WebRequestHandler().Post("/Client", client).Result;
            RefreshClients();
        }

        public void Read()
        {
            Clients.ForEach(Console.WriteLine);
        }

        public void DeleteClient(int id) 
        {
            var clientToRemove = Clients.FirstOrDefault(c => c.Id == id);
            if (clientToRemove != null)
            {
                var response = new WebRequestHandler().Delete($"/Client/Delete/{id}").Result;
            }
            RefreshClients();
            /*var clientToRemove = Clients.FirstOrDefault(c => c.Id == id);
            if (clientToRemove != null) 
            {
                Clients.Remove(clientToRemove);
            }*/

        }

        public IEnumerable<Client> Search(string query)
        {

            var response = new WebRequestHandler().Get($"/Client?query={query}").Result;
            var clients = JsonConvert.
                DeserializeObject<List<Client>>(response) ?? new List<Client>();
            return clients ?? new List<Client>();
            /*return Clients
                 .Where(c => c.Name.ToUpper()
                     .Contains(query.ToUpper()));*/
        }

        private void RefreshClients()
        {
            var response = new WebRequestHandler().Get("/Client").Result;
            clients = JsonConvert.
                DeserializeObject<List<Client>>(response) ?? new List<Client>();
            clients = clients.OrderBy(c => c.Id).ToList();
        }

        private int LastId
        {
            get
            {
                return Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
            }
        }
    }
}
