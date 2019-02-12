using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.Entities
{
    public class Unit
    {
        [Key]
        [Display(Name = "ID")]
        public int Unit_Id { get; set; }

        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "Unit Code is required (example: INF30011")]
        public string Unit_Code { get; set; }

        [Display(Name = "Unit Name")]
        [Required(ErrorMessage = "Unit Name is required")]
        public string Unit_Name { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
