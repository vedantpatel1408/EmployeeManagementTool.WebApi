using EmployeeManagementTool.DataModels;
using EmployeeManagementTool.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementTool.Areas.Employees.Controllers
{
    [Area("Employees")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBContext _context;
        public EmployeeController(EmployeeDBContext context)
        {
            _context = context;
        }
        public IActionResult EmployeeLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EmployeeLogin(RegisterViewModel _employee, string Type, string Gender)
        {
            if (Type == "Login")
            {
                if (ModelState["Password"].Errors.Count > 0 || ModelState["Email"].Errors.Count > 0)
                {

                }
                else
                {
                    var EmployeeCredetial = _context.Employees.FirstOrDefault(employee => employee.Email == _employee.Email);
                    if (EmployeeCredetial != null)
                    {
                        if (EmployeeCredetial.Password == _employee.Password)
                        {
                            HttpContext.Session.SetString("Name", EmployeeCredetial.Firstname + " " + EmployeeCredetial.Lastname);
                            HttpContext.Session.SetInt32("EmpID", (int)EmployeeCredetial.EmpId);
                            EmployeeCredetial.Attempts = 0;
                            _context.Employees.Update(EmployeeCredetial);
                            _context.SaveChanges();
                            /*return RedirectToAction("AdminPanel");*/
                            return RedirectToAction("LandingPage");
                        }
                        else
                        {
                            TempData["Wrong PassWord"] = "Wrong Password";
                            if (EmployeeCredetial.Attempts < EmployeeCredetial.TotalAttempts)
                            {
                                EmployeeCredetial.Attempts = EmployeeCredetial.Attempts + 1;
                                _context.Employees.Update(EmployeeCredetial);
                                _context.SaveChanges();
                                TempData["LockTrail"] = "Only "+ (EmployeeCredetial.TotalAttempts - EmployeeCredetial.Attempts) +" Attempt Left";
                            }
                            else
                            {

                                EmployeeCredetial.IsLocked = true;
                                _context.Employees.Update(EmployeeCredetial);
                                _context.SaveChanges();
                                TempData["Locked"] = "Your Accout is Locked Contact Admin";
                            }
                        }
                    }
                    else
                    {
                        TempData["Not Exist"] = "Account Not Exist";
                        return View();
                    }
                }

            }
            else
            {
                if (ModelState.IsValid)
                {
                    

                    var EmployeeRegister = new Employee()
                    {
                        Email = _employee.Email,
                        Password = _employee.Password,
                        Firstname = _employee.Firstname,
                        Lastname = _employee.Lastname,
                        Dob = _employee.Dob,
                        TotalAttempts = 0,
                        Gender = Gender,
                    };
                    _context.Add(EmployeeRegister);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }

                return View(_employee);
            }
            return View(_employee);

        }
        public IActionResult LandingPage()
        {
            var date = _context.PasswordExpiries.FirstOrDefault(m => m.EmpId == 1);
            if ((DateTime.Now - date.PasswordUpdated.Value).TotalDays > date.PasswordexpiryDay)
            {
                date.PasswordexpiryStatus = true;
                _context.PasswordExpiries.Update(date);
                _context.SaveChanges();
                TempData["Please Reset Password First"] = "Please Reset Password First";
                return RedirectToAction("ResetPassword");
            }
            else
            {
                date.PasswordexpiryStatus = false;
                _context.PasswordExpiries.Update(date);
                _context.SaveChanges();
            }
            ViewBag.EmployeeName = HttpContext.Session.GetString("Name");
            ViewBag.EmpID = HttpContext.Session.GetInt32("EmpID");
            return View(date);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("EmployeeLogin");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(PasswordViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var oldPassword = _context.Employees.FirstOrDefault(password => password.EmpId == HttpContext.Session.GetInt32("EmpID"));
                if(oldPassword.Password == employee.Password)
                {
                    if(employee.Password != employee.ConfirmPassword)
                    {
                        if (employee.ConfirmPassword == employee.ConfirmNewPassword)
                        {
                            oldPassword.Password = employee.ConfirmNewPassword;
                            _context.Employees.Update(oldPassword);
                            _context.SaveChanges();
                            return RedirectToAction("EmployeeLogin");
                        }
                        else
                        {
                            TempData["ConfirmPassword Not Matched"] = "Confirm Password And Confirm New Password Not Matched";
                        }
                    }
                    else
                    {
                        TempData["OldPassword And Confirm Password"] = "Old Password And Confirm Password Are same";

                    }
                    
                }
                else
                {
                    TempData["OldPasswordNotMatched"] = "Old Password is Wrong";

                }
                return View();
            }
            return View();
        }
    }
}
