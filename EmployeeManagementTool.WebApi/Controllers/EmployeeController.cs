using EmployeeManagementTool.WebApi.ApiModels;
using Emt.Repository.Interface;
using EMT.Entities.DataModels;
using EMT.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace EmployeeManagementTool.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext _context;
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeController(EmployeeDBContext context, IEmployeeRepo employeeRepo)
        {
            _context = context;
            _employeeRepo = employeeRepo;
        }
        [HttpGet]
        public ApiResponse<List<EmployeeDTO>> Get()
        {
            List<EmployeeDTO> employees = _employeeRepo.AllEmployee();
            var apiResponse = new ApiResponse<List<EmployeeDTO>>();
            return apiResponse.HandleResponse(employees);

            /*var values = _context.Employees.Where(employee => employee.DeletedAt == null).ToList();
            return Ok(values);*/
        }
        [HttpGet]
        [Route("{id}")]
        public ApiResponse<EmployeeDTO> GetById([FromRoute] long id)
        {
            var employee = _employeeRepo.EmployeeByID(id);
            var apiResponse = new ApiResponse<EmployeeDTO>();
            if (employee != null)
            {
                return apiResponse.HandleResponse(employee);
            }
            else
            {
                return apiResponse.HandleException("Enter Valid Details");
            }
            /*var values = _context.Employees.Where(employee=>employee.EmpId == id).ToList();
            return Ok(values);*/
        }
        [HttpPost]
        public ApiResponse<Employee> CreateEmployee([FromBody] CreateEmployeeModel createEmployeeModel)
        {

            var apiResponse = new ApiResponse<Employee>();
            if (ModelState.IsValid)
            {
                var employee = _employeeRepo.CreateEmployee(createEmployeeModel);
                return apiResponse.HandleResponse(employee);

            }
            return apiResponse.HandleException("Enter All Details");

            /*var employee = new Employee()
            {
                Firstname = createEmployeeModel.Firstname,
                Lastname = createEmployeeModel.Lastname,
                Email = createEmployeeModel.Email,
                Dob = createEmployeeModel.Dob,
                Gender = createEmployeeModel.Gender,
                Password = createEmployeeModel.Password,
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = employee.EmpId }, employee);*/
        }
        [HttpPut]
        [Route("{Id}")]
        public ApiResponse<Employee> UpdateEmployee([FromRoute] long Id, CreateEmployeeModel UpdateEmployeeModel)
        {
            /*var employee = _context.Employees.Find(Id);
            if (employee != null)
            {
                employee.Firstname = UpdateEmployeeModel.Firstname;
                employee.Lastname = UpdateEmployeeModel.Lastname;
                employee.Email = UpdateEmployeeModel.Email;
                employee.Gender = UpdateEmployeeModel.Gender;
                employee.Dob = UpdateEmployeeModel.Dob;
                employee.Password = UpdateEmployeeModel.Password;
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = employee.EmpId }, employee);
            }
            return NotFound();*/
            var apiResponse = new ApiResponse<Employee>();
            if (ModelState.IsValid)
            {
                var employee = _employeeRepo.UpdateEmployee(Id, UpdateEmployeeModel);
                return apiResponse.HandleResponse(employee);
            }
            return apiResponse.HandleException("Enter All Details");


        }
        [HttpDelete]
        [Route("{Id}")]
        public ApiResponse<Employee> DeleteEmployee([FromRoute] long Id)
        {
            var apiResponse = new ApiResponse<Employee>();
            var employee = _employeeRepo.DeleteEmployee(Id);
            if (employee != null)
            {
                return apiResponse.HandleResponse(employee);
            }
            else
            {
                return apiResponse.HandleException("ID Not Found");
            }

            /*var Employee = _context.Employees.Find(Id);
            Employee.DeletedAt = DateTime.Now;
            _context.Employees.Update(Employee);
            _context.SaveChanges();

            return Ok();*/
        }
        [HttpPut]
        public IActionResult UpdateExpiryDay(ExpiryDayDTO model)
        {
            var apiResponse = new ApiResponse<Employee>();
            _employeeRepo.UpdateExpiry(model);

            /*var apiResponse = new ApiResponse<Employee>();
            if (model != null)
            {

                return apiResponse.HandleResponse(model);
            }*/
            return Ok(model);
        }
        [HttpPatch]
        public ApiResponse<Employee> LockStatusChange(EmployeeIdDTO model)
        {
            var apiResponse = new ApiResponse<Employee>();
            var employees = _employeeRepo.ChnageLockStatus(model.EmployeeId);
            if (employees != null)
            {
                return apiResponse.HandleResponse(employees);
            }
            else
            {
                return apiResponse.HandleException("Enter Valid Id");
            }

        }
        [HttpGet]
        public ApiResponse<ConfigurationDTO> GetConfiguration()
        {
            var apiResponse = new ApiResponse<ConfigurationDTO>();
            var data = _employeeRepo.GetConfigurationData();
            if (data != null)
            {
                return apiResponse.HandleResponse(data);
            }
            else
            {
                return apiResponse.HandleException("No Data Found");
            }
        }
        
    }
}
