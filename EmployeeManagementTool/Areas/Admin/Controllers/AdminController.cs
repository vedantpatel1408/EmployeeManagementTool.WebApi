using Microsoft.AspNetCore.Mvc;
using EmployeeManagementTool.DataModels;
using EmployeeManagementTool.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Security;
using System.Net;

namespace EmployeeManagementTool.Controllers
{

    [Area("Admin")]
    public class AdminController : Controller
    {
        
        Uri baseUrl = new Uri("https://localhost:44397/api");
        private readonly EmployeeDBContext _employeeDB;
        private readonly HttpClient _client;
        public AdminController(EmployeeDBContext employeeDB)
        {
            _employeeDB = employeeDB;
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Admin _admin, string Type)
        {
            if (Type == "Login")
            {
                if (ModelState["Password"].Errors.Count > 0 || ModelState["Email"].Errors.Count > 0)
                {
                    return View();
                }
                else
                {
                    var AdminCredetial = _employeeDB.Admins.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);
                    if (AdminCredetial != null)
                    {
                        if (AdminCredetial.Password == _admin.Password)
                        {
                            HttpContext.Session.SetString("Name", AdminCredetial.Firstname + " " + AdminCredetial.Lastname);
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            TempData["Wrong PassWord"] = "Wrong Password";
                            if (AdminCredetial.Attempts < 3)
                            {
                                AdminCredetial.Attempts = AdminCredetial.Attempts + 1;
                                _employeeDB.Admins.Update(AdminCredetial);
                                _employeeDB.SaveChanges();
                            }
                            else
                            {

                                AdminCredetial.IsLocked = true;
                                _employeeDB.Admins.Update(AdminCredetial);
                                _employeeDB.SaveChanges();
                                TempData["Locked"] = "Your Accout is Locked";
                            }

                        }
                    }
                    else
                    {
                        TempData["Not Exist"] = "User Not exist";
                        return View();
                    }
                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    var AdminRegister = new Admin()
                    {
                        Email = _admin.Email,
                        Password = _admin.Password,
                        Firstname = _admin.Firstname,
                        Lastname = _admin.Lastname,
                        Dob = _admin.Dob,
                        TotalAttempts = 3,
                        /*Gender = _admin.Gender,*/
                    };
                    _employeeDB.Add(AdminRegister);
                    _employeeDB.SaveChanges();
                    return RedirectToAction("LogIn");
                }
                return View(_admin);
            }

            /*if (ModelState.IsValid)
            {
                return View(_admin);
            }*/
            return View(_admin);
        }

        public IActionResult Register(Employee employee, string dateofbirth)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LogIn");
            }
            return RedirectToAction("LogIn");
        }

        public IActionResult Dashboard()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpGet]
        public IActionResult Configuration()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpPost]
        public IActionResult Configuration(int TotalAttempts, int PasswordExpiryDays)
        {
            var Employees = _employeeDB.Employees.ToList();
            foreach (var employee in Employees)
            {
                employee.TotalAttempts = TotalAttempts;
                _employeeDB.Employees.Update(employee);
            }
            var Expiryday = _employeeDB.PasswordExpiries.ToList();
            foreach (var exp in Expiryday)
            {
                exp.PasswordexpiryDay = PasswordExpiryDays;
                _employeeDB.PasswordExpiries.Update(exp);
            }
            _employeeDB.SaveChanges();
            return View();
        }
        public IActionResult GetConfigurationData()
        {
            var value = (from emp in _employeeDB.Employees
                         join pass in _employeeDB.PasswordExpiries on emp.EmpId equals pass.EmpId
                         select new
                         {
                             day = pass.PasswordexpiryDay,
                             total = emp.TotalAttempts
                         });
            return Json(new { value });
        }
        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            ViewBag.Name = HttpContext.Session.GetString("Name");

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
            }
            return View(employees);

            /*var Employees = _employeeDB.Employees.Where(model=>model.DeletedAt == null).ToList();
            return View(Employees);*/
        }



        public IActionResult ChangeLockStatus(long EmployeeId)
        {
            var EmployeeDetails = _employeeDB.Employees.FirstOrDefault(employee => employee.EmpId == EmployeeId);
            if (EmployeeDetails.IsLocked == true)
            {
                EmployeeDetails.IsLocked = false;
            }
            else
            {
                EmployeeDetails.IsLocked = true;
            }
            _employeeDB.Employees.Update(EmployeeDetails);
            _employeeDB.SaveChanges();
            return Json(new { z = 1 });
        }
        public IActionResult DeleteEmployee(long EmployeeId)
        {
            var EmployeeDetails = _employeeDB.Employees.FirstOrDefault(employee => employee.EmpId == EmployeeId);
            EmployeeDetails.DeletedAt = DateTime.Now;
            _employeeDB.Employees.Update(EmployeeDetails);
            _employeeDB.SaveChanges();
            return Json(new { z = 1 });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }
    }
}
