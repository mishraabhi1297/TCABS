using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Dapper.Connections;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TCABS.Data.Models.Entities;
using TCABS.Data.Models.Project;
using TCABS.Data.Repository;

namespace TCABS.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _projectRepo;

        public ProjectController(IConnectionProvider connection, IUnitOfWork uow)
        {
            _projectRepo = new ProjectRepository(connection, uow);
        }

        /**
         *  GET - PROJECT
         */

        // GET: CreateProject
        [Authorize(Policy = "CREATE_PROJECT")]
        public async Task<IActionResult> CreateProject()
        {
            var model = new Project();
            var roleGroups = await _projectRepo.GetProjectRoleGroupsAsync();
            model.ProjectRoleGroupList = roleGroups.ToList();
            return View(model);
        }

        // GET: ViewProjects
        [Authorize(Policy = "VIEW_PROJECT_LIST")]
        public async Task<IActionResult> ViewProjects() => View(await _projectRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT")));

        // GET: EditProject
        [Authorize(Policy = "EDIT_PROJECT")]
        public async Task<IActionResult> EditProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _projectRepo.GetProjectsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT"));

            var model = list.FirstOrDefault(x => x.Project_ID == id);

            if (model == null)
            {
                return NotFound();
            }

            var projectRoleGroups = await _projectRepo.GetProjectRoleGroupsAsync();
            model.ProjectRoleGroupList = projectRoleGroups.ToList();

            return View(model);
        }

        // GET: DeleteProject
        [Authorize(Policy = "DELETE_PROJECT")]
        public async Task<IActionResult> DeleteProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _projectRepo.DeleteProjectAsync(id, HttpContext.Session.GetInt32("SELECTED_UNIT"));

            if (result > 0)
            {
                return RedirectToAction(nameof(ViewProjects));
            }

            return NotFound("Server was unable to delete the Project.");
        }

        /**
         *  GET - PROJECTROLEGROUP
         */

        // GET: CreateProjectRoleGroup
        [Authorize(Policy = "CREATE_PROJECT_ROLE_GROUP")]
        public IActionResult CreateProjectRoleGroup() => View();

        // GET: ViewProjectRoleGroups
        [Authorize(Policy = "VIEW_PROJECT_ROLE_GROUP_LIST")]
        public async Task<IActionResult> ViewProjectRoleGroups() => View(await _projectRepo.GetProjectRoleGroupsAsync());

        // GET: EditProjectRoleGroup
        [Authorize(Policy = "EDIT_PROJECT_ROLE_GROUP")]
        public async Task<IActionResult> EditProjectRoleGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _projectRepo.GetProjectRoleGroupsAsync();

            var model = list.FirstOrDefault(x => x.ProjectRoleGroup_ID == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: DeleteProjectRoleGroup
        [Authorize(Policy = "DELETE_PROJECT_ROLE_GROUP")]
        public async Task<IActionResult> DeleteProjectRoleGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _projectRepo.DeleteProjectRoleGroupAsync(id);

            if (result > 0)
            {
                return RedirectToAction(nameof(ViewProjectRoleGroups));
            }

            return NotFound("Server was unable to delete the Project Role Group.");
        }

        /**
         *  GET - PROJECT ROLE
         */
        // GET: CreateProjectRole
        [Authorize(Policy = "CREATE_PROJECT_ROLE")]
        public async Task<IActionResult> CreateProjectRole(int? roleGroupID)
        {
            var model = new ProjectRole
            {
                Groups = await _projectRepo.GetProjectRoleGroupsAsync()
            };

            if (roleGroupID != null)
            {
                model.ProjectRoleGroupID = roleGroupID;
            }

            return View(model);
        }

        // GET: ViewProjectRoles
        [Authorize(Policy = "VIEW_PROJECT_ROLE_LIST")]
        public async Task<IActionResult> ViewProjectRoles(int? id)
        {
            var model = new ProjectRoleViewModel
            {
                Roles = await _projectRepo.GetProjectRoleListAsync()
            };

            if (id != null)
            {
                model.SelectedRoles = await _projectRepo.GetSelectedProjectRolesAsync(id);
                model.RoleGroupID = id;
            }

            return View(model);
        }

        // GET: EditProjectRole
        [Authorize(Policy = "EDIT_PROJECT_ROLE")]
        public async Task<IActionResult> EditProjectRole(int? id, int? roleGroupID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleList = await _projectRepo.GetProjectRoleListAsync();

            var groupList = await _projectRepo.GetProjectRoleGroupsAsync();

            var model = roleList.FirstOrDefault(x => x.ProjectRoleID == id);

            if (model == null)
            {
                return NotFound();
            }

            if (roleGroupID != null)
            {
                model.ProjectRoleGroupID = roleGroupID;
            }

            model.Groups = groupList;

            return View(model);
        }

        // GET: DeleteProjectRole
        [Authorize(Policy = "DELETE_PROJECT_ROLE")]
        public async Task<IActionResult> DeleteProjectRole(int? id, int roleGroupID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _projectRepo.DeleteProjectRoleAsync(id);

            if (result > 0)
            {
                return RedirectToAction(nameof(ViewProjectRoles), new { id = roleGroupID });
            }

            return NotFound("Server was unable to delete the Project Role.");
        }

        /**
         *  POST - PROJECT
         */

        // POST: CreateProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_PROJECT")]
        public async Task<IActionResult> CreateProject(Project model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepo.CreateProjectAsync(model, HttpContext.Session.GetInt32("SELECTED_UNIT"));
                    return RedirectToAction(nameof(ViewProjects));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(model);
        }

        // POST: EditProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_PROJECT")]
        public async Task<IActionResult> EditProject(Project model)
        {
            if (ModelState.IsValid)
            {
                await _projectRepo.EditProjectAsync(model, HttpContext.Session.GetInt32("SELECTED_UNIT"));
                return RedirectToAction(nameof(ViewProjects));
            }

            // Need to repopulate the list.
            var groupList = await _projectRepo.GetProjectRoleGroupsAsync();
            model.ProjectRoleGroupList = groupList.ToList();

            return View(model);
        }

        /**
         *  POST - PROJECT ROLE GROUP
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_PROJECT_ROLE_GROUP")]
        public async Task<IActionResult> CreateProjectRoleGroup(ProjectRoleGroup model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepo.CreateProjectRoleGroupAsync(model);
                    return RedirectToAction(nameof(ViewProjectRoleGroups));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_PROJECT_ROLE_GROUP")]
        public async Task<IActionResult> EditProjectRoleGroup(ProjectRoleGroup model)
        {
            if (ModelState.IsValid)
            {
                await _projectRepo.EditProjectRoleGroupAsync(model);
                return RedirectToAction(nameof(ViewProjectRoleGroups));
            }

            return View(model);
        }

        /**
         *  POST - PROJECT ROLE
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_PROJECT_ROLE_GROUP")]
        public async Task<IActionResult> CreateProjectRole(ProjectRole model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepo.CreateProjectRoleAsync(model);
                    return RedirectToAction(nameof(ViewProjectRoles), new { id = model.ProjectRoleGroupID });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_PROJECT_ROLE")]
        public async Task<IActionResult> EditProjectRole(ProjectRole model)
        {
            if (ModelState.IsValid)
            {
                await _projectRepo.EditProjectRoleAsync(model);
                return RedirectToAction(nameof(ViewProjectRoles), new { id = model.ProjectRoleGroupID });
            }

            return View(model);
        }

        // POST: UpdateRoleInGroup
        [HttpPost]
        public async Task<IActionResult> UpdateRoleInGroup()
        {
            try
            {
                var id = HttpContext.Request.Form["id"];
                var roleGroup = HttpContext.Request.Form["roleGroup"];

                var id_int = Convert.ToInt32(id);
                var roleGroupID_int = Convert.ToInt32(roleGroup);

                var result = await _projectRepo.UpdateRoleInGroup(id_int, roleGroupID_int);

                if (result > 0)
                {
                    return Content("success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Content("failed");
        }
    }
}
