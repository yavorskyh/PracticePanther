using Microsoft.Data.SqlClient;
using PP.API.EC;
using PP.Library.Models;

namespace PP.API.Database
{
    public class MsSQLContext
    {
        private MsSQLContext()
        {
            connectionString = "Server=LAPTOP-K3FG2PA4;Database=PracticePanther;Trusted_Connection=True;TrustServerCertificate=True";
        }

        private string connectionString;

        public Client Insert(Client c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"InsertClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("openDate", c.OpenDate));
                        cmd.Parameters.Add(new SqlParameter("closeDate", c.CloseDate));
                        cmd.Parameters.Add(new SqlParameter("active", c.isActive));
                        cmd.Parameters.Add(new SqlParameter("name", c.Name));
                        cmd.Parameters.Add(new SqlParameter("notes", c.Notes));
                        conn.Open();
                        var Id = (int)cmd.ExecuteScalar();
                        c.Id = Id;
                    }
                }
            }
            catch (Exception)
            {
                return c;
            }

            return c;
        }

        public Client Edit(Client c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"UpdateClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("id", c.Id));
                        cmd.Parameters.Add(new SqlParameter("openDate", c.OpenDate));
                        cmd.Parameters.Add(new SqlParameter("closeDate", c.CloseDate));
                        cmd.Parameters.Add(new SqlParameter("active", c.isActive));
                        cmd.Parameters.Add(new SqlParameter("name", c.Name));
                        cmd.Parameters.Add(new SqlParameter("notes", c.Notes));
                        conn.Open();
                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {
                return c;
            }

            return c;
        }

        public Client DeleteById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"DeleteClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("id", id));
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.Read()) 
                        {
                            return new Client
                            {
                                Id = (int)reader[0],
                                OpenDate = !reader.IsDBNull(1) ? reader.GetDateTime(1) : new DateTime(),
                                CloseDate = !reader.IsDBNull(2) ? reader.GetDateTime(2) : new DateTime(),
                                isActive = !reader.IsDBNull(3) ? (bool)reader[3] : true,
                                Name = reader[4]?.ToString() ?? string.Empty,
                                Notes = reader[5]?.ToString() ?? null
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new Client();
            }

            return new Client();
        }

        public IEnumerable<Client> Get()
        {
            var results = new List<Client>();
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "select * from Clients";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Client
                        {
                            Id = (int)reader[0],
                            OpenDate = !reader.IsDBNull(1) ? reader.GetDateTime(1) : new DateTime(),
                            CloseDate = !reader.IsDBNull(2) ? reader.GetDateTime(2) : new DateTime(),
                            isActive = !reader.IsDBNull(3) ? (bool)reader[3] : true,
                            Name = reader[4]?.ToString() ?? string.Empty,
                            Notes = reader[5]?.ToString() ?? null
                        });
                    }
                }
            }

            return results;
        }

        public Client GetById(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var sql = $"select * from Clients where Id = {id}";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    var reader = cmd.ExecuteReader() ;
                    using (reader)
                    {
                        if (reader.Read())
                        {
                            Client client = new Client();
                            client.Id = (int)reader[0];
                            client.OpenDate = !reader.IsDBNull(1) ? reader.GetDateTime(1) : new DateTime();
                            client.CloseDate = !reader.IsDBNull(2) ? reader.GetDateTime(2) : new DateTime();
                            client.isActive = !reader.IsDBNull(3) ? (bool)reader[3] : true;
                            client.Name = reader[4]?.ToString() ?? null;
                            client.Notes = reader[5]?.ToString() ?? null;
                            return client;
                        }
                    } 
                }
            }
            return new Client();
        }

        private static MsSQLContext? instance;
        public static MsSQLContext Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new MsSQLContext();
                }
                return instance;
            }
        }
    }
}
