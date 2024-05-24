using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Business.Services
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        Result Add(MovieModel model);
        Result Update(MovieModel model);
        Result Delete(int id);
        List<MovieModel> GetList() => Query().ToList();
        MovieModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
    }

    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(Db db) : base(db)
        {
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(g => g.Director).Include(g => g.UserMovies).ThenInclude(ug => ug.User)
                .OrderByDescending(g => g.PublishDate).ThenByDescending(g => g.Revenue).ThenBy(g => g.Name)
                .Select(g => new MovieModel()
                {
                    Guid = g.Guid,
                    Id = g.Id,
                    Name = g.Name,
                    PublishDate = g.PublishDate,
                    DirectorId = g.DirectorId,
                    Revenue = g.Revenue,
                    DirectorOutput = g.Director.Name,
                    RevenueOutput = (g.Revenue ?? 0).ToString("C2"),
                    PublishDateOutput = g.PublishDate.HasValue ? g.PublishDate.Value.ToString("MM/dd/yyyy") : string.Empty,

                    Users = g.UserMovies.Select(ug => new UserModel()
                    {
                        UserName = ug.User.UserName,
                        Status = ug.User.Status
                    }).ToList(),

                    UsersInput = g.UserMovies.Select(ug => ug.UserId).ToList()
                });
        }

        public Result Add(MovieModel model)
        {
            if (_db.Movies.Any(g => g.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Movie with the same name exists!");
            Movie entity = new Movie()
            {
                Name = model.Name.Trim(),
                PublishDate = model.PublishDate,
                Revenue = model.Revenue,
                DirectorId = model.DirectorId,

                UserMovies = model.UsersInput?.Select(userInput => new UserMovie()
                {
                    UserId = userInput
                }).ToList()
            };

            _db.Movies.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public Result Update(MovieModel model)
        {
            if (_db.Movies.Any(g => g.Id != model.Id && g.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Movie with the same name exists!");

            Movie entity = _db.Movies.Include(g => g.UserMovies).SingleOrDefault(g => g.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Movie not found!");

            _db.UserMovies.RemoveRange(entity.UserMovies);

            entity.Name = model.Name.Trim();
            entity.PublishDate = model.PublishDate;
            entity.Revenue = model.Revenue;
            entity.DirectorId = model.DirectorId;
            entity.UserMovies = model.UsersInput?.Select(userInput => new UserMovie()
            {
                UserId = userInput
            }).ToList();

            _db.Movies.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Movie entity = _db.Movies.Include(g => g.UserMovies).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return new ErrorResult("Movie not found!");

            _db.UserMovies.RemoveRange(entity.UserMovies);

            _db.Movies.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Movie deleted successfully.");
        }
    }
}
