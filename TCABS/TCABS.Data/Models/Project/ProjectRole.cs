using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Models.Project
{
    public class ProjectRole
    {
        [Display(Name = "Role ID")]
        public int ProjectRoleID { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "The Project Role name is required.")]
        public string ProjectRoleName { get; set; }

        [Display(Name = "Hourly Rate")]
        [Required]
        public int ProjectRoleRate { get; set; }

        [Display(Name = "Role Group ID")]
        public int? ProjectRoleGroupID { get; set; }

        [Display(Name = "Role Group Name")]
        public string ProjectRoleGroupName { get; set; }

        public IEnumerable<ProjectRoleGroup> Groups { get; set; }

        public bool CanBeRemoved { get; set; }
        public bool CanBeDeallocated { get; set; }
    }
}
