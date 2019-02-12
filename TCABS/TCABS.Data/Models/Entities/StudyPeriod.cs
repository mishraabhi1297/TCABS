using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.Entities
{
    public class StudyPeriod
    {
        [Display(Name = "Id")]
        public int StudyPeriod_ID { get; set; }

        [Display(Name = "Study Period")]
        [Required(ErrorMessage = "The Study Period name is required.")]
        public string StudyPeriod_Name { get; set; }

        public bool CanBeRemoved { get; set; }
    }
}
