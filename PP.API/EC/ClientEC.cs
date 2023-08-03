using Microsoft.Data.SqlClient;
using PP.API.Database;
using PP.Library.Models;

namespace PP.API.EC
{
    public class ClientEC
    {
        public IEnumerable<Client> Get(string query)
        {
            if (query == null || query.Length == 0)
                return MsSQLContext.Current.Get();

            return MsSQLContext.Current.Get()
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }

        public Client GetById(int id)
        {
            return MsSQLContext.Current.GetById(id);
        }

        public Client? DeleteById(int id)
        {
            return MsSQLContext.Current.DeleteById(id);
        }

        public Client AddOrUpdate(Client client)
        {
            // Add Client
            if (client.Id == 0)
            {
                MsSQLContext.Current.Insert(client);
            }
            // Edit Client
            else
            {
                if (client != null)
                    MsSQLContext.Current.Edit(client);
            }
            return client;
        }
    }
}
