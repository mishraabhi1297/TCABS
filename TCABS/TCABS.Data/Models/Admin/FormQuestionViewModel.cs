using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Admin
{
    public class FormQuestionViewModel
    {
        public int Category_ID { get; set; }
        public string Form_Name { get; set; }
        public string Category_Type { get; set; }
        public string Question_Text { get; set; }
    }
}
