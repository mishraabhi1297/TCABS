using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Admin
{
    public class FormOfferingViewModel
    {
        public int FormOffering_ID { get; set; }
        public int UnitOffering_ID { get; set; }
        public int Form_ID { get; set; }
        public DateTime DueDate { get; set; }
        public string Form_Name { get; set; }
    }
}
