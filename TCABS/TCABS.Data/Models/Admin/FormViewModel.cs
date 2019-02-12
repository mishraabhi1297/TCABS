using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TCABS.Data.Models.Admin
{
    public class FormViewModel
    {
        public int Form_ID { get; set; }
        public string Form_Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
    }
}
