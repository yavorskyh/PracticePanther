using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP.Library.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalAmount { get; set; }
    }
}
