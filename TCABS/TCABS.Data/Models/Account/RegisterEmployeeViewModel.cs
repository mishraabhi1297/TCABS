using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Identity.Dapper.Entities;

namespace TCABS.Data.Models.AccountViewModels
{
    public class RegisterEmployeeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Role")]
        public string Role { get; set; }

        public IEnumerable<DapperIdentityRole> Roles { get; set; }
    }
}
