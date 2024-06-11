#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Entities;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Bases;

namespace MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class DirectorsController : MvcControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }
        public IActionResult Index()
        {
            List<DirectorModel> directorList = _directorService.Query().OrderBy(p => p.Name).ToList();
            return View(directorList);
        }

        public IActionResult Details(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p => p.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        public IActionResult Create()
        {
            return View();
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                Result result = _directorService.Add(director);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }

        public IActionResult Edit(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p => p.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                Result result = _directorService.Update(director);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }

        public IActionResult Delete(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p => p.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _directorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
