using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.Entities
{
    public class ProjectRoleGroup
    {
        [Display(Name = "ID")]
        public int ProjectRoleGroup_ID { get; set; }

        [Display(Name = "RoleGroup Name")]
        [Required(ErrorMessage = "The Project Role Group type is required.")]
        public string ProjectRoleGroup_TYPE { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
