using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public Boolean isActive { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int ClientID { get; set; }

        public Project()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
        }

        public override string ToString()
        {
            return $"\nID: {Id}\n" +
                $"OpenDate: {OpenDate}\n" +
                $"CloseDate: {CloseDate}\n" +
                $"isActive: {isActive}\n" +
                $"Name: {ShortName}\n" +
                $"Notes: {LongName}\n" +
                $"ClientID: {ClientID}\n";
        }
    }
}
