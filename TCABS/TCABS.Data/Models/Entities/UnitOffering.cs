using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TCABS.Data.Models.Entities
{
    public class UnitOffering
    {
        [Key]
        [Display(Name = "ID")]
        public int UnitOffering_ID { get; set; }

        [Display(Name = "Unit ID")]
        public int Unit_ID { get; set; }

        [Display(Name = "Study Period")]
        public int StudyPeriod_ID { get; set; }

        [Display(Name = "Study Period Name")]
        public string StudyPeriod_Name { get; set; }

        [Display(Name = "Employee ID")]
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "A start date for a unit offering is required")]
        public DateTime UnitOffering_StartDate { get; set; }
        
        //get unit name and code (displayed)
        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "Unit Code is required (example: INF30011")]
        public string Unit_Code { get; set; }

        [Display(Name = "Unit Name")]
        [Required(ErrorMessage = "Unit Name is required")]
        public string Unit_Name { get; set; }

        //Get Convenor Name
        [Display(Name = "Convenor Name")]
        [Required(ErrorMessage = "Unit Name is required")]
        public string Convenor_Name { get; set; }
        public int Convenor_Id { get; set; }

        //Get lists for population
        public List<Unit> Unit_List { get; set; }
        public List<StudyPeriod> SP_List { get; set; }
        public List<UnitOffering> employeeList { get; set; }
        public bool CanBeRemoved { get; set; }
    }
}
