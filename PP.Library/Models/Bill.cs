﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP.Library.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }

        public override string ToString()
        {
            return $"Project ID: {ProjectId}\n" +
                $"Due Date: {DueDate}\n" +
                $"Total Amount Due: {TotalAmount}";
        }
    }
}
