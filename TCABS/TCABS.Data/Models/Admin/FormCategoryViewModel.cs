using System;
using System.Collections.Generic;
using System.Text;

namespace TCABS.Data.Models.Admin
{
    public class FormCategoryViewModel
    {
        public int Category_ID { get; set; }
        public string Category_Type { get; set; }

        public List<FormCategoryViewModel> FormCategoryViewModelList { get; set; }


        public string Question_Text { get; set; }
    }
}
