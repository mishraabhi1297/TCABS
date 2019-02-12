using System.Collections.Generic;
using System.Linq;
using TCABS.Data.Models.Entities;

namespace TCABS.Data.Models.Home
{
    public class UnitSelectViewModel
    {
        public int SelectedUnitOffering { get; set; }

        public IEnumerable<UnitAssociation> Units { get; set; }
    }
}
