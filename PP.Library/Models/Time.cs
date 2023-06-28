using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PP.Library.Models
{
    public class Time
    {
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public override string ToString()
        {
            return $"Date: {Date}\n" +
                   $"Narrative: {Narrative}\n" +
                   $"Hours: {Hours}\n" +
                   $"Project ID: {ProjectId}\n" +
                   $"Employee ID: {EmployeeId}";
        }
    }
}
