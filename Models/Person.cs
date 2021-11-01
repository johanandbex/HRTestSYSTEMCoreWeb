using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HRSystemCore.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Departmewnt is required")]
        public int? DepartmentId { get; set; }
        public int? CompanyId { get; set; }
        [Required(ErrorMessage = "Employee Number is required")]
        public string EmployeeNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual Status Status { get; set; }
    }
  

}
