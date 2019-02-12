using System.Collections.Generic;

namespace TCABS.Data.Models.AdminViewModels
{
    public class RoleClaimViewModel
    {
        public RoleClaimViewModel()
        {
            SelectedClaims = new List<int>();
            ClaimLists = new List<List<ClaimCheckboxViewModel>>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public List<int> SelectedClaims { get; set; }
        public List<List<ClaimCheckboxViewModel>> ClaimLists { get; set; }
    }
}
