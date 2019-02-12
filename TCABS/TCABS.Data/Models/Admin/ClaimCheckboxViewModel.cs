using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.AdminViewModels
{
    public class ClaimCheckboxViewModel
    {
        public int ID { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string DisplayName { get; set; }
    }
}
