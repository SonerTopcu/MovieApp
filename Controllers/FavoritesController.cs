using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private const string _SESSIONKEY = "favoritessessionkey";

        private readonly IMovieService _movieService;

        public FavoritesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var favorites = GetSession();
            return View(favorites);
        }

        public IActionResult Add(int movieId)
        {
            var favorites = GetSession();
            var movie = _movieService.GetItem(movieId);
            var favorite = new FavoriteModel()
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                Revenue = (double)movie.Revenue,
                RevenueOutput = movie.RevenueOutput,
                UserName = User.Identity.Name
            };
            if (!favorites.Any(f => f.MovieId == favorite.MovieId))
                favorites.Add(favorite);
            SetSession(favorites);
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Clear()
        {
            var favorites = GetSession();
            favorites.RemoveAll(f => f.UserName == User.Identity.Name);
            SetSession(favorites);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int movieId)
        {
            var favorites = GetSession();
            favorites.RemoveAll(f => f.MovieId == movieId);
            SetSession(favorites);
            return RedirectToAction(nameof(Index));
        }

        private List<FavoriteModel> GetSession()
        {
            var favorites = new List<FavoriteModel>();
            var json = HttpContext.Session.GetString(_SESSIONKEY);
            if (!string.IsNullOrWhiteSpace(json))
                favorites = JsonConvert.DeserializeObject<List<FavoriteModel>>(json);
            return favorites;
        }

        private void SetSession(List<FavoriteModel> favorites)
        {
            var json = JsonConvert.SerializeObject(favorites);
            HttpContext.Session.SetString(_SESSIONKEY, json);
        }
    }
}
