using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EmployeeAndDepartment
{
    public class Department : INotifyPropertyChanged
    {
        private string _nameDepartment;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public string NameDepartment
        {
            set 
            {
               if(this._nameDepartment != value)
               {
                  this._nameDepartment = value;
                  this.NotifyPropertyChanged("NameDepartment");
               }  
            }
            get { return this._nameDepartment; }
        }

        public Department(string department)
        {
            NameDepartment = department;
        }
        public override string ToString()
        {
            return NameDepartment;
        }
        //public override int GetHashCode()
        //{
        //    return NameDepartment.GetHashCode();
        //}
        //public override bool Equals(object obj)
        //{
        //    if (obj.GetType() != this.GetType()) return false;
        //    var o = obj as Department;
        //    return this.NameDepartment == o.NameDepartment;
        //}
    }
}
