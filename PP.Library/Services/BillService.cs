using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;

namespace PP.Library.Services
{
    public class BillService
    {
        private static BillService? instance;
        public static BillService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillService();
                }
                return instance;
            }
        }

        private List<Bill> bills;
        private BillService()
        {
            bills = new List<Bill>();
        }
        public List<Bill> Bills
        {
            get { return bills; }
        }
        public Bill? GetBill(int id)
        {
            return bills.FirstOrDefault(b => b.Id == id);
        }

        public void AddOrUpdateBill(Bill bill)
        {
            if (bill != null)
            {
                var billToUpdate = bills.FirstOrDefault(b => b.Id == bill.Id);
                if (billToUpdate != null)
                {
                    billToUpdate.Id = bill.Id;
                    billToUpdate.DueDate = bill.DueDate;
                    billToUpdate.TotalAmount = bill.TotalAmount;
                }
                else
                {
                    bill.Id = LastId + 1;
                    bills.Add(bill);
                }
            }

        }

        public void DeleteBill(int id)
        {
            var billToRemove = bills.FirstOrDefault(b => b.Id == id);
            if (billToRemove != null)
            {
                bills.Remove(billToRemove);
            }

        }

        private int LastId
        {
            get
            {
                return bills.Any() ? bills.Select(b => b.Id).Max() : 0;
            }
        }
    }
}
