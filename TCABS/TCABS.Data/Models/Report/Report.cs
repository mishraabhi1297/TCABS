using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TCABS.Data.Models
{
    public class Report
    {
    }

    public class Report_Project
    {
        [Display(Name = "Project Name")]
        public string Project_NAME { get; set; }
        [Display(Name = "Project Budget")]
        public string PROJECT_BUDGET { get; set; }
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
    }

    public class Report_Supervisor
    {
        [Display(Name = "Employee Name")]
        public string Employee_Name { get; set; }
        [Display(Name = "Employee Username")]
        public string Username { get; set; }
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Role Group")]
        public string Role { get; set; }
    }

    public class Report_Convenor
    {
        [Display(Name = "Employee Name")]
        public string Employee_Name { get; set; }

        [Display(Name = "Registered Units")]
        public string Unit_Name { get; set; }

        [Display(Name = "Role Group")]
        public string Role { get; set; }
    }

    public class Report_Student
    {
        [Display(Name = "Student Id")]
        public string Student_ID { get; set; }
        [Display(Name = "Student Name")]
        public string Student_Name { get; set; }
        [Display(Name = "Enrolled Units")]
        public string Unit_Name { get; set; }
        
    }

    public class Report_Registered_Team
    {
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Student Name")]
        public string STUDENT_NAME { get; set; }
        [Display(Name = "Student Id")]
        public string STUDENT_ID { get; set; }
        [Display(Name = "Project Manager")]
        public bool TEAMALLOC_PROJECTMANAGER { get; set; }
    }

    public class Report_Student_Summary
    {
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Project Name")]
        public string PROJECT_NAME { get; set; }
        [Display(Name = "Supervisor")]
        public string SUPERVISOR { get; set; }
        [Display(Name = "Studnet Name")]
        public string STUDENT_NAME { get; set; }
    }

    public class Report_Unit_Overview
    {
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Project Name")]
        public string PROJECT_NAME { get; set; }
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Project Budget")]
        public int PROJECT_BUDGET { get; set; }
        [Display(Name = "Remaining Budget")]
        public int REMAINING_BUDGET { get; set; }
        [Display(Name = "Budget State")]
        public string BUDGET_STATE { get; set; }
    }

    public class Report_Project_Budget
    {
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Project Name")]
        public string PROJECT_NAME { get; set; }
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Project Budget")]
        public int PROJECT_BUDGET { get; set; }
        [Display(Name = "Budget Spend to Date")]
        public int BUDGET_SPENT { get; set; }
        [Display(Name = "Remaining Budget")]
        public int REMAINING_BUDGET { get; set; }
    }

    public class Report_Team_Weekly
    {
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Project Name")]
        public string PROJECT_NAME { get; set; }
        [Display(Name = "Project Start Date")]
        public DateTime PROJECT_STARTDATE { get; set; }
        [Display(Name = "Student Id")]
        public string STUDENT_ID { get; set; }
        [Display(Name = "Student Name")]
        public string STUDENT_NAME { get; set; }
        [Display(Name = "Task Name")]
        public string TIMELOG_TASKNAME { get; set; }
        [Display(Name = "Project Role")]
        public string PROJECTROLE_NAME { get; set; }
        [Display(Name = "Task Time")]
        public int TIMELOG_TOTALTIME { get; set; }
        [Display(Name = "Task Date")]
        public string TASK_DATE { get; set; }
        [Display(Name = "Week Number")]
        public int WEEK_NUMBER { get; set; }
        [Display(Name = "Cost")]
        public int COST { get; set; }
    }

    public class Report_Budget_Weekly
    {
        [Display(Name = "Unit Name")]
        public string UNIT_NAME { get; set; }
        [Display(Name = "Project Name")]
        public string PROJECT_NAME { get; set; }
        [Display(Name = "Team Name")]
        public string TEAM_NAME { get; set; }
        [Display(Name = "Project Budget")]
        public int PROJECT_BUDGET { get; set; }
        [Display(Name = "Budget Spend to Date")]
        public int BUDGET_SPENT { get; set; }
        [Display(Name = "Remaining Budget")]
        public int REMAINING_BUDGET { get; set; }
    }

}
