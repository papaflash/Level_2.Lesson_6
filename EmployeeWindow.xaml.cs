using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
namespace EmployeeAndDepartment
{
    /// <summary>
    /// Логика взаимодействия для addEmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private ObservableCollection<Department> _department;
        private ObservableCollection<Employee> _employees;
        private ListView _listView;
        private bool _change;
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        public EmployeeWindow(ObservableCollection<Department> department, ObservableCollection<Employee> employees, ListView listView, bool change) : this()
        {
            _department = department;
            _employees = employees;
            _listView = listView;
            _change = change;
            departmentBox.ItemsSource = _department;
            if (_change) InitChangeEmployee();
        }
        /// <summary>
        /// Метод добавления нового департамента
        /// </summary>
        private void AddNewDeppartment()
        {
            _department.Add(new Department(newDepartment.Text));
            newDepartment.Text = "";
        }
        #region Обработчики по клику или по нажатой Enter вызывают метод для добавления нового департамента
        private void NewDepartment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && newDepartment.Text != "") AddNewDeppartment();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (newDepartment.Text != "") AddNewDeppartment();
        }
        #endregion
        /// <summary>
        /// Метод добавления нового сотрудника
        /// </summary>
        private void AddNewEmployee()
        {
            if (IsValidateEmployer())
            {
                _employees.Add(new Employee(nameTxBox.Text, middleNameTxBox.Text, lastNameTxBox.Text, departmentBox.Text));
                nameTxBox.Text = "";
                middleNameTxBox.Text = "";
                lastNameTxBox.Text = "";
                departmentBox.Text = "";
            }
            else MessageBox.Show("Поля с ФИО должны быть заполнены, в них не должно быть пробелов и спец. символов кроме -");
        }
        /// <summary>
        /// Добавляем по клику нового сотрудника либо, изменяем выбранного
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_change)
                ChangeEmployee();
            else
                AddNewEmployee();
        }
        #region Методы заполнения полей ФИО выбранным сотрудником для изменения
        private void InitChangeEmployee()
        {
            nameTxBox.Text = (_listView.SelectedItem as Employee).Name;
            middleNameTxBox.Text = (_listView.SelectedItem as Employee).MiddleName;
            lastNameTxBox.Text = (_listView.SelectedItem as Employee).LastName;
            departmentBox.SelectedItem = (_listView.SelectedItem as Employee).Department;
            addOrChangeBtn.Content = "Изменить";
        }
        private void ChangeEmployee()
        {
            int i = _listView.SelectedIndex;
            if (i != -1 && IsValidateEmployer())
            {
                _employees[i].Name = nameTxBox.Text;
                _employees[i].MiddleName = middleNameTxBox.Text;
                _employees[i].LastName = lastNameTxBox.Text;
                _employees[i].Department = departmentBox.Text;
                _listView.Items.Refresh();
            }
            else MessageBox.Show("Поля с ФИО должны быть заполнены, в них не должно быть пробелов и спец. символов кроме -");
        }
        #endregion
        #region Обработчики Связывания лейблов с текстовыми полями
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nameTxBox.Focus();
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            middleNameTxBox.Focus();
        }

        private void Label_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            lastNameTxBox.Focus();
        }

        private void DepLbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newDepartment.Focus();
        }
        #endregion
        #region Метод Проверки на корректность заполнения полей
        private bool IsValidateEmployer()
        {
            string pattern = @"^\w+\-?\w+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(nameTxBox.Text) && regex.IsMatch(middleNameTxBox.Text) && regex.IsMatch(lastNameTxBox.Text) && departmentBox.SelectedItem != null)
                return true;
            return false;
        }
        #endregion
    }
}
