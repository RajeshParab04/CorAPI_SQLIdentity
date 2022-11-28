using sampleapi.DBContext;
using sampleapi.Enum;
using sampleapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
namespace sampleapi.Data.DAL
{
    public static class EmployeeDAL
    {
        
        //public static  Task<ResponseModel> GetEmployees()
        //{
        //   // using (var entity = new vpireportingContext()
        //   // {
        //        var employees = entity.Employees.ToList();
        //        return  Task.FromResult(new ResponseModel(ResponseCode.OK, "success", employees));
        //    //}
        //}
        //public static Task<ResponseModel> AddEmployees(Employees emp)
        //{
        //    try
        //    {
        //        //using (var entity = new vpireportingContext())
        //        //{
        //            entity.Employees.Add(emp);
        //            entity.SaveChanges();
        //            return Task.FromResult(new ResponseModel(ResponseCode.OK, "success", null));
        //      //  }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Task.FromResult(new ResponseModel(ResponseCode.ERROR, "error", ex.Message));
        //    }
        //}
        //public static Task<ResponseModel> UpdateEmployees(Employees emp)
        //{
        //    try
        //    {
        //        //using (var entity = new vpireportingContext())
        //        //{
        //            var employee = entity.Employees.FirstOrDefault(a => a.EmployeeId == emp.EmployeeId);
        //            if (employee == null)
        //            {
        //                return Task.FromResult(new ResponseModel(ResponseCode.OK, "not found", "Employee not found"));
        //            }
        //            entity.Employees.Update(emp);
        //            entity.SaveChanges();
        //            return Task.FromResult(new ResponseModel(ResponseCode.OK, "success", "Employee Updated"));
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(new ResponseModel(ResponseCode.ERROR, "error", ex.Message));
        //    }
        //}
        //public static Task<ResponseModel> RemoveEmployee(int empid)
        //{
        //    try
        //    {
        //        //using (var entity = new vpireportingContext())
        //        //{
        //            var emp = entity.Employees.FirstOrDefault(a => a.EmployeeId == empid);
        //            if (emp == null)
        //            {
        //                return Task.FromResult(new ResponseModel(ResponseCode.OK, "not found", "Employee not found"));
        //            }
        //            entity.Employees.Remove(emp);
        //            entity.SaveChanges();
        //            return Task.FromResult(new ResponseModel(ResponseCode.OK, "success", "Employee Deleted"));
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromResult(new ResponseModel(ResponseCode.ERROR, "error", ex.Message));
        //    }
        //}
    }
}
