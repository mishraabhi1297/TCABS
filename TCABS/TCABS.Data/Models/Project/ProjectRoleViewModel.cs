using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.Project
{
    public class ProjectRoleViewModel
    {
        public ProjectRoleViewModel()
        {
            Roles = new List<ProjectRole>();
            SelectedRoles = new List<int>();
        }

        [Display(Name = "Role ID")]
        public int? RoleGroupID { get; set; }

        public IEnumerable<ProjectRole> Roles { get; set; }
        public IEnumerable<int> SelectedRoles { get; set; }
    }
}
