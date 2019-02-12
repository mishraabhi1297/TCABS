using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCABS.Data.Models.Entities
{
    public class Project
    {
        [Display(Name = "ID")]
        public int Project_ID { get; set; }

        [Required(ErrorMessage = "The Project Role Group ID is required.")]
        public int ProjectRoleGroup_ID { get; set; }

        [Display(Name = "Project Group")]
        public string ProjectRoleGroup_TYPE { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "The Project name is required.")]
        public string Project_NAME { get; set; }

        [Display(Name = "Project Budget")]
        [Required(ErrorMessage = "The Project budget is required.")]
        public int Project_BUDGET { get; set; }

        [NotMapped]
        public List<ProjectRoleGroup> ProjectRoleGroupList { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
