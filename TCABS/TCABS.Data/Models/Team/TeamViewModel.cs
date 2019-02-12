using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Team
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {
            Teams = new List<Team>();
        }

        public string SelectedUnitName { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }
}
