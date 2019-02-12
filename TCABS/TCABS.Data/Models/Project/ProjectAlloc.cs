using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Project
{
    public class ProjectAlloc
    {
        public int ProjectAllocID { get; set; }

        public int UnitOfferingID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }
    }
}
