using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TCABS.Data.Models.Entities
{
    public class Enrolment
    {
        [Display(Name = "Id")]
        public int Enrolment_ID { get; set; }

        [Display(Name = "Student Code")]
        [Required(ErrorMessage = "The Student ID is required.")]
        public string Student_Code { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "The Student name is required.")]
        public string Student_Name { get; set; }

        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "The Unit code is required.")]
        public string Unit_Code { get; set; }

        [Display(Name = "Unit Name")]
        [Required(ErrorMessage = "The Unit name is required.")]
        public string Unit_Name { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "The Start date is required.")]
        public string UnitOffering_StartDate { get; set; }

        public int Student_ID { get; set; }
        public int UnitOffering_ID { get; set; }
        public bool CanBeRemoved { get; set; }

        public List<Enrolment> Student_List { get; set; }
        public List<Enrolment> UnitOffering_List { get; set; }
    }
}
