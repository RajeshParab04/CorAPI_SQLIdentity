﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapi.Model.BindingModel
{
    public class EmployeeBindingModel
    {

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; }
        
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
    }
}
