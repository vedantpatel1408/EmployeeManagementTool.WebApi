using EMT.Entities.DataModels;
using EMT.Entities.Model;
using EMT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMT.Repository.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly EmployeeDBContext _context;
        public AccountRepo(EmployeeDBContext context)
        {
            _context = context;
        }
        public Employee getByEmail(string Email)
        {
            var EmployeeCredential = _context.Employees.FirstOrDefault(employee => employee.Email == Email);
            if (EmployeeCredential != null)
            {
                return EmployeeCredential;
            }
            else
            {
                return null;
            }
        }
        public void UpdateAttempt(CreateEmployeeModel model)
        {
            var EmployeeCredetial = _context.Employees.FirstOrDefault(employee => employee.Email == model.Email);
            EmployeeCredetial.Attempts = 0;
            _context.Employees.Update(EmployeeCredetial);
            _context.SaveChanges();
        }
        public void UpdateAttempsForWrong(CreateEmployeeModel model)
        {
            var EmployeeCredetial = _context.Employees.FirstOrDefault(employee => employee.Email == model.Email);
            EmployeeCredetial.Attempts = EmployeeCredetial.Attempts + 1;
            _context.Employees.Update(EmployeeCredetial);
            _context.SaveChanges();
        }
        public void UpdateLockStatus(CreateEmployeeModel model)
        {
            var EmployeeCredetial = _context.Employees.FirstOrDefault(employee => employee.Email == model.Email);
            EmployeeCredetial.IsLocked = true;
            _context.Employees.Update(EmployeeCredetial);
            _context.SaveChanges();
        }
        public PasswordExpiry CheckStatusOfExpiry(long Id)
        {
            var date = _context.PasswordExpiries.FirstOrDefault(m => m.EmpId == Id);
            if (date != null)
            {
                return date;
            }
            else
            {
                return null;
            }
        }
        public bool StatusUpdate(long EmpId)
        {
            var date = _context.PasswordExpiries.FirstOrDefault(m => m.EmpId == EmpId);
            if (date != null)
            {
                if (date.PasswordexpiryStatus)
                {
                    date.PasswordexpiryStatus = false;
                }
                else
                {
                    date.PasswordexpiryStatus = true;
                }
                
                _context.PasswordExpiries.Update(date);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
             
        }
        public bool UpdatePassword(long EmpId, PasswordDTO model)
        {
            var oldPassword = _context.Employees.FirstOrDefault(password => password.EmpId == EmpId);
            if(oldPassword != null)
            {
                oldPassword.Password = model.ConfirmNewPassword;
                _context.Employees.Update(oldPassword);
                
                var ExpiryStatus = _context.PasswordExpiries.Find(EmpId);
                ExpiryStatus.PasswordUpdated = DateTime.Now;
                _context.PasswordExpiries.Update(ExpiryStatus);

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public Employee CheckValidPassword(long EmpId)
        {
            var oldPassword = _context.Employees.FirstOrDefault(password => password.EmpId == EmpId);
            return oldPassword;
        }

    }
}
