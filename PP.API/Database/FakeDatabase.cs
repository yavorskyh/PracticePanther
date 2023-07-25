using PP.Library.Models;

namespace PP.API.Database
{
    public static class FakeDatabase
    {
        public static List<Client> Clients = new List<Client>
            {
                new Client{ Id = 1, Name = "Walmart"},
                new Client{ Id = 2, Name = "Costco"},
                new Client{ Id = 3, Name = "Aldi"},
                new Client{ Id = 4, Name = "Target"},
                new Client{ Id = 5, Name = "Hunter's Store"},
                new Client{ Id = 6, Name = "Ari's Shop"}
        };

        public static int LastClientId
            => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
    }
}