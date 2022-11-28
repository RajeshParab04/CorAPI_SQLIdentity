using sampleapi.Data.DAL;
using sampleapi.DBContext;
using sampleapi.Enum;
using sampleapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapi.BusinessLayer
{
    public static class EmployeeBL
    {
        //public static  Task<ResponseModel> GetEmployeesBL()
        //{
        //    try
        //    {
        //        return  EmployeeDAL.GetEmployees();
        //    }
        //    catch(Exception ex)
        //    {
        //        return  Task.FromResult(new ResponseModel(ResponseCode.OK, "success",ex.Message));
        //    }

        //}
        //public static Task<ResponseModel> AddEmployeeBL(Employees emp)
        //{
        //   return EmployeeDAL.AddEmployees(emp);
        //}
        //public static Task<ResponseModel> UpdateEmployeeBL(Employees emp)
        //{
        //    return EmployeeDAL.UpdateEmployees(emp);
        //}
        //public static Task<ResponseModel> RemoveEmployeeBL(int empid)
        //{
        //    return EmployeeDAL.RemoveEmployee(empid);
        //}
    }
}
