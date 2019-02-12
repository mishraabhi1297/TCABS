using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCABS.Data.Repository;



namespace TCABS.Controllers
{
    [Authorize(Policy = "VIEW_REPORTS")]
    public class ReportController : Controller
    {
        
        public IActionResult Report_Home()
        {
            return View();
        }


        private readonly ReportRepository _reportRepo;

        public ReportController(IConnectionProvider connection, IUnitOfWork uow)
        {
            _reportRepo = new ReportRepository(connection, uow);
        }

        public async Task<IActionResult> Report_All_Projects(string type)
        {
            var reports = await _reportRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Project>();
            }
            return View(reports);
        }

        public async Task<IActionResult> Report_Supervisor(string type)
        {
            var reports = await _reportRepo.GetSupervisorAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Supervisor>();
            }
            return View(reports);
        }

        public async Task<IActionResult> Report_Registered_Convenors(string type)
        {
            var reports = await _reportRepo.GetConvenorAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Convenor>();
            }
            return View(reports);
        }

        public async Task<IActionResult> Report_Enrolled_Students(string type)
        {
            var reports = await _reportRepo.GetStudentAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Student>();
            }
            return View(reports); 
        }

        public async Task<IActionResult> Report_Registered_Teams(string type)
        {
            var reports = await _reportRepo.GetRegisteredTeamsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Registered_Team>();
            }
            return View(reports); 
        }

        public async Task<IActionResult> Report_Summary_Student_Teams(string type)
        {
            var reports = await _reportRepo.GetStudentSummaryAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Student_Summary>();
            }
            return View(reports); 
        }

        public async Task<IActionResult> Report_Unit_Overview(string type)
        {
            var reports = await _reportRepo.GetUnitOverviewAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Unit_Overview>();
            }
            return View(reports); 
        }

        public async Task<IActionResult> Report_Project_Budget(string type)
        {
            var reports = await _reportRepo.GetProjectBudgetAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Project_Budget>();
            }
            return View(reports); 
        }

        public async Task<IActionResult> Report_Team_Weekly(string type)
        {
            var reports = await _reportRepo.GetTeamWeeklyAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Team_Weekly>();
            }
            return View(reports); 
        }
        public async Task<IActionResult> Report_Budget_Weekly(string type)
        {
            var reports = await _reportRepo.GetBudgetWeeklyAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            if (reports == null)
            {
                reports = new List<Data.Models.Report_Budget_Weekly>();
            }
            return View(reports); 
        }
        
    }


    
}
