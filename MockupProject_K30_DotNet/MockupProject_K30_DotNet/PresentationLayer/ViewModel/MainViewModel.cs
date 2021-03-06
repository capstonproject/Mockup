﻿using MockupProject_K30_DotNet.DataAccessLayer;
using MockupProject_K30_DotNet.PresentationLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.DirectoryServices.AccountManagement;
using System.Windows.Controls;

namespace MockupProject_K30_DotNet.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        public enum Visibility
        {            
            Hidden,            
            Visible
        }
        
        private ObservableCollection<TabItem> tabs;
        public ObservableCollection<TabItem> Tabs
        {
            get
            {
                return tabs;
            }
            set
            {
                tabs = value;
                NotifyPropertyChanged("Tabs");
            }
        }

        private TabItem selectedTab;
        public TabItem SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                selectedTab = value;
                NotifyPropertyChanged("SelectedTab");
            }
        }

        private Employee _resultEmployee;
        public Employee ResultEmployee
        {
            get { return _resultEmployee; }
            set
            {
                _resultEmployee = value;
                NotifyPropertyChanged("ResultEmployee");
            }
        }
        private string _keySearch;
        public string KeySearch
        {
            get { return _keySearch; }
            set
            {
                _keySearch = value;
                NotifyPropertyChanged("KeySearch");
            }
        }

        private ObservableCollection<FSU> _listFsu;

        public ObservableCollection<FSU> ListFsu
        {
            get { return _listFsu; }
            set
            {
                if (_listFsu != value)
                {
                    _listFsu = value;
                    NotifyPropertyChanged("ListFsu");
                }
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged("DisplayName");
            }
        }

        private string _abbreviationDisplayName;
        public string AbbreviationDisplayName
        { 
            get { return _abbreviationDisplayName; }
            set
            {
                _abbreviationDisplayName = value;
                NotifyPropertyChanged("AbbreviationDisplayName");
            }
        }
        #endregion 

        #region Command
        private ICommand closeTabCommand;
        public ICommand CloseTabCommand
        {
            get
            {
                if (closeTabCommand == null)
                {
                    closeTabCommand = new RelayCommand(param => this.CloseTab(), null);
                }
                return closeTabCommand;
            }
        }
        
        private ICommand selectedChangedCommand;
        public ICommand SelectedChangedCommand
        {
            get
            {
                if (selectedChangedCommand == null)
                {
                    selectedChangedCommand = new RelayCommand(param => this.ChangeSelected(), null);
                }
                return selectedChangedCommand;
            }
        }

        private ICommand addTabCommand;
        public ICommand AddTabCommand
        {
            get
            {
                if (addTabCommand == null)
                {
                    addTabCommand = new RelayCommand(param => this.AddTab(),
                        null);
                }
                return addTabCommand;
            }
        }

        public ICommand SearchEmployeeCommand { get; set; }
        public ICommand SaveEmployeeCommand { get; set; }        
        #endregion 

        private void CloseTab()
        {
            if (SelectedTab != null)
            {
                this.Tabs.Remove(this.SelectedTab);
            }
            SelectedTab = null;
        }
        private void AddTab()
        {            
            int count = Tabs.Count();
            TabItem tab = new TabItem();
            tab.Header = string.Format("Tab {0}", count);
            tab.Content = string.Format("Tab {0} content", count);
            tab.CloseButtonVisibility = Visibility.Hidden.ToString();
            var tempHeaderTab = tab.Header;
            Tabs.Insert(count - 1, tab);           
            SelectedTab = Tabs.Where(tabAdd => tabAdd.Header.Equals(tempHeaderTab)).First();
            
        }
        private void ChangeSelected()
        {
            foreach (var tab in this.Tabs)
            {
                tab.CloseButtonVisibility = Visibility.Hidden.ToString();
            }
            if (this.SelectedTab != null && this.SelectedTab.Header.Equals("+") )
            {                
                AddTab();
            }
            else if (this.SelectedTab != null)
            {
                this.Tabs.Where(tab => tab.Header.Equals(this.SelectedTab.Header)).First().CloseButtonVisibility = Visibility.Visible.ToString();
            }          
        }

        private void SearchEmployee(object param)
        {
            if (new EmployeeDAL().SearchEmployeeByName(KeySearch).Count > 0)
            {
                ResultEmployee = new EmployeeDAL().SearchEmployeeByName(KeySearch)[0];
            }
            else
            {
                ResultEmployee.ID = 0;
                ResultEmployee.FirstName = "";
                ResultEmployee.LastName = "";
                ResultEmployee.Email = "";
                ResultEmployee.Position = "";
                ResultEmployee.FSU = "";
                MessageBox.Show("No items match your search.");
            }
        }

        private void SaveEmployee(object param)
        {
            // check available
            if (new EmployeeDAL().IsAvailabe(ResultEmployee.Email) == true)
            {
                MessageBoxResult dialog = new MessageBoxResult();
                dialog = MessageBox.Show("Employee availabe. Do you want to update information?", "Availabe employee!", MessageBoxButton.YesNoCancel);
                if (dialog == MessageBoxResult.Yes)
                {
                    // update
                    new EmployeeDAL().UpdateEmployeeByEmail(ResultEmployee);
                    MessageBox.Show("Updated Employee successful!");
                }
            }
            else
            {
                // add
                new EmployeeDAL().AddEmployee(ResultEmployee);
                MessageBox.Show("Saved Employee successful!");
            }
            // reset
            KeySearch = "";
            ResultEmployee.ID = 0;
            ResultEmployee.FirstName = "";
            ResultEmployee.LastName = "";
            ResultEmployee.Email = "";
            ResultEmployee.Position = "";
            ResultEmployee.FSU = "";
            LoadFSUers();
        }

        private string NameConverter(string name)
        {
            string convertedName = "";
            string[] words = name.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                convertedName += words[i];
                if (i != words.Length - 2)
                {
                    convertedName += " ";
                }
            }
            return convertedName;
        }

        private string AbbreviationNameConverter(string name)
        {
            string convertedName = "";
            string[] words = name.Split(' ');
            foreach (var word in words)
            {
                convertedName += word[0].ToString();
            }
            return convertedName;
        }

        public void LoadFSUers()
        {
            ListFsu = new ObservableCollection<FSU>();
            try
            {
                List<string> allFSU = new EmployeeDAL().GetAllFSU();

                foreach (var item in allFSU)
                {
                    var fsu = new FSU();
                    fsu.FsuName = item;
                    var listEm = new EmployeeDAL().GetEmployeeByFSU(item);

                    if (item == null)
                    {
                        fsu.FsuName = "NULL";
                        listEm = new EmployeeDAL().GetEmployeeNullFSU();
                    }

                    foreach (var employee in listEm)
                    {
                        EmployeeDetail employeeDetail = new EmployeeDetail();
                        employeeDetail.EmployTemplate = employee.ID + " - " + employee.LastName + " " + employee.FirstName;
                        employeeDetail.Detail.Add("First name: " + employee.FirstName);
                        employeeDetail.Detail.Add("Last name: " + employee.LastName);
                        employeeDetail.Detail.Add("Email: " + employee.Email);
                        employeeDetail.Detail.Add("Position: " + employee.Position);
                        fsu.Employees.Add(employeeDetail);
                    }
                    ListFsu.Add(fsu);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No data!");
            }
            
        }

        
        #region Constructor
        public MainViewModel()
        {
            LoadFSUers();

            SearchEmployeeCommand = new RelayCommand(SearchEmployee);
            SaveEmployeeCommand = new RelayCommand(SaveEmployee);
            ResultEmployee = new Employee();

            try
            {
                DisplayName = NameConverter(UserPrincipal.Current.DisplayName);
                AbbreviationDisplayName = AbbreviationNameConverter(DisplayName);
            }
            catch (Exception)
            {
                MessageBox.Show("You don't log into domain Fsoft yet!");
            }            

            //this.Tabs = new ObservableCollection<TabItem>();

            //TabItem tab3 = new TabItem();
            //tab3.Header = "tab3";
            //tab3.Content = "example3";
            //tab3.CloseButtonVisibility = Visibility.Hidden.ToString();
            //this.Tabs.Add(tab3);

            //TabItem tabAdd = new TabItem();
            //tabAdd.Header = "+";
            //tabAdd.CloseButtonVisibility = Visibility.Hidden.ToString();
            //this.Tabs.Add(tabAdd);
        }
        #endregion


        
    }
}
