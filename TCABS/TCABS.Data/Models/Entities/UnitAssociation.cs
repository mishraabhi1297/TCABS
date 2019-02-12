using System;

namespace TCABS.Data.Models.Entities
{
    public class UnitAssociation
    {

        public int UnitOfferingID { get; set; }

        public string UnitName { get; set; }

        public string UnitCode { get; set; }

        public string AssociationType { get; set; }

        public DateTime UnitOfferingStartDate { get; set; }
    }
}
