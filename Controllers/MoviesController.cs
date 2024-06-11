#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Bases;

namespace MVC.Controllers
{
    [Authorize]
    public class MoviesController : MvcControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IUserService _userService;

        public MoviesController(IMovieService movieService, IDirectorService directorService, IUserService userService)
        {
            _movieService = movieService;
            _directorService = directorService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.GetList();
            return View("List", movieList);
        }

        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.GetItem(id);
            if (movie == null)
            {
                return View("Error", $"Movie with ID {id} not found!");
            }
            return View(movie);
        }

        [Authorize(Roles = "user")]
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                var result = _movieService.Add(movie);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        [Authorize(Roles = "user")]
        public IActionResult Edit(int id)
        {
            MovieModel movie = _movieService.GetItem(id);
            if (movie == null)
            {
                return View("Error", $"Movie with ID {id} not found!");
            }
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public IActionResult Edit(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                var result = _movieService.Update(movie);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        [Authorize(Roles = "user")]
        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
