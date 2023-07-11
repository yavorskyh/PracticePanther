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

        public Bill? GetBillByProjectID(int id)
        {
            return bills.FirstOrDefault(b => b.ProjectId == id);
        }

        public List<Bill> SearchByClientID(int id)
        {
            var projects = ProjectService.Current.Projects.Where(p => p.ClientID == id);
            var bills = new List<Bill>();

            foreach (var project in projects) 
                bills.Add(GetBillByProjectID(project.Id));
            

            return bills;
        }

        public void AddOrUpdateBill(Bill bill)
        {
            if (bill != null)
            {
                var billToUpdate = bills.FirstOrDefault(b => b.Id == bill.Id);
                if (billToUpdate != null)
                {
                    billToUpdate.ProjectId = bill.ProjectId;
                    billToUpdate.DueDate = bill.DueDate;
                    billToUpdate.TotalAmount = TotalAmountCalc(bill.ProjectId);
                }
                else
                {
                    bill.TotalAmount = TotalAmountCalc(bill.ProjectId);
                    bill.Id = LastId + 1;
                    bills.Add(bill);
                }
            }

        }

        private decimal TotalAmountCalc(int projectId)
        {
            var times = TimeService.Current.Times.Where(t => t.ProjectId == projectId);
            decimal totalAmount = 0;
            foreach (Time time in times)
            {
                var employee = EmployeeService.Current.GetEmployee(time.EmployeeId);
                if (employee != null) 
                {
                    totalAmount += employee.Rate * time.Hours;
                }
            }
            return totalAmount;
            
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
