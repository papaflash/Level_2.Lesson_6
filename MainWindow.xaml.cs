﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

/*Шагов Александр. Уровень_2.Урок_5:
   1. Создать сущности Employee и Department и заполнить списки сущностей начальными данными.
   2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение).
        Это можно сделать, например, с использованием ComboBox или ListView.
   3. Предусмотреть редактирование сотрудников и департаментов. Должна быть возможность изменить департамент у сотрудника.
        Список департаментов для выбора можно выводить в ComboBox, и все это можно выводить на дополнительной форме.
   4. Предусмотреть возможность создания новых сотрудников и департаментов.
        Реализовать это либо на форме редактирования, либо сделать новую форму.
 */

namespace EmployeeAndDepartment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Department> _department;
        private ObservableCollection<Employee> _employees;
        private string _pathEmp = $"{Environment.CurrentDirectory}\\employees.json";
        private string _pathDep = $"{Environment.CurrentDirectory}\\departments.json";
        public MainWindow()
        {
            InitializeComponent();
            InitEmployeeDepartment();
        }
        #region Заполняем начальными данными или же загружаем уже ранее сохраненые (имитация БД)
        private void InitEmployeeDepartment()
        {
            if (File.Exists(_pathEmp) && File.Exists(_pathDep))
            {
               _employees = LoadEmp();
               _department = LoadDep();
            }
            else
            {
                _department = new ObservableCollection<Department>() 
                {   new Department("Отдел продаж"),
                    new Department("Отдел маркетинга"),
                    new Department("Склад"),
                    new Department("Отдел сервиса и брака")
                };
                _employees = new ObservableCollection<Employee>()
                {
                    new Employee("Василий", "Степанович", "Горшков", _department[1].NameDepartment),
                    new Employee("Аркадий", "Иванович", "Смирнов", _department[0].NameDepartment),
                    new Employee("Алексей", "Валерьевич", "Иванов", _department[2].NameDepartment),
                    new Employee("Светлана", "Александровна", "Приходько", _department[3].NameDepartment),
                    new Employee("Жанна", "Васильевна", "Морозова", _department[0].NameDepartment)
                };
            }
            employeeList.ItemsSource = _employees;
        }
        #endregion
        #region Обработчики кнопок вызывают форму для изменения или создания нового сотрудника
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeeWindow addEmpWindow = new EmployeeWindow(_department, _employees, employeeList, false)
            {
                Owner = this
            };
            addEmpWindow.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EmployeeWindow addEmpWindow = new EmployeeWindow(_department, _employees, employeeList, true)
            {
                Owner = this
            };
            addEmpWindow.Show();
        }
        #endregion
        /// <summary>
        /// Обработчик удаления выбранного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить выбранного сотрудника?", "Удаление сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _employees.Remove(employeeList.SelectedItem as Employee);
        }
        #region Серелизация/Десерелизация Json имитация БД
        private void SaveEmp()
        {
            using (StreamWriter empFile = File.CreateText(_pathEmp))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(empFile, _employees);
            }
        }
        private void SaveDep()
        {
            using (StreamWriter depFile = File.CreateText(_pathDep))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(depFile, _department);
            }
        }
        private ObservableCollection<Employee> LoadEmp()
        {
            using (StreamReader empReader = File.OpenText(_pathEmp))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                return (ObservableCollection<Employee>)jsonSerializer.Deserialize(empReader, typeof(ObservableCollection<Employee>));
            }
        }
        private ObservableCollection<Department> LoadDep()
        {
            using (StreamReader empReader = File.OpenText(_pathDep))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                return (ObservableCollection<Department>)jsonSerializer.Deserialize(empReader, typeof(ObservableCollection<Department>));
            }
        }
        #endregion

        /// <summary>
        /// Обработчик до закрытия формы сохрнаить/не сохранить сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show($"Сохранить изменения?", "Запись в базу", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveEmp();
                SaveDep();
            }
           
        }
    }
}
