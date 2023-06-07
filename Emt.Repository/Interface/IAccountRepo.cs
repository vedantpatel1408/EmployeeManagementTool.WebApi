using EMT.Entities.DataModels;
using EMT.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMT.Repository.Interface
{
    public interface IAccountRepo
    {
        Employee getByEmail(string Email);
        void UpdateAttempt(CreateEmployeeModel model);
        void UpdateAttempsForWrong(CreateEmployeeModel model);
        void UpdateLockStatus(CreateEmployeeModel model);
        PasswordExpiry CheckStatusOfExpiry(long Id);
        bool StatusUpdate(long EmpId);
        bool UpdatePassword(long EmpId, PasswordDTO model);
        Employee CheckValidPassword(long EmpId);
    }
}
