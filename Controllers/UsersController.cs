#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Bases;

namespace MVC.Controllers
{

    [Authorize]
    public class UsersController : MvcControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.GetList();
            return View(userList);
        }

        public IActionResult Details(int id)
        {
            UserModel user = _userService.GetItem(id); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Add(user);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = user.Id });
				}
                ModelState.AddModelError("", result.Message);
            }
			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");

			return View(user);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            UserModel user = _userService.GetItem(id); 
            if (user == null)
            {
                return NotFound();
            }

            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(UserModel user)
        {
			if (ModelState.IsValid)
			{
				Result result = _userService.Update(user);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = user.Id });
				}
				ModelState.AddModelError("", result.Message);
			}
			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");

			return View(user);
		}

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            UserModel user = _userService.GetItem(id);
            if (user == null)
            {
                return NotFound();
			}
			return View(user);
		}

		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
