using Emt.Repository.Interface;
using EMT.Entities.DataModels;
using EMT.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emt.Repository.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeDBContext _context;
        public EmployeeRepo(EmployeeDBContext context)
        {
            _context = context;
        }

        public List<EmployeeDTO> AllEmployee()
        {
            var tblEmployee = _context.Employees.Where(employee => employee.DeletedAt == null).ToList();

            var employeeDTO = new List<EmployeeDTO>();
            foreach (var employee in tblEmployee)
            {
                employeeDTO.Add(new EmployeeDTO()
                {
                    EmpId = employee.EmpId,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Email = employee.Email,
                    Dob = employee.Dob,
                    Gender = employee.Gender,
                    Attempts = employee.Attempts,
                    Password = employee.Password,
                    Status = employee.Status,
                    IsLocked = employee.IsLocked,
                });
            }
            return employeeDTO;
        }
        public EmployeeDTO EmployeeByID(long id)
        {
            var tblEmployee = _context.Employees.FirstOrDefault(employee => employee.DeletedAt == null && employee.EmpId == id);
            if (tblEmployee != null)
            {
                var employeeDTO = new EmployeeDTO()


                {
                    EmpId = tblEmployee.EmpId,
                    Firstname = tblEmployee.Firstname,
                    Lastname = tblEmployee.Lastname,
                    Email = tblEmployee.Email,
                    Dob = tblEmployee.Dob,
                    Gender = tblEmployee.Gender,
                    Attempts = tblEmployee.Attempts,
                    Password = tblEmployee.Password,
                    Status = tblEmployee.Status,
                };
                return employeeDTO;
            }
            else
            {

                return null;
            }


        }
        public Employee CreateEmployee(CreateEmployeeModel createEmployeeModel)
        {
            var employee = new Employee()
            {
                Firstname = createEmployeeModel.Firstname,
                Lastname = createEmployeeModel.Lastname,
                Email = createEmployeeModel.Email,
                Dob = createEmployeeModel.Dob,
                Gender = createEmployeeModel.Gender,
                Password = createEmployeeModel.Password,
            };
            _context.Employees.Add(employee);

            var expiryDay = _context.PasswordExpiries.FirstOrDefault().PasswordexpiryDay;
            var passwordStatus = new PasswordExpiry()
            {
                EmpId = employee.EmpId,
                PasswordexpiryDay = expiryDay,
                PasswordexpiryStatus = false,
                PasswordUpdated = DateTime.Now,
            };
            _context.PasswordExpiries.Add(passwordStatus);
            _context.SaveChanges();
            return employee;
        }
        public Employee UpdateEmployee(long Id, CreateEmployeeModel UpdateEmployeeModel)
        {
            var employee = _context.Employees.Find(Id);
            employee.Firstname = UpdateEmployeeModel.Firstname;
            employee.Lastname = UpdateEmployeeModel.Lastname;
            employee.Email = UpdateEmployeeModel.Email;
            employee.Gender = UpdateEmployeeModel.Gender;
            employee.Dob = UpdateEmployeeModel.Dob;
            employee.Password = UpdateEmployeeModel.Password;
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }
        public Employee DeleteEmployee(long Id)
        {
            var employee = _context.Employees.Find(Id);
            if (employee != null)
            {
                employee.DeletedAt = DateTime.Now;
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return employee;
            }
            else
            {
                return null;
            }

        }
        public void UpdateExpiry(ExpiryDayDTO model)
        {
            var Employees = _context.Employees.ToList();
            foreach (var employee in Employees)
            {
                employee.TotalAttempts = model.TotalAttempts;
                _context.Employees.Update(employee);
            }
            var Expiryday = _context.PasswordExpiries.ToList();
            foreach (var exp in Expiryday)
            {
                exp.PasswordexpiryDay = model.PasswordExpiryDays;
                _context.PasswordExpiries.Update(exp);
            }
            _context.SaveChanges();
        }
        public Employee ChnageLockStatus(long EmployeeId)
        {
            var EmployeeDetails = _context.Employees.FirstOrDefault(employee => employee.EmpId == EmployeeId);
            if (EmployeeDetails.IsLocked == true)
            {
                EmployeeDetails.IsLocked = false;
            }
            else
            {
                EmployeeDetails.IsLocked = true;
            }
            _context.Employees.Update(EmployeeDetails);
            _context.SaveChanges();
            return EmployeeDetails;
        }
        public ConfigurationDTO GetConfigurationData()
        {
            var day = _context.PasswordExpiries.FirstOrDefault().PasswordexpiryDay;
            var totalAttempt = _context.Employees.FirstOrDefault().TotalAttempts;
            var employee = new ConfigurationDTO()
            {
                PasswordexpiryDay = day,
                TotalAttempts = totalAttempt
            };
            return employee;

        }
    }
}
