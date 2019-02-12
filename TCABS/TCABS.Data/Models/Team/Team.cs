using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCABS.Data.Models.Project;

namespace TCABS.Data.Models.Team
{
    public class Team
    {
        public Team()
        {
            Projects = new List<ProjectAlloc>();
            EmployeeList = new List<Team>();
        }

        public int TeamID { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public int ProjectAllocID { get; set; }

        public string ProjectName { get; set; }

        [Required]
        public int SupervisorUserID { get; set; }

        public string SupervisorName { get; set; }

        public bool CanBeRemoved { get; set; }

        public IEnumerable<ProjectAlloc> Projects { get; set; }
        public IEnumerable<Team> EmployeeList { get; set; }
    }
}
