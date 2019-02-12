using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Identity.Dapper.Connections;
using Identity.Dapper.Entities;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCABS.Data.Models.Admin;
using TCABS.Data.Models.AdminViewModels;
using TCABS.Data.Models.Entities;
using TCABS.Data.Repository;

namespace TCABS.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleRepository _roleRepo;
        private readonly EmployeeRepository _employeeRepo;
        private readonly StudentRepository _studentRepo;
        private readonly UnitRepository _unitRepo;
        private readonly UnitOfferingRepository _unitOfferingRepo;
        private readonly StudyPeriodRepository _studyPeriodRepo;
        private readonly EnrolmentRepository _enrolmentRepo;
        private readonly FormRepository _formRepo;
        private readonly FormOfferingRepository _formofferingRepo;

        public AdminController(UserManager<DapperIdentityUser> userManager, RoleManager<DapperIdentityRole> roleManager,
            IConnectionProvider connection, IUnitOfWork uow)
        {
            _roleRepo = new RoleRepository(roleManager, connection);
            _employeeRepo = new EmployeeRepository(userManager, connection, uow);
            _studentRepo = new StudentRepository(userManager, connection, uow);
            _unitRepo = new UnitRepository(userManager, connection, uow);
            _unitOfferingRepo = new UnitOfferingRepository(userManager, connection, uow);
            _studyPeriodRepo = new StudyPeriodRepository(userManager, connection, uow);
            _enrolmentRepo = new EnrolmentRepository(userManager, connection, uow);
            _formRepo = new FormRepository(userManager, connection, uow);
            _formofferingRepo = new FormOfferingRepository(userManager, connection, uow);
        }

        [Authorize]
        public IActionResult Index() => View();

        /**
         * GET ROLE
         */

        // GET: CreateRole
        [Authorize(Policy = "CREATE_ACCESS_ROLE")]
        public async Task<IActionResult> CreateRole()
        {
            var model = new RoleClaimViewModel();

            var claims = await _roleRepo.GetIdentityClaimListAsync();

            var roleList = claims.GroupBy(x => x.ClaimType)
                .Select(list => list.Select(element => new ClaimCheckboxViewModel
                    {
                        ID = element.ID,
                        ClaimType = element.ClaimType,
                        ClaimValue = element.ClaimValue,
                        DisplayName = element.DisplayName
                    })
                    .ToList())
                .ToList();

            model.ClaimLists = roleList;

            return View(model);
        }

        // GET: ViewRoles
        [Authorize(Policy = "VIEW_ACCESS_ROLE_LIST")]
        public async Task<IActionResult> ViewRoles() => View(await _roleRepo.GetIdentityRolesAsync());

        // GET: EditRole
        [Authorize(Policy = "EDIT_ACCESS_ROLE")]
        public async Task<IActionResult> EditRole(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the corresponding Role
            var repoRolesList = await _roleRepo.GetIdentityRolesAsync();
            var role = repoRolesList.FirstOrDefault(x => x.ID == id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleClaimViewModel
            {
                RoleID = role.ID,
                RoleName = role.Name
            };

            var claims = await _roleRepo.GetIdentityClaimListAsync();

            // Get the existing RoleClaims for Role
            var existingClaims = await _roleRepo.GetIdentityClaimsForRoleAsync(role.ID);

            var selected = new List<int>();

            if (existingClaims != null)
            {
                selected.AddRange(existingClaims.Select(claim => claim.ID));
            }

            var roleList = claims.GroupBy(x => x.ClaimType)
                .Select(list => list.Select(element => new ClaimCheckboxViewModel
                    {
                        ID = element.ID,
                        ClaimType = element.ClaimType,
                        ClaimValue = element.ClaimValue,
                        DisplayName = element.DisplayName
                    })
                    .ToList())
                .ToList();

            model.ClaimLists = roleList;
            model.SelectedClaims = selected;

            return View(model);
        }

        // GET: DeleteRole
        [Authorize(Policy = "DELETE_ACCESS_ROLE")]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _roleRepo.DeleteRoleAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(ViewRoles));
            }

            return NotFound("Server was unable to delete the Role.");
        }

        /**
         * GET EMPLOYEE
         */

        // GET: CreateEmployee
        [Authorize(Policy = "CREATE_EMPLOYEE")]
        public async Task<IActionResult> CreateEmployee()
        {
            var model = new UserViewModel();

            var result = await _roleRepo.GetIdentityRolesAsync();

            model.Roles = result.ToList();

            return View(model);
        }

        // GET: ViewEmployees
        [Authorize(Policy = "VIEW_EMPLOYEE_LIST")]
        public async Task<IActionResult> ViewEmployees() => View(await _employeeRepo.GetEmployeesAsync());

        // GET: EditEmployee
        [Authorize(Policy = "EDIT_EMPLOYEE")]
        public async Task<IActionResult> EditEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the corresponding Employee
            var employeeList = await _employeeRepo.GetEmployeesAsync();
            var model = employeeList.FirstOrDefault(x => x.ID == id);

            if (model == null)
            {
                return NotFound();
            }

            var result = await _roleRepo.GetIdentityRolesAsync();

            model.Roles = result.ToList();
            model.SelectedRoles = await _employeeRepo.GetRolesForUserAsync(model.ID);
            model.DateOfBirth = DateTime.ParseExact(model.DOB, "ddMMyy", new DateTimeFormatInfo());

            return View(model);
        }

        // GET: DeleteEmployee
        [Authorize(Policy = "DELETE_EMPLOYEE")]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _employeeRepo.DeleteEmployeeAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(ViewEmployees));
            }

            return NotFound("Server was unable to delete the Employee.");
        }

        /**
         * GET STUDENT
         */

        // GET: CreateStudent
        [Authorize(Policy = "CREATE_STUDENT")]
        public async Task<IActionResult> CreateStudent()
        {
            var model = new UserViewModel();

            var result = await _roleRepo.GetIdentityRolesAsync();

            model.Roles = result.ToList();

            return View(model);
        }

        // GET: ViewStudents
        [Authorize(Policy = "VIEW_STUDENT_LIST")]
        public async Task<IActionResult> ViewStudents() => View(await _studentRepo.GetStudentsAsync());

        // GET: EditStudent
        [Authorize(Policy = "EDIT_STUDENT")]
        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the corresponding Students
            var studentList = await _studentRepo.GetStudentsAsync();
            var model = studentList.FirstOrDefault(x => x.ID == id);

            if (model == null)
            {
                return NotFound();
            }

            var result = await _roleRepo.GetIdentityRolesAsync();

            model.Roles = result.ToList();
            model.SelectedRoles = await _studentRepo.GetRolesForUserAsync(model.ID);
            model.DateOfBirth = DateTime.ParseExact(model.DOB, "ddMMyy", new DateTimeFormatInfo());

            return View(model);
        }

        // GET: DeleteStudent
        [Authorize(Policy = "DELETE_STUDENT")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _studentRepo.DeleteStudentAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(ViewStudents));
            }

            return NotFound("Server was unable to delete the Student.");
        }

        /**
         * POST ROLE
         */

        // POST: CreateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_EMPLOYEE")]
        public async Task<IActionResult> CreateRole(RoleClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleRepo.CreateIdentityRoleAsync(model);
                return RedirectToAction(nameof(ViewRoles));
            }

            // Need to repopulate the checkbox list.

            var claims = await _roleRepo.GetIdentityClaimListAsync();
            var claimsList = claims.ToList();

            var roleList = claimsList.GroupBy(x => x.ClaimType)
                .Select(list => list.Select(element => new ClaimCheckboxViewModel
                {
                    ID = element.ID,
                    ClaimType = element.ClaimType,
                    ClaimValue = element.ClaimValue,
                    DisplayName = element.DisplayName
                })
                    .ToList())
                .ToList();

            model.ClaimLists = roleList;

            return View(model);
        }

        // POST: EditRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_EMPLOYEE")]
        public async Task<IActionResult> EditRole(RoleClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleRepo.CreateRoleClaimsAsync(model);
                return RedirectToAction(nameof(ViewRoles));
            }
            // Need to repopulate the checkbox list.

            var claims = await _roleRepo.GetIdentityClaimListAsync();
            var claimsList = claims.ToList();

            var roleList = claimsList.GroupBy(x => x.ClaimType)
                .Select(list => list.Select(element => new ClaimCheckboxViewModel
                {
                    ID = element.ID,
                    ClaimType = element.ClaimType,
                    ClaimValue = element.ClaimValue,
                    DisplayName = element.DisplayName
                })
                    .ToList())
                .ToList();

            model.ClaimLists = roleList;

            return View(model);
        }

        /**
         * POST EMPLOYEE
         */

        // POST: CreateEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_EMPLOYEE")]
        public async Task<IActionResult> CreateEmployee(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepo.CreateEmployeeAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewEmployees));
                }
            }
            // Need to repopulate the Roles dropdown.
            var roles = await _roleRepo.GetIdentityRolesAsync();
            model.Roles = roles.ToList();

            return View(model);
        }

        // POST: EditEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_EMPLOYEE")]
        public async Task<IActionResult> EditEmployee(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepo.EditEmployeeAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewEmployees));
                }
            }
            // Need to repopulate the Roles dropdown.
            var roles = await _roleRepo.GetIdentityRolesAsync();
            model.Roles = roles.ToList();

            return View(model);
        }

        /**
         * POST STUDENT
         */

        // POST: CreateStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_STUDENT")]
        public async Task<IActionResult> CreateStudent(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _studentRepo.CreateStudentAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewStudents));
                }
            }
            // Need to repopulate the Roles dropdown.
            var roles = await _roleRepo.GetIdentityRolesAsync();
            model.Roles = roles.ToList();

            return View(model);
        }

        // POST: EditStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_STUDENT")]
        public async Task<IActionResult> EditStudent(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _studentRepo.EditStudentAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewStudents));
                }
            }
            // Need to repopulate the Roles dropdown.
            var roles = await _roleRepo.GetIdentityRolesAsync();
            model.Roles = roles.ToList();

            return View(model);
        }


        // GET: Unit/Create
        [Authorize("CREATE_UNIT")]
        public IActionResult CreateUnit()
        {
            var model = new Unit();
            return View(model);
        }

        // GET: ViewUnits
        [Authorize(Policy = "VIEW_UNIT_LIST")]
        public async Task<IActionResult> ViewUnits() => View(await _unitRepo.FetchUnitListAsync());

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("CREATE_UNIT")]
        public async Task<IActionResult> CreateUnit(Unit model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitRepo.CreateUnitAsync(model);
                    return RedirectToAction(nameof(ViewUnits));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(model);
        }

        //get editunit
        [Authorize("EDIT_UNIT")]
        public async Task<IActionResult> EditUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _unitRepo.FetchUnitListAsync();

            var model = list.FirstOrDefault(x => x.Unit_Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //POST EditUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("EDIT_UNIT")]
        public async Task<IActionResult> EditUnit(Unit model)
        {
            if (ModelState.IsValid)
            {
                await _unitRepo.UpdateUnitAsync(model);
                return RedirectToAction(nameof(ViewUnits));
            }
            return View(model);
        }


        //Get Delete
        [Authorize("DELETE_UNIT")]
        public async Task<IActionResult> DeleteUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _unitRepo.DeleteUnitAsync((int)id);

            if (result > 0)
            {
                return RedirectToAction(nameof(ViewUnits));
            }
            return NotFound("Server was unable to delete the Unit");
        }

        // GET: ViewUnits
        [Authorize(Policy = "VIEW_UNIT_OFFERING_LIST")]
        public async Task<IActionResult> ViewUnitOfferings() => View(await _unitOfferingRepo.FetchAllUnitOfferingsAsync());

        // GET: UnitOffering/Create
        [Authorize(Policy = "CREATE_UNIT_OFFERING")]
        public async Task<IActionResult> CreateUnitOffering()
        {
            var model = new UnitOffering();
            var unit = await _unitOfferingRepo.GetUnitListAsync();
            var sp = await _unitOfferingRepo.GetStudyPeriodListAsync();
            var emp = await _unitOfferingRepo.GetEmployeesAsync();

            //employee logic here
            model.Unit_List = unit.ToList();
            model.SP_List = sp.ToList();
            model.employeeList = emp.ToList();
            return View(model);
        }

        // POST: UnitOffering/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_UNIT_OFFERING")]
        public async Task<IActionResult> CreateUnitOffering(UnitOffering model)
        {
            try
            {
                if (await _unitOfferingRepo.CreateUnitOfferingAsync(model) >= 0)
                {
                    return RedirectToAction(nameof(ViewUnitOfferings));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(model);
        }

        // GET: UnitOffering/Edit/5
        [Authorize(Policy = "EDIT_UNIT_OFFERING")]
        public async Task<IActionResult> EditUnitOffering(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _unitOfferingRepo.FetchAllUnitOfferingsAsync();
            var model = list.FirstOrDefault(x => x.UnitOffering_ID == id);

            if (model == null)
            {
                return NotFound();
            }

            var studyPeriods = await _unitOfferingRepo.GetStudyPeriodListAsync();
            var units = await _unitOfferingRepo.GetUnitListAsync();
            var emp = await _unitOfferingRepo.GetEmployeesAsync();

            model.SP_List = studyPeriods.ToList();
            model.employeeList = emp.ToList();
            model.Unit_List = units.ToList();

            return View(model);
        }

        // POST: UnitOffering/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_UNIT_OFFERING")]
        public async Task<IActionResult> EditUnitOffering(UnitOffering model)
        {
            var studyPeriods = await _unitOfferingRepo.GetStudyPeriodListAsync();
            var units = await _unitOfferingRepo.GetUnitListAsync();
            var emp = await _unitOfferingRepo.GetEmployeesAsync();

            model.SP_List = studyPeriods.ToList();
            model.employeeList = emp.ToList();
            model.Unit_List = units.ToList();

            if (await _unitOfferingRepo.EditUnitOffering(model) >= 0)
            {
                return RedirectToAction(nameof(ViewUnitOfferings));
            }

            return View(model);
        }

        // POST: UnitOffering/Delete/5
        [Authorize(Policy = "DELETE_UNIT_OFFERING")]
        public async Task<IActionResult> DeleteUnitOffering(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _unitOfferingRepo.DeleteUnitOffering((int)id);

            if (result >= 0)
            {
                return RedirectToAction(nameof(ViewUnitOfferings));
            }
            return NotFound("Server was unable to delete the Unit Offering");
        }

        //get study periods
        [Authorize(Policy = "VIEW_STUDY_PERIOD_LIST")]
        public async Task<IActionResult> ViewStudyPeriods() => View(await _studyPeriodRepo.GetStudyPeriodsAsync());

        // GET: StudyPeriod/Edit/5
        [Authorize(Policy = "EDIT_STUDY_PERIOD")]
        public async Task<IActionResult> EditStudyPeriod(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spList = await _studyPeriodRepo.GetStudyPeriodsAsync();
            var model = spList.FirstOrDefault(x => x.StudyPeriod_ID == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: StudyPeriod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_STUDY_PERIOD")]
        public async Task<IActionResult> EditStudyPeriod(StudyPeriod model)
        {
            if (ModelState.IsValid)
            {
                await _studyPeriodRepo.EditStudyPeriodAsync(model);
                return RedirectToAction(nameof(ViewStudyPeriods));
            }
            return View(model);
        }

        // GET: StudyPeriod/Create
        [Authorize(Policy = "CREATE_STUDY_PERIOD")]
        public IActionResult CreateStudyPeriod()
        {
            var model = new StudyPeriod();
            return View(model);
        }

        // POST: StudyPeriod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_STUDY_PERIOD")]
        public async Task<IActionResult> CreateStudyPeriod(StudyPeriod model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studyPeriodRepo.CreateStudyPeriodAsync(model);
                    return RedirectToAction(nameof(ViewStudyPeriods));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(model);
        }

        // POST: StudyPeriod/Delete/5
        [Authorize(Policy = "DELETE_STUDY_PERIOD")]
        public async Task<IActionResult> DeleteStudyPeriod(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _studyPeriodRepo.DeleteStudyPeriodAsync(id);

            if (result >= 0)
            {
                return RedirectToAction(nameof(ViewStudyPeriods));
            }
            return NotFound("Server was unable to delete the Study Periods");
        }

        //get enrolments
        [Authorize(Policy = "VIEW_ENROLMENT_LIST")]
        public async Task<IActionResult> ViewEnrolments() => View(await _enrolmentRepo.FetchAllEnrolmentsAsync());

        // GET: Enrolment/Create
        [Authorize(Policy = "CREATE_ENROLMENT")]
        public async Task<IActionResult> CreateEnrolment()
        {
            var model = new Enrolment();
            var unitOfferings = await _enrolmentRepo.GetUnitOfferingListAsync();
            var students = await _enrolmentRepo.GetStudentListAsync();

            //employee logic here
            model.Student_List = students.ToList();
            model.UnitOffering_List = unitOfferings.ToList();
            return View(model);
        }

        // POST: Enrolment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CREATE_ENROLMENT")]
        public async Task<IActionResult> CreateEnrolment(Enrolment model)
        {
            try
            {
                if (await _enrolmentRepo.CreateEnrolmentAsync(model) >= 0)
                {
                    return RedirectToAction(nameof(ViewEnrolments));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(model);
        }

        // GET: Enrolment/Edit/5
        [Authorize(Policy = "EDIT_ENROLMENT")]
        public async Task<IActionResult> EditEnrolment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _enrolmentRepo.FetchAllEnrolmentsAsync();
            var model = list.FirstOrDefault(x => x.Enrolment_ID == id);

            if (model == null)
            {
                return NotFound();
            }

            var unitOfferings = await _enrolmentRepo.GetUnitOfferingListAsync();
            var students = await _enrolmentRepo.GetStudentListAsync();

            model.Student_List = students.ToList();
            model.UnitOffering_List = unitOfferings.ToList();

            return View(model);
        }

        // POST: Enrolment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EDIT_ENROLMENT")]
        public async Task<IActionResult> EditEnrolment(Enrolment model)
        {
            var unitOfferings = await _enrolmentRepo.GetUnitOfferingListAsync();
            var students = await _enrolmentRepo.GetStudentListAsync();

            model.Student_List = students.ToList();
            model.UnitOffering_List = unitOfferings.ToList();

            if (await _enrolmentRepo.EditEnrolmentAsync(model) >= 0)
            {
                return RedirectToAction(nameof(ViewEnrolments));
            }

            return View(model);
        }

        public async Task<IActionResult> ViewFormOffering() => View(await _formRepo.GetFormOfferingsAsync(HttpContext.Session.GetInt32("SELECTED_UNIT")));

        public async Task<IActionResult> ViewFormCategories() => View(await _formRepo.GetFormCategoriesAsync());

        public async Task<IActionResult> ViewFormQuestions(int id) => View(await _formRepo.GetFormQuestionsAsync(id));

        public async Task<IActionResult> ViewFormItems(int id) => View(await _formRepo.GetFormItemsAsync(id));



        public ActionResult CreateFormCategory()
        {
            return View();
        }

        public async Task<IActionResult> CreateFormQuestion()
        {
            //return View();
            var model = new FormCategoryViewModel();
            var catGroups = await _formRepo.GetFormCategoriesAsync();
            model.FormCategoryViewModelList = catGroups.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "CREATE_PROJECT")]
        public async Task<IActionResult> CreateFormQuestion(FormCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _formRepo.CreateFormQuestionAsync(model);
                    return View(model);
                    //return RedirectToAction(nameof(ViewFormCategories));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(model);
        }

        /*public async Task<IActionResult> CreateProject()
        {
            var model = new Project();
            var roleGroups = await _projectRepo.GetProjectRoleGroupsAsync();
            model.ProjectRoleGroupList = roleGroups.ToList();
            return View(model);
        }*/

        // POST Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "CREATE_STUDENT")]
        public async Task<IActionResult> CreateFormCategory(FormCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _formRepo.CreateFormCategoryAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewStudents));
                }
            }
            // Need to repopulate the Roles dropdown.
            //var roles = await _roleRepo.GetIdentityRolesAsync();
            //model.Roles = roles.ToList();

            return View(model);
        }

        public ActionResult CreateForm()
        {
            return View();
        }

        public ActionResult CreateFO()
        {
            return View();
        }

        // POST Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "CREATE_STUDENT")]
        public async Task<IActionResult> CreateForm(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _formRepo.CreateFormAsync(model);
                if (result > 0)
                {
                    return RedirectToAction(nameof(ViewStudents));
                }
            }
            // Need to repopulate the Roles dropdown.
            //var roles = await _roleRepo.GetIdentityRolesAsync();
            //model.Roles = roles.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "CREATE_STUDENT")]
        public async Task<IActionResult> CreateFO(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = HttpContext.Session.GetInt32(@"SELECTED_UNIT");

                await _formRepo.CreateFOAsync(model, x);

                return RedirectToAction(nameof(ViewFormOffering));
            }
            return View(model);
        }
    }
}