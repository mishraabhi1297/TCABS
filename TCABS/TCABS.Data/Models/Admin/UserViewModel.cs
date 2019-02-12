using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Models.Admin
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            SelectedRoles = new List<int>();
            Roles = new List<IdentityRole>();
        }

        public int ID { get; set; }

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

        public string DOB { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "UniqueID")]
        public string UniqueID { get; set; }

        [Required]
        public IEnumerable<int> SelectedRoles { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
