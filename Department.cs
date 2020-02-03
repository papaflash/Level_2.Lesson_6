using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace EmployeeAndDepartment
{
    public class Department
    {
        private string _nameDepartment;
        public string NameDepartment { set => _nameDepartment = value; get => _nameDepartment; }
        public Department(string department)
        {
            NameDepartment = department;
        }
        public override string ToString()
        {
            return NameDepartment;
        }
    }
}
