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
        public Boolean isActive { get; set; } = true;
        public string? Name { get; set; }
        public string? Notes { get; set; }

      

        public override string ToString()
        {
            return $"{Id}.  {Name}\n" +
                   $"Is open? {isActive}";
        }

        public string GetDetails()
        {
            return $"ID: {Id}\n" +
                $"Name: {Name}\n" +
                $"Open Date: {OpenDate}\n" +
                $"Close Date: {CloseDate}\n" +
                $"isActive: {isActive}\n" +
                $"Notes: {Notes}";
        }
    }
}
