using MockupProject_K30_DotNet.DataAccessLayer;
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

        private List<FSU> _listFsu;

        public List<FSU> ListFsu
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
        #endregion //Properties

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
        private void CloseTab()
        {
            if (SelectedTab != null)
            {
                this.Tabs.Remove(this.SelectedTab);
            }            
            SelectedTab = null;
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

        
        #endregion //Command

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
            new EmployeeDAL().AddEmployee(ResultEmployee);
            MessageBox.Show("Saved Employee successful!");
            KeySearch = "";
            ResultEmployee.ID = 0;
            ResultEmployee.FirstName = "";
            ResultEmployee.LastName = "";
            ResultEmployee.Email = "";
            ResultEmployee.Position = "";
            ResultEmployee.FSU = "";
        }

        #region Constructor
        public MainViewModel()
        {
            ListFsu = new List<FSU>();
            List<string> allFSU = new EmployeeDAL().GetAllFSU();

            foreach(var item in allFSU)
            {
                var fsu = new FSU();
                fsu.FsuName = item;
                var listEm = new EmployeeDAL().GetEmployeeByFSU(item);

                if (item == null)
                {
                    fsu.FsuName = "NULL";
                    listEm = new EmployeeDAL().GetEmployeeNullFSU();
                }

                fsu.Employeees = listEm;
                ListFsu.Add(fsu);
                
            }

            //var listEmployee = new EmployeeDAL().GetAllEmployee();
            //for (int i = 0; i < listEmployee.Count; i++)
            //{
            //    var fsu = new FSU();
            //    fsu.FsuName = listEmployee[i].FSU;
            //    var listEm = new EmployeeDAL().GetEmployeeByFSU(listEmployee[i].FSU);
            //    fsu.Employeees = listEm;
            //    ListFsu.Add(fsu);
            //}

            SearchEmployeeCommand = new RelayCommand(SearchEmployee);
            SaveEmployeeCommand = new RelayCommand(SaveEmployee);
            ResultEmployee = new Employee();

            //this.Tabs = new ObservableCollection<TabItem>();

            //TabItem tab1 = new TabItem();
            //tab1.Header = "tab1";
            //tab1.Content = "example1";
            //tab1.CloseButtonVisibility = Visibility.Visible.ToString();
            //this.Tabs.Add(tab1);

            //TabItem tab2 = new TabItem();
            //tab2.Header = "tab2";
            //tab2.Content = "example2";
            //tab2.CloseButtonVisibility = Visibility.Hidden.ToString();
            //this.Tabs.Add(tab2);

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
