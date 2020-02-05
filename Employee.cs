using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeAndDepartment
{
    public class Employee : INotifyPropertyChanged
    {
        private string _name;
        private string _middleName;
        private string _lastName;
        private Department _department;
        public event PropertyChangedEventHandler PropertyChanged;

        public Department Department 
        { set
            {
                if(_department != value)
                {
                    _department = value;
                    OnPropChanged("Department");
                }
            }
            get { return _department; }
        }
        public string Name
        {
            set
            { 
                if(_name != value)
                {
                    _name = value;
                    OnPropChanged("Name");
                } 
            }
            get { return _name; }
        }
        public string MiddleName
        {
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropChanged("MiddleName");
                }
            }
            get { return _middleName; }
        }
        public string LastName
        {
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropChanged("LastName");
                }
            }
            get { return _lastName; }
        }
        public Employee(string name, string middleName, string lastName, Department department)
        {
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            Department = department;
        }
        public Employee() { }
        public void OnPropChanged([CallerMemberName]string Property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
    }
}
