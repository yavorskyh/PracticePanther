using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;

namespace PP.Library.Services
{
    public class EmployeeService
    {
        private static EmployeeService? instance;
        public static EmployeeService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmployeeService();
                }
                
                return instance;
            }

        }

        private List<Employee> employees;
        private EmployeeService()
        {
            employees = new List<Employee>();
        }
        public List<Employee> Employees
        {
            get { return employees; }
        }
        public Employee? GetEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddorUpdateEmployee(Employee? employee)
        {
            if (employee != null)
            {
                var employeeToUpdate = employees.FirstOrDefault(e => e.Id == employee.Id);
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.Name = employee.Name;
                    employeeToUpdate.Rate = employee.Rate;
                    employeeToUpdate.Id = employee.Id;
                }
                else
                {
                    employee.Id = LastId + 1;
                    employees.Add(employee);
                }
            }
        }

        public void Read()
        {
            employees.ForEach(Console.WriteLine);
        }

        public void DeleteEmployee(int id)
        {
            var employeeToRemove = employees.FirstOrDefault(e => e.Id == id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }

        }

        public IEnumerable<Employee> Search(string query)
        {
            return employees
                .Where(e => e.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }

        public IEnumerable<Employee> SearchByEmployeeID(int id)
        {
            return employees.Where(e => e.Id == id);
        }

        private int LastId
        {
            get
            {
                return employees.Any() ? employees.Select(e => e.Id).Max() : 0;
            }
        }
    }
}
