using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PP.Library.Models
{
    public class Employee
    {
        public string? Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }
        
        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}
