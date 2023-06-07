using EmployeeManagementTool.WebApi.ApiModels;
using EMT.Entities.DataModels;
using EMT.Entities.Model;
using EMT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementTool.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EmployeeDBContext _context;
        private readonly IAccountRepo _accountRepo;
        public AccountController(EmployeeDBContext context, IAccountRepo accountRepo)
        {
            _context = context;
            _accountRepo = accountRepo;
        }
        [HttpGet]
        [Route("{Email}")]
        public ApiResponse<Employee> GetEmployeeEmail([FromRoute] string Email)
        {
            var apiResponse = new ApiResponse<Employee>();
            var employee = _accountRepo.getByEmail(Email);
            if (employee != null)
            {
                return apiResponse.HandleResponse(employee);
            }
            else
            {
                return apiResponse.HandleException("Email Not Found");
            }

        }
        [HttpPut]
        public IActionResult UpdateAttempt(CreateEmployeeModel model)
        {

            _accountRepo.UpdateAttempt(model);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateAttempsForWrong(CreateEmployeeModel model)
        {

            _accountRepo.UpdateAttempsForWrong(model);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateLockStatus(CreateEmployeeModel model)
        {

            _accountRepo.UpdateLockStatus(model);
            return Ok();
        }
        [HttpGet]
        [Route("{Id}")]
        public ApiResponse<PasswordExpiry> CheckStatusOfExpiry([FromRoute] long Id)
        {
            var apiResponse = new ApiResponse<PasswordExpiry>();
            var date = _accountRepo.CheckStatusOfExpiry(Id);
            if (date != null)
            {
                return apiResponse.HandleResponse(date);
            }
            else
            {
                return apiResponse.HandleException("Email Not Found");
            }

        }
        [HttpPut]
        public ApiResponse<string> StatusUpdate(EmployeeIdDTO model)
        {
            var apiResponse = new ApiResponse<string>();
            
            var status =  _accountRepo.StatusUpdate(model.EmployeeId);
            if (status)
            {
                return apiResponse.HandleResponse("Update Successfully Completed");
            }
            else
            {

                return apiResponse.HandleException("Invalid Employee Id");
            }
            
        }
        [HttpPut]
        [Route("{EmpId}")]
        public ApiResponse<string> UpdatePassWord([FromRoute] long EmpId,PasswordDTO model)
        {
            var apiResponse = new ApiResponse<string>();
            var status = _accountRepo.UpdatePassword(EmpId, model);
            if (status)
            {
                return apiResponse.HandleResponse("Password Updated SuccessFully");
            }
            else
            {
                return apiResponse.HandleException("Something Went Wrong");
            }


        }
        [HttpGet]
        [Route("{EmpId}")]
        public ApiResponse<Employee> CheckValidPassword(long EmpId)
        {
            var apiResponse = new ApiResponse<Employee>();
            var data = _accountRepo.CheckValidPassword(EmpId);
            if (data != null)
            {
                return apiResponse.HandleResponse(data);
            }
            else
            {
                return apiResponse.HandleException("Invalid Id");
            }
        }
    }
}
