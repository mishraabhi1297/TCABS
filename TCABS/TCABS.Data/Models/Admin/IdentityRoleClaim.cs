using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Entities
{
    public class IdentityRoleClaim
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
