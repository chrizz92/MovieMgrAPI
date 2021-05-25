using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;

namespace MovieMgr.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteMovie(Movie movie)
        {
            _dbContext.Movies.Remove(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return _dbContext.Movies.OrderBy(m => m.Title).ToList();
        }

        public int GetCount()
        {
            return _dbContext.Movies.Count();
        }

        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public List<Movie> GetMoviesByCategoryId(int? categoryId)
        {
            if (categoryId == null)
            {
                return GetAllMovies();
            }
            else
            {
                return _dbContext.Movies.Where(m => m.Category_Id == categoryId).ToList();
            }
        }

        public void InsertMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            _dbContext.Movies.Update(movie);
        }
    }
}