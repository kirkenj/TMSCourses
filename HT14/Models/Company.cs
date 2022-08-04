using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT14.Models
{
    public class Company
    {
        private readonly List<Employee> _employees = new ();

        public Company()
        {

        }

        public Employee[] Employees => _employees.ToArray();
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _employees.Add(employee);
        }

        public bool Contains(Employee employee) => _employees.Contains(employee);
        //public void SetPost(Employee employee, )
        //{

        //}
    }
}
