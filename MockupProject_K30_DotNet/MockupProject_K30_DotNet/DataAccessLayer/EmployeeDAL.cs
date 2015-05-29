using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MockupProject_K30_DotNet.DataAccessLayer
{
    public class EmployeeDAL
    {
        public List<Employee> GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();
            using (var context = new LinQModelDataContext())
            {
                employees = (from e in context.Employees
                             select e).ToList();
            }
            return employees;
        }

        public void SaveChangeEmployee(Employee employee)
        {
            using (var context = new LinQModelDataContext())
            {
                var employees = (from e in context.Employees
                                 where e.ID == employee.ID
                                 select e).ToList();
                foreach (Employee myEmployee in employees)
                {
                    myEmployee.FirstName = employee.FirstName;
                    myEmployee.LastName = employee.LastName;
                    myEmployee.Email = employee.Email;
                    myEmployee.FSU = employee.FSU;
                    myEmployee.Position = employee.Position;
                }
                context.SubmitChanges();
            }
        }

        public Employee GetEmployeeByID(int id)
        {
            Employee employee = new Employee();
            using (var context = new LinQModelDataContext())
            {
                employee = (from e in context.Employees
                            where e.ID == id
                            select e).SingleOrDefault();
            }
            return employee;
        }
        public List<Employee> GetEmployeeByFSU(string fsu)
        {
            List<Employee> employees = new List<Employee>();
            using (var context = new LinQModelDataContext())
            {
                employees = (from e in context.Employees
                             where e.FSU == fsu
                             select e).ToList();
            }
            return employees;
        }

        public List<Employee> SearchEmployeeByIdentity(string name)
        {
            List<Employee> asc = new List<Employee>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(context, name);

                Employee objSurveyUsers = new Employee();

                string displayName = user.DisplayName;
                // hoang dinh vu (fsu12.bu11)
                string[] words = displayName.Split(' ');
                objSurveyUsers.FirstName = words[words.Length - 2].ToString();
                objSurveyUsers.LastName = words[0];
                foreach (var word in words)
                {
                    if (word[0] == '(')
                    {
                        objSurveyUsers.FSU = word.Split('.')[0];
                        objSurveyUsers.Position = word.Split('.')[1];
                    }
                }

                objSurveyUsers.Email = user.EmailAddress;
                asc.Add(objSurveyUsers);
            }

            catch (Exception ex)
            {
                ex.InnerException.Message.ToString();
            }
            return asc;
        }

        public List<Employee> SearchEmployeeByName(string name)
        {
            List<Employee> employeesResult = new List<Employee>();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                UserPrincipal userPrin = new UserPrincipal(context);
                userPrin.Name = "*" + name + "*";
                var searcher = new PrincipalSearcher();
                searcher.QueryFilter = userPrin;
                var results = searcher.FindAll();
                var count = results.Count();
                
                foreach (var item in results)
                {
                    Employee employeeResult = new Employee();
                    string displayName = item.DisplayName;

                    //string[] words = displayName.Split(' ');
                    //employeeResult.FirstName = words[words.Length - 2].ToString();
                    //employeeResult.LastName = words[0];
                    //foreach (var word in words)
                    //{
                    //    if (word[0] == '(')
                    //    {
                    //        employeeResult.FSU = word.Split('.')[0];
                    //        employeeResult.Position = word.Split('.')[1];
                    //    }
                    //}

                    if (displayName.IndexOf(' ') != -1)
                    {
                        string[] words = displayName.Split(' ');
                        employeeResult.FirstName = words[words.Length - 2];
                        employeeResult.LastName = words[0];
                    }

                    if (displayName.IndexOf('(') != -1)
                    {
                        if (displayName.IndexOf('.') != -1)
                        {
                            employeeResult.FSU = displayName.Substring(displayName.IndexOf('(') + 1, displayName.IndexOf('.') - displayName.IndexOf('(') - 1);
                            employeeResult.Position = displayName.Substring(displayName.IndexOf('.') + 1, displayName.IndexOf(')') - displayName.IndexOf('.') - 1);
                        }
                        else
                        {
                            employeeResult.FSU = displayName.Substring(displayName.IndexOf('(') + 1, displayName.IndexOf('(') - displayName.IndexOf(')') - 1);
                        }
                    }
                    
                    employeeResult.Email = item.UserPrincipalName;
                    employeesResult.Add(employeeResult);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid format!");
            }
            return employeesResult;
        }


        public void AddEmployees(List<Employee> employees)
        {
            using (var context = new LinQModelDataContext())
            {
                context.Employees.InsertAllOnSubmit<Employee>(employees);
                context.SubmitChanges();
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (var context = new LinQModelDataContext())
            {
                context.Employees.InsertOnSubmit(employee);
                context.SubmitChanges();
            }
        }

        public List<string> GetAllFSU()
        {
            List<string> Fsu = new List<string>();
            using (var context = new LinQModelDataContext())
            {
                var query = (from e in context.Employees
                             select e.FSU).Distinct();
                foreach (var fsu in query)
                {
                    Fsu.Add(fsu);
                }
            }
            return Fsu;
        }
        
        internal List<Employee> GetEmployeeNullFSU()
        {
            List<Employee> employees = new List<Employee>();
            using (var context = new LinQModelDataContext())
            {
                employees = (from e in context.Employees
                             where e.FSU == null
                             select e).ToList();
            }
            return employees;
        }
    }
}
