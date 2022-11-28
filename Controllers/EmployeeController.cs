using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sampleapi.BusinessLayer;
using sampleapi.Data;
using sampleapi.DBContext;
using sampleapi.Enum;
using sampleapi.Model;
using sampleapi.Model.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllEmployees")]
        public async Task<ResponseModel> GetAllEmployees()
        {


            //  var data = _context.Employees  .ToList();
            var data = from e in _context.Employees
                            join d in _context.Departments on e.DepartmentId equals d.DepartmentId
                            select new
                            {
                                EmployeeId = e.EmployeeId,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                Email = e.Email,
                                HireDate = e.HireDate,
                                PhoneNumber = e.PhoneNumber,
                                Salary = e.Salary,
                                Department=d.DepartmentName,
                                DepartmentId = d.DepartmentId

                            };

            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Get Employee", data));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut( "UpdateEmployee/{empid}")]
        public async Task<ResponseModel> UpdateEmployee(int empid,[FromBody] EmployeeBindingModel emp)
        {
            try
            {
                var employeedata = _context.Employees.Where(a => a.EmployeeId == empid).FirstOrDefault();
                if(employeedata==null)
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Employee not found", null));

              //  var employee = new Employees { FirstName = employeedata.FirstName, LastName = employeedata.LastName, Email = employeedata.Email, PhoneNumber = employeedata.PhoneNumber, HireDate = employeedata.HireDate, Salary = emp.Salary, DepartmentId = emp.DepartmentId };
                employeedata.FirstName = emp.FirstName;
                employeedata.LastName = emp.LastName;
                employeedata.Email = emp.Email;
                employeedata.PhoneNumber = emp.PhoneNumber;
                employeedata.HireDate = emp.HireDate;
                employeedata.Salary = emp.Salary;
                employeedata.DepartmentId = emp.DepartmentId;

                 _context.Employees.Update(employeedata);
                //_context.Attach(employee);
                //_context.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Employee Updated", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Update Error", ex.Message));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("DeleteEmployee/{empid}")]
        public async Task<ResponseModel> DeleteEmployee(int empid)
        {
            try
            {
                var empdata = _context.Employees.FirstOrDefault(a => a.EmployeeId == empid);
                if (empdata != null)
                {
                    _context.Employees.Remove(empdata);
                    _context.SaveChanges();
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Delete", "Employee Deleted"));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Delete", "Employee not found"));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Delete Error", ex.Message));
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddEmployee")]
        public async Task<ResponseModel> AddEmployee([FromBody] EmployeeBindingModel emp)
        {
            try
            {
                var employee = new Employees { FirstName = emp.FirstName, LastName = emp.LastName, Email = emp.Email, PhoneNumber = emp.PhoneNumber, HireDate = emp.HireDate, Salary = emp.Salary, DepartmentId = emp.DepartmentId };
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Employee Added", null));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Add Error", ex.Message));
            }
        }

    }
}
