using EMT.Entities.DataModels;
using EMT.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emt.Repository.Interface
{
    public interface IEmployeeRepo
    {
        List<EmployeeDTO> AllEmployee();
        EmployeeDTO EmployeeByID(long id);
        Employee CreateEmployee(CreateEmployeeModel createEmployeeModel);
        Employee UpdateEmployee(long Id, CreateEmployeeModel UpdateEmployeeModel);
        Employee DeleteEmployee(long Id);
        void UpdateExpiry(ExpiryDayDTO model);
        Employee ChnageLockStatus(long EmployeeId);
        ConfigurationDTO GetConfigurationData();

    }
}
