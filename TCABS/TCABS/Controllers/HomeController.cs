using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Identity.Dapper.Connections;
using Identity.Dapper.Entities;
using Identity.Dapper.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCABS.Data.Models;
using TCABS.Data.Models.Entities;
using TCABS.Data.Models.Home;
using TCABS.Data.Repository;
using TCABS.Util;

namespace TCABS.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeRepository _homeRepo;
        private readonly UserManager<DapperIdentityUser> _userManager;


        public HomeController(UserManager<DapperIdentityUser> userManager, IConnectionProvider connection, IUnitOfWork uow)
        {
            _homeRepo = new HomeRepository(connection, uow);
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("SELECTED_UNIT") == null)
            {
                return RedirectToAction(nameof(SelectUnit));
            }

            return View();
        }

        [ViewLayout("_UnitSelect")]
        [Authorize]
        public async Task<IActionResult> SelectUnit()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var units = await _homeRepo.GetUnitsForUserAsync(user.Id);

            if (units == null)
            {
                if (HttpContext.User.IsInRole("ADMIN") || HttpContext.User.IsInRole("Admin"))
                {
                    return RedirectToAction(nameof(Index), "Admin");
                }
                else
                {
                    return RedirectToAction(nameof(NoUnit));
                }
            }

            var model = new UnitSelectViewModel { Units = units };

            return View(model);
        }

        [Authorize]
        public IActionResult NoUnit()
        {
            return View();
        }

        [Authorize]
        public IActionResult UnitSelected(int id)
        {
            HttpContext.Session.SetInt32("SELECTED_UNIT", id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IEnumerable<UnitAssociation>> GetUnits(int userID)
        {
            return await _homeRepo.GetUnitsForUserAsync(userID);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<DapperIdentityUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
