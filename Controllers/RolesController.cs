#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Bases;

namespace MVC.Controllers
{
	[Authorize(Roles = "admin")]
    public class RolesController : MvcControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            List<RoleModel> roleList = _roleService.Query().ToList();
            return View(roleList);
        }

		public IActionResult Details(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);

            if (role is null)
            {
                return NotFound();
            }
            return View("RoleDetails", role);
        }

		public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                Result result = _roleService.Add(role);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message; 
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }
		public IActionResult Edit(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                Result result = _roleService.Update(role);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }
		public IActionResult Delete(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
        {
            Result result = _roleService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
