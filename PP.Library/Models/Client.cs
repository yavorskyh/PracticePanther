using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PP.Library.Models
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public Boolean isActive { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public Client()
        {
            Name = string.Empty;
            Notes = string.Empty;
        }

        public override string ToString() 
        {
            return $"\nID: {Id}\n" +
                $"OpenDate: {OpenDate}\n" +
                $"CloseDate: {CloseDate}\n" +
                $"isActive: {isActive}\n" +
                $"Name: {Name}\n" +
                $"Notes: {Notes}\n";
        }
    }
}
