using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAndDepartment
{
    public class Employee
    {
        private string _name;
        private string _middleName;
        public Department Department { set; get; }
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        public string MiddleName
        {
            set { _middleName = value; }
            get { return _middleName; }
        }
        public string LastName { set; get; }
        public Employee(string name, string middleName, string lastName, Department department)
        {
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            Department = department;
        }
        public Employee() { }
    }
}
