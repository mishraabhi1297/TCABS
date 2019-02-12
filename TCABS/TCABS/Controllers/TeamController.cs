using System.Threading.Tasks;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCABS.Data.Models.Team;
using TCABS.Data.Repository;

namespace TCABS.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamRepository _teamRepo;

        public TeamController(IConnectionProvider connection, IUnitOfWork uow)
        {
            _teamRepo = new TeamRepository(connection, uow);
        }

        /*
         * GET - TEAM
         */

        // GET: CreateTeam
        public async Task<IActionResult> CreateTeam()
        {
            var model = new Team
            {
                Projects = await _teamRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT")),
                EmployeeList = await _teamRepo.GetEmployeesAsync()
            };

            return View(model);
        }

        // GET: ViewTeams
        public async Task<IActionResult> ViewTeams()
        {
            var model = new TeamViewModel
            {
                SelectedUnitName = await _teamRepo.GetUnitNameAsync(HttpContext.Session.GetInt32("SELECTED_UNIT")),
                Teams = await _teamRepo.GetTeamsAsync()
            };

            return View(model);
        }

        // GET: EditTeam
        public async Task<IActionResult> EditTeam(int id)
        {
            var model = new Team
            {
                Projects = await _teamRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT")),
                EmployeeList = await _teamRepo.GetEmployeesAsync()
            };

            var teamData = await _teamRepo.GetTeamData(id, HttpContext.Session.GetInt32("SELECTED_UNIT"));
            model.TeamID = id;
            model.ProjectAllocID = teamData.ProjectAllocID;
            model.SupervisorUserID = teamData.SupervisorUserID;
            model.TeamName = teamData.TeamName;
            return View(model);
        }

        // POST: EditTeam
        [HttpPost]
        public async Task<IActionResult> EditTeam(Team model)
        {
            if (ModelState.IsValid)
            {
                await _teamRepo.EditTeamAsync(model);
                return RedirectToAction(nameof(ViewTeams));
            }

            // Need to repopulate the list.
            var projectList = await _teamRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));
            var empList = await _teamRepo.GetEmployeesAsync();
            model.Projects = projectList;
            model.EmployeeList = empList;

            return View(model);
        }
    }
}
